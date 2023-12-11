using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target = null;
    [SerializeField] private GameObject targetOffset = null;
    [SerializeField] private float speed = 1.5f;

    void FixedUpdate()
    {
        this.transform.LookAt(target.transform);
        float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, targetOffset.transform.position) * speed);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetOffset.transform.position, car_Move * Time.deltaTime);
    }

    public void SetTarget(GameObject targetObj, GameObject offset)
    {
        target = targetObj;
        targetOffset = offset;
    }
}
