using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target = null;
    [SerializeField] private GameObject targetOffset = null;
    [SerializeField] private float speed = 1.5f;

    public static CameraFollow instance;

    private bool canFollow = true;

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        if (!canFollow) return;

        this.transform.LookAt(target.transform);
        float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, targetOffset.transform.position) * speed);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetOffset.transform.position, car_Move * Time.deltaTime);
    }

    public void SetTarget(GameObject targetObj, GameObject offset)
    {
        target = targetObj;
        targetOffset = offset;
    }

    public void SetFollowState(bool val)
    {
        canFollow = val;
    }

    public void PanToPlayer(Action OnPanningDone)
    {
        transform.position = target.transform.position + new Vector3(-10, 0, 25f);
        transform.LookAt(target.transform);

        SoundManager.instance.PlaySound(0);

        transform.DOMove(targetOffset.transform.position, 4f);
        transform.DODynamicLookAt(target.transform.position, 4f).OnComplete
        (
            () =>
            {
                SetFollowState(true);
                OnPanningDone?.Invoke();
            }
        );
    }
}
