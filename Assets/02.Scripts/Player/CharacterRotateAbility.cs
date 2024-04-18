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
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        // 1. 마우스 입력 값
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 2. 회전 값 누적
        _mx += mouseX * RotationSpeed * Time.deltaTime;
        _my += mouseY * RotationSpeed * Time.deltaTime;
        _my = Mathf.Clamp(_my, -90f, 90f);

        // 3. 캐릭터와 카메라를 회전
        transform.eulerAngles = new Vector3(0, _mx, 0f);
        CameraRoot.localEulerAngles = new Vector3(-_my, 0, 0f);

        // 4. 시네머신 -> 패키지매니저에서 설치
        // Virtual Camera -> body: 3rd Person Follow, rig: offset조절

    }
}
