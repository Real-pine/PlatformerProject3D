using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;
    

    [Header("Look")] public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRotation;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody rigidbody;
    private PlayerInput input;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnStop;
        input.Player.Jump.started += OnJump;
        input.Player.Look.performed += OnLook;
        
        input.Enable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        camCurXRotation += mouseDelta.y * lookSensitivity;
        camCurXRotation = Mathf.Clamp(camCurXRotation, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRotation, 0, 0);
        
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
        
        mouseDelta = Vector2.zero;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void PlayerMove()
    {
        Vector3 dir = (transform.forward * curMovementInput.y) + (transform.right * curMovementInput.x);
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;
        
        rigidbody.velocity = dir;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        curMovementInput = context.ReadValue<Vector2>();
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        curMovementInput = Vector2.zero;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrunded())
        {
            rigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrunded()
    {
        Ray[] rays = new Ray[]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; ++i)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        int jumpZoneForce = 200;
        if (other.gameObject.CompareTag("JumpZone"))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(Vector3.up * jumpZoneForce , ForceMode.Impulse);
        }
    }
}
