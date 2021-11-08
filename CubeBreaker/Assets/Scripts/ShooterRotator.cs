using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    private enum RotateState
    {
        Idle, Vertical, Horizontal, Ready
    }
    //enum 을 사용하여 현제 상테를 저장하여 하용할 수 있다.
    //Case 와 상성이 좋다.
    private RotateState state = RotateState.Idle;

    public float verticalRotateSpeed = 360f;

    public float horizontalRotatepeed = 360f;

    //게임을 시작했을때 상테를 idle 로 지정

    public BallShooter ballShooter;
    void Update()
    {
        switch(state){
            case RotateState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = RotateState.Horizontal;
                }
            break;

            case RotateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalRotatepeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
            break;

            case RotateState.Vertical:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                    ballShooter.enabled = true;
                }
            break;
        }
    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.identity; //회전 상테가 0, 0, 0 인 초기 상태
        state = RotateState.Idle;
        ballShooter.enabled = false;
    }
}