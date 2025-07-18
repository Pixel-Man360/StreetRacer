using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MunStudios
{
    public class CarController : MonoBehaviour, IPlayer
    {
        [SerializeField] private EngineAudio engineAudio;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private List<Wheel> wheels;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float motorPower;
        [SerializeField] private float brakePower;
        [SerializeField] private AnimationCurve steerCurve;
        [SerializeField] private float maxSteering = 90f;
        [SerializeField] private float minSteering = -90f;
        [SerializeField] private float fallspeed = 3f;
        [SerializeField] private Transform centerOfMass;
        [SerializeField] private Transform cameraOffset;
        [SerializeField] private float redLine;
        [SerializeField] private float idleRPM;
        [SerializeField] private float minNeedleRotation;
        [SerializeField] private float maxNeedleRotation;
        [SerializeField] private float[] gearRatios;
        [SerializeField] private float differentialRatio;
        [SerializeField] private AnimationCurve hpToRPMCurve;
        [SerializeField] private float increaseGearRPM;
        [SerializeField] private float decreaseGearRPM;
        [SerializeField] private float changeGearTime = 0.5f;
        [HideInInspector] public int isEngineRunning;

        [SerializeField] private bool canDrive = false;
        private bool isReversing = false;
        private int currentGear;
        private float steerInput;
        private float gasInput;
        private float rpm;
        public float speed { get; set; }
        private float speedClamped;
        private float slipAngle;
        private float brakeInput;
        private float currentTorque;
        private float clutch;
        private float wheelRPM;

        private GearState gearState;


        void Start()
        {
            Physics.gravity = new Vector3(0, Physics.gravity.y * fallspeed, 0);
            rb.centerOfMass = centerOfMass.transform.localPosition;
        }

        void Update()
        {
            UIManager.instance.SetNeedleRotation(Quaternion.Euler(0, 0, Mathf.Lerp(minNeedleRotation, maxNeedleRotation, rpm / (redLine * 1.1f))));
            UIManager.instance.SetRpmText(Mathf.Abs(speed).ToString("000") + "mph");
            UIManager.instance.SetGearText((gearState == GearState.Neutral) ? "N" : (currentGear + 1).ToString());
        }

        public void SetDriveState(bool val)
        {
            canDrive = val;
        }

        void FixedUpdate()
        {
            speed = wheels[2].GetRPM() * wheels[2].GetRadius() * 2f * Mathf.PI / 10f;
            speedClamped = Mathf.Lerp(speedClamped, speed, Time.deltaTime);

            ApplyMotor();
            CheckEngineAudio();
            CheckSmokeParticles();
            HandleAcceleration();
            HandleBraking();
        }

        private void SetClutch()
        {
            float movingDirection = Vector3.Dot(transform.forward, rb.velocity);
            if (gearState != GearState.Changing)
            {
                if (gearState == GearState.Neutral)
                {
                    clutch = 0;
                    if (Mathf.Abs(gasInput) > 0) gearState = GearState.Running;
                }
                else
                {
                    clutch = Input.GetKey(KeyCode.LeftShift) ? 0 : Mathf.Lerp(clutch, 1, Time.deltaTime);
                }
            }
            else
            {
                clutch = 0;
            }
        }

        private void CheckEngineAudio()
        {
            if (Mathf.Abs(gasInput) > 0 && isEngineRunning == 0)
            {
                engineAudio.StartEngine();
            }

            else if (gasInput == 0)
            {
                if (Mathf.Abs(GetSpeedRatio()) <= 0.05f)
                {
                    engineAudio.StopEngine();
                }
            }
        }

        private void CheckSlip()
        {
            slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);
            float movingDirection = Vector3.Dot(transform.forward, rb.velocity);

            if (movingDirection < -0.5f && gasInput > 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else if (movingDirection > 0.5f && gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else
            {
                brakeInput = 0;
            }
        }


        private void CheckSmokeParticles()
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.SetSmoke();
            }
        }

        void ApplyMotor()
        {
            if (gasInput < 0)
            {
                wheels[2].ApplyMotorTorque(currentTorque / 5f * gasInput);
                wheels[3].ApplyMotorTorque(currentTorque / 5f * gasInput);

                return;
            }

            currentTorque = CalculateTorque();
            wheels[2].ApplyMotorTorque(currentTorque * gasInput);
            wheels[3].ApplyMotorTorque(currentTorque * gasInput);
        }

        float CalculateTorque()
        {
            float torque = 0;
            if (rpm < idleRPM + 200 && gasInput == 0 && currentGear == 0)
            {
                gearState = GearState.Neutral;
            }

            if (gearState == GearState.Running && clutch > 0)
            {
                if (rpm > increaseGearRPM && !isReversing)
                {
                    StartCoroutine(ChangeGear(1));
                }
                else if (rpm < decreaseGearRPM && !isReversing)
                {
                    StartCoroutine(ChangeGear(-1));
                }
            }

            if (isEngineRunning > 0)
            {
                if (clutch < 0.1f)
                {
                    rpm = Mathf.Lerp(rpm, Mathf.Max(idleRPM, redLine * gasInput) + UnityEngine.Random.Range(-50, 50), Time.deltaTime);
                }
                else
                {
                    wheelRPM = Mathf.Abs((wheels[2].GetRPM() + wheels[3].GetRPM()) / 2f) * gearRatios[currentGear] * differentialRatio;
                    rpm = Mathf.Lerp(rpm, Mathf.Max(idleRPM - 100, wheelRPM), Time.deltaTime * 3f);
                    torque = (hpToRPMCurve.Evaluate(rpm / redLine) * motorPower / rpm) * gearRatios[currentGear] * differentialRatio * 5252f * clutch;
                }
            }
            return torque;
        }

        private IEnumerator ChangeGear(int gearChange)
        {
            gearState = GearState.CheckingChange;
            if (currentGear + gearChange >= 0)
            {
                if (gearChange > 0)
                {
                    //increase the gear
                    yield return new WaitForSeconds(0.7f);
                    if (rpm < increaseGearRPM || currentGear >= gearRatios.Length - 1)
                    {
                        gearState = GearState.Running;
                        yield break;
                    }
                }
                if (gearChange < 0)
                {
                    //decrease the gear
                    yield return new WaitForSeconds(0.1f);

                    if (rpm > decreaseGearRPM || currentGear <= 0)
                    {
                        gearState = GearState.Running;
                        yield break;
                    }
                }
                gearState = GearState.Changing;
                yield return new WaitForSeconds(changeGearTime);
                currentGear += gearChange;
            }

            if (gearState != GearState.Neutral)
                gearState = GearState.Running;
        }

        private void HandleAcceleration()
        {
            if (!canDrive) return;

            if (gasInput < 0)
            {
                isReversing = true;
                currentGear = 0;
                rpm = idleRPM;
            }

            else
            {
                isReversing = false;
            }

            if (Mathf.Abs(speed) < maxSpeed)
            {
                if (gasInput < 0)
                {
                    wheels[0].HandleAcceleration(motorPower / 3f * gasInput);
                    wheels[1].HandleAcceleration(motorPower / 3f * gasInput);
                    return;
                }

                wheels[0].HandleAcceleration(motorPower * gasInput);
                wheels[1].HandleAcceleration(motorPower * gasInput);
            }
            else
            {
                wheels[2].HandleAcceleration(0);
                wheels[3].HandleAcceleration(0);
            }
        }


        private void HandleBraking()
        {
            if (!canDrive) return;

            if (brakeInput > 0)
            {
                currentGear = 0;
                rpm = idleRPM;
            }

            for (int i = 0; i < wheels.Count; i++)
            {
                if (i <= 1)
                {
                    wheels[i].ApplyBrake(brakeInput * brakePower * 0.7f);
                }

                else
                {
                    wheels[i].ApplyBrake(brakeInput * brakePower * 0.3f);
                }

            }
        }

        private void HandleSteering(float steeringAngle)
        {
            if (slipAngle < 120f)
            {
                steeringAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);
            }
            steeringAngle = Mathf.Clamp(steeringAngle, minSteering, maxSteering);

            wheels[0].ApplySteerAngle(steeringAngle);
            wheels[1].ApplySteerAngle(steeringAngle);
        }

        public void SetInput(float throttleIn, float steeringIn, float clutchIn, float handbrakeIn)
        {
            if (!canDrive) return;

            gasInput = throttleIn;
            steerInput = steeringIn * steerCurve.Evaluate(speed);

            HandleSteering(steerInput);

            CheckSlip();
            SetClutch();
        }


        internal float GetSpeedRatio()
        {
            float gas = Mathf.Clamp(Mathf.Abs(gasInput), 0.5f, 1f);
            return speedClamped * gas / maxSpeed;
        }

        public Transform GetCameraOffset()
        {
            return cameraOffset;
        }
    }




    public enum GearState
    {
        Neutral,
        Running,
        CheckingChange,
        Changing
    };


}