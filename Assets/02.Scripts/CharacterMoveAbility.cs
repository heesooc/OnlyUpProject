using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveAbility : MonoBehaviour
{
    CharacterController _characterController;
    Animator _animator;

    public float MoveSpeed = 2f;
    public float RunSpeed = 5f;
    public float SlowSpeed = 1f;
    public float JumpPower = 5f;
    private float _yVelocity = 0f;
    private float _gravity = -9.8f;

    public float enhancedJumpPower = 15f; // 침대에서 뛸 때 추가되는 점프 힘

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isGrounded = _characterController.isGrounded;
        if (isGrounded && _yVelocity < 0)
        {
            _yVelocity = -0.5f; // 캐릭터가 바닥에 "붙어" 있게 함
        }

        HandleMovement(isGrounded);

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            StartJump(JumpPower);
        }

        ApplyGravity();
    }

    void HandleMovement(bool isGrounded)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = Camera.main.transform.TransformDirection(h, 0, v).normalized;

        float speed = CalculateSpeed(h, v);
        Vector3 movement = moveDirection * speed * Time.deltaTime;
        _characterController.Move(movement);

        UpdateAnimator(moveDirection.magnitude, speed);
    }

    float CalculateSpeed(float h, float v)
    {
        if (Input.GetKey(KeyCode.LeftShift) && (h != 0 || v != 0))
        {
            return RunSpeed;
        }
        if (Input.GetMouseButton(0) && (h != 0 || v != 0))
        {
            return SlowSpeed;
        }
        return MoveSpeed;
    }

    void UpdateAnimator(float magnitude, float speed)
    {
        // 현재 애니메이터에 설정된 'Move' 값을 가져옴
        float currentBlend = _animator.GetFloat("Move");
        float targetBlend;

        if (magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetBlend = 1.0f; // 달리기
            }
            else if (Input.GetMouseButton(0))
            {
                targetBlend = 0.33f; // 느린 걷기
            }
            else
            {
                targetBlend = 0.66f; // 일반 걷기
            }
        }
        else
        {
            targetBlend = 0f; // 정지
        }

        // Mathf.Lerp 함수를 사용하여 현재 값에서 목표 값으로 부드럽게 전환
        _animator.SetFloat("Move", Mathf.Lerp(currentBlend, targetBlend, Time.deltaTime * 5));
    }

    void ApplyGravity()
    {
        _yVelocity += _gravity * Time.deltaTime;
        _characterController.Move(new Vector3(0, _yVelocity, 0) * Time.deltaTime);
    }

    void StartJump(float power)
    {
        _yVelocity = power;
        _animator.SetTrigger("Jump");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Bed") && _characterController.isGrounded)
        {
            StartJump(enhancedJumpPower);
        }
    }
}