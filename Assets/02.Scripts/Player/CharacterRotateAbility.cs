using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotateAbility : MonoBehaviour
{
    // * 캐릭터 및 카메라 회전 기능 구현 (3인칭, 시네머신: Virtual Camera 사용) [마우스] 이동

    private float _mx;
    private float _my;
    public float RotationSpeed = 200f;
    public Transform CameraRoot;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 캐릭터의 초기 회전 설정
        _mx = 180f;  // 게임 시작 시 캐릭터가 180도 회전하도록 설정
        _my = 0f;    // 카메라의 상하 각도 초기화
        UpdateRotation();  // 초기 회전 적용
    }

    private void UpdateRotation()
    {
        // 캐릭터와 카메라 회전 적용
        transform.eulerAngles = new Vector3(0, _mx, 0f);
        CameraRoot.localEulerAngles = new Vector3(-_my, 0, 0f);
    }

    private void Update()
    {
        // 마우스 입력 값
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 회전 값 누적
        _mx += mouseX * RotationSpeed * Time.deltaTime;
        _my += mouseY * RotationSpeed * Time.deltaTime;
        _my = Mathf.Clamp(_my, -90f, 90f);

        // 회전 적용
        UpdateRotation();
    }
}
