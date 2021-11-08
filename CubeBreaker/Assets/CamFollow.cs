 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    {
        Idle, Ready, Tracking
    }
    private State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = readyshotZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSiae;
                    break;
            }
        }
    }

    private Transform target;

    public float smoothTime = 0.2f;

    private Vector3 lastmovingVelocity;

    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize = 5f;

    private const float roundReadyZoomSize = 14.5f;
    private const float readyshotZoomSize = 5f;
    private const float trackingZoomSiae = 10f;
    //const를 사용하여 

    private float lastZommSize;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    private void Move()
    {
        targetPosition = target.transform.position;

        Vector3 smoothPostion = Vector3.SmoothDamp(transform.position, targetPosition,
            ref lastmovingVelocity, smoothTime);

        transform.position = smoothPostion;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize,
            ref lastZommSize, smoothTime);

        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
