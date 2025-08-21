using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float runSpeed = 1;
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
    public bool canLook = true;

    public Action inventory;
    private Rigidbody rb;
    private Animator animator;
    private PlayerCondition playerCondition;
    bool movingbool;
    bool backMovingbool;
    public bool runbool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCondition = GetComponent<PlayerCondition>();
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
        if (canLook)
        {
            CameraLook();
        }
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
            animator.SetBool("isMoving", movingbool);
            animator.SetBool("isBackMoving", backMovingbool);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
            movingbool = false;
            backMovingbool = false;
            animator.SetBool("isMoving", movingbool);
            animator.SetBool("isBackMoving", backMovingbool);
        }
    }
    private void Move()
    {
            Vector3 dir = transform.forward * moveInput.y + transform.right * moveInput.x;
            dir *= moveSpeed * runSpeed;
            dir.y = rb.velocity.y;
            rb.velocity = dir;
            

        //else if(moveInput.y == 1.5)
        //{
        //    animator.SetBool("isMoving", movingbool);
        //    animator.SetBool("isBackMoving", backMovingbool);
        //}
        //else if(moveInput.y == 0)
        //{
        //    rb.velocity = new Vector3(0,rb.velocity.y,0);
        //    animator.SetBool("isMoving", movingbool);
        //    animator.SetBool("isBackMoving", backMovingbool);
        //}
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
            runSpeed = 1.5f; 
            runbool = true;
            animator.SetBool("isRun", runbool);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            runSpeed = 1;
            runbool = false;
            animator.SetBool("isRun", runbool);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && playerCondition.gameUI.stamina.curValue>=10)
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
            animator.ResetTrigger("isJump");
            animator.SetTrigger("isJump");
            playerCondition.gameUI.stamina.curValue -= 10;
        }
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
            if (Physics.Raycast(rays[i],0.3f,groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase==InputActionPhase.Started)
        {
            inventory?.Invoke();
            //ToggleCursor();
        }
    }


    public void ChangeCanLook(bool canlook)
    {
        canLook = canlook;
    }
    public void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
        Debug.Log("Åä±Û");
    }
}
