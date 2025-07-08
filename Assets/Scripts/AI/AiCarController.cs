using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCarController : MonoBehaviour, IAICar
{
    [SerializeField] private float gasDampen;
    [SerializeField] private float maximumAngle = 90f;
    [SerializeField] private float maximumSpeed = 120f;
    private CheckPoint currentCheckPoint;
    private MunStudios.CarController carController;
    private float currentAngle;
    private float gasInput;
    private bool isInsideBraking = false;

    private AIState aIState;

    void Start()
    {
        carController = GetComponent<MunStudios.CarController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brake"))
        {
            isInsideBraking = true;
            StartCoroutine(ResetBraking());
        }

        CheckPoint checkPoint = other.GetComponent<CheckPoint>();

        if (checkPoint != null)
        {
            RaceManager.instance.OnAIEnteredCheckPoint(checkPoint, this);
        }
    }

    private IEnumerator ResetBraking()
    {
        yield return new WaitForSeconds(0.5f);

        isInsideBraking = false;
    }

    void Update()
    {
        switch (aIState)
        {
            case AIState.Idle:
                carController.SetDriveState(false);
                break;

            case AIState.Driving:
                carController.SetDriveState(true);
                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                currentAngle = Vector3.SignedAngle(fwd, currentCheckPoint.transform.position - transform.position, Vector3.up);
                gasInput = Mathf.Clamp01((1f - Mathf.Abs(carController.speed * 0.02f * currentAngle) / maximumAngle));

                if (isInsideBraking)
                {
                    gasInput = -gasInput * ((Mathf.Clamp01((carController.speed) / maximumSpeed) * 2 - 1f));
                }
                gasDampen = Mathf.Lerp(gasDampen, gasInput, Time.deltaTime * 3f);
                carController.SetInput(gasDampen, currentAngle, 0, 0);
                Debug.DrawRay(transform.position, currentCheckPoint.transform.position - transform.position, Color.yellow);
                break;
        }


    }

    public CheckPoint GetCurrentCheckPoint()
    {
        return currentCheckPoint;
    }

    public AiCarController GetAI()
    {
        return this;
    }

    public void SetCurrentCheckPoint(CheckPoint checkPoint)
    {
        currentCheckPoint = checkPoint;
    }

    public void SetDrivingState(AIState state)
    {
        aIState = state;
    }

    public void OnRaceWon()
    {
        carController.SetInput(-1f, UnityEngine.Random.Range(-90f, 90f), 0, 0);
    }
}

public enum AIState
{
    Idle,
    Driving
}