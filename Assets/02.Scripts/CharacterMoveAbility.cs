using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

// 상태패턴으로 애니메이션 구현해볼까
public class CharacterMoveAbility : MonoBehaviour
{
    // 점프, 스태미나 구현
    public bool IsJumping => !_characterController.isGrounded; //-> true면 물체 잡아
    // todo. 물체 잡은게 true이면 -> 올라가

    // 실습 과제 1. 캐릭터 이동 기능 구현 [WASD] 키
    CharacterController _characterController;
    Animator _animator;

    public float MoveSpeed = 2f;
    public float RunSpeed = 5f;
    public float SlowSpeed = 1f;
    private float _yVelocity = 0f;
    private float _gravity = -9.8f;
    public float JumpPower = 5f;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 1. 입력받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 방향구하기
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        dir = Camera.main.transform.TransformDirection(dir);
        // _animator.SetFloat("Move", dir.magnitude);

        // 3-1. 중력값 적용
        _yVelocity += _gravity * Time.deltaTime;
        dir.y = _yVelocity;

        // 4. 스태미나 적용
        float speed = MoveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RunSpeed;
        }

        // 4-1. 느린 걷기 적용
        else if (Input.GetMouseButton(0)) 
        {
            speed = SlowSpeed;
        }
        else
        {
            speed = MoveSpeed;
        }
        

        // 3. 이동하기
        _characterController.Move(dir * speed * Time.deltaTime);

        // 5. 점프 적용
        if (Input.GetKey(KeyCode.Space) && _characterController.isGrounded)
        {
            _yVelocity = JumpPower;
        }

    }

}

