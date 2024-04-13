using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveAbility : MonoBehaviour
{
    // 점프, 스태미나 구현
    public bool IsJumping => !_characterController.isGrounded; //-> true면 물체 잡아
    // todo. 물체 잡은게 true이면 -> 올라가

    // * 캐릭터 이동 기능 구현 [WASD] 키
    CharacterController _characterController;
    Animator _animator;

    public float MoveSpeed = 2f;
    public float RunSpeed = 5f;
    public float SlowSpeed = 1f;
    public float JumpPower = 5f;
    private float _yVelocity = 0f;
    private float _gravity = -9.8f;
    private float _velocitySmoothing;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isGrounded = _characterController.isGrounded;
        HandleMovement();
        HandleJumpAndFall(isGrounded);
    }

    void HandleMovement()
    {
        // 1. 입력받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 방향구하기
        Vector3 horizontalDir = new Vector3(h, 0, v);
        horizontalDir.Normalize();
        horizontalDir = Camera.main.transform.TransformDirection(horizontalDir);

        // 3-1. 중력값 적용
        _yVelocity += _gravity * Time.deltaTime;
        Vector3 finalDir = new Vector3(horizontalDir.x, _yVelocity, horizontalDir.z);

        float speed = MoveSpeed;

        // 4. 스태미나 적용
        if (Input.GetKey(KeyCode.LeftShift) && (h != 0 || v != 0))
        {
            speed = RunSpeed;
            _animator.SetFloat("Move", Mathf.Lerp(_animator.GetFloat("Move"), 1.0f, Time.deltaTime * 3));
        }

        // 4-1. 느린 걷기 적용
        else if (Input.GetMouseButton(0) && (h != 0 || v != 0))
        {
            speed = SlowSpeed;
            _animator.SetFloat("Move", Mathf.Lerp(_animator.GetFloat("Move"), 0.33f, Time.deltaTime * 5));
        }

        else if (h != 0 || v != 0) // 일반 걷기
        {
            speed = MoveSpeed;
            _animator.SetFloat("Move", Mathf.Lerp(_animator.GetFloat("Move"), 0.66f, Time.deltaTime * 4));
        }
        else // 정지 상태
        {
            _animator.SetFloat("Move", Mathf.Lerp(_animator.GetFloat("Move"), 0f, Time.deltaTime * 8));
        }


        // 3. 이동하기
        _characterController.Move(finalDir * speed * Time.deltaTime);
        //_animator.SetFloat("Move", horizontalDir.magnitude);
    }

    void HandleJumpAndFall(bool isGrounded)
    {
        // 5. 점프 적용
       if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _yVelocity = JumpPower;
                _animator.SetTrigger("Jump");
                
            }
            else
            {
                _yVelocity = -0.5f;
            }
        }
        else
        {
            _yVelocity = Mathf.SmoothDamp(_yVelocity, _gravity * Time.deltaTime, ref _velocitySmoothing, 0.4f, Mathf.Abs(_gravity * 1.1f));
        }
    }
}

