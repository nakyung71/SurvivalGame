using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float JumpPower;
    private Vector2 moveInput;
    private Vector2 runInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform CameraContainer;
    public float minXLook;
    public float maxXLook;
    private float cameraMouseDelta;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody rb;
    public Animator animator;
    bool movingbool;
    bool backMovingbool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
            if (moveInput.y == 1)
            {
                movingbool = true;
                backMovingbool = false;
            }
            else
            {
                backMovingbool = true;
                movingbool = false;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
            movingbool = false;
            backMovingbool = false;
        }
    }
    private void Move()
    {
        if (moveInput.y == 1)
        {
            Vector3 dir = transform.forward * moveInput.y;
            dir *= moveSpeed;
            dir.y = rb.velocity.y;
            rb.velocity = dir;
            animator.SetBool("isMoving", movingbool);
            animator.SetBool("isBackMoving", backMovingbool);
        }
        else if(moveInput.y == 2)
        {

        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    void CameraLook()
    {
        cameraMouseDelta += mouseDelta.y * lookSensitivity;
        cameraMouseDelta = Mathf.Clamp(cameraMouseDelta,minXLook,maxXLook);
        CameraContainer.localEulerAngles = new Vector3(-cameraMouseDelta,0,0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
        }
        Debug.Log(IsGrounded());
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down)
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i],0.2f,groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
