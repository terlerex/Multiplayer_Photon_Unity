using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    //Vérifier et fonctionnel avec Photon à ne pas toucher. 
    //terlerex 01/09/21 PlayerController

    [Header("Photon")] 
    private PhotonView _view;
    
    [Header("Main")]
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] private float gravity = -13.0f;
    private float _cameraPitch = 0.0f;
    private float _velocityY = 0.0f;
    private CharacterController _controller = null;
    
    [Header("Cursor")]
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    private bool lockCursor = true;
    
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 7.0f;
    [SerializeField] private float runBuildUpSpeed;
    [SerializeField] private KeyCode runKey;

    [Header("Sloop & Jittering")] 
    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLenght;
    
    
    [Header("Smooth")]
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    [Header("Jump")] 
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    private bool _isJumping;

    void Awake()
    {
        _view = GetComponent<PhotonView>();
        _controller = GetComponent<CharacterController>();
    }


    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (!_view.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(_controller);
        }
    }
    
    void Update()
    {
        if(!_view.IsMine)
            return;
        UpdateMouseLook();
        UpdateMovement();
    }

    private void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref  currentMouseDeltaVelocity, mouseSmoothTime);
        
        _cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -80.0f, 80.0f);
        
        playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    private void UpdateMovement()
    {
        if(!_view.IsMine)
            return;
        
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        if (_controller.isGrounded)
                _velocityY = 0.0f;

        _velocityY += gravity * Time.deltaTime;
        
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * movementSpeed + Vector3.up 
            * _velocityY;
        
        _controller.Move(velocity * Time.deltaTime);

        if (targetDir != Vector2.zero && OnSlope())
        {
                _controller.Move(Vector3.down * _controller.height / 2 * slopeForce * Time.deltaTime);
        }
        
        SetMovementSpeed();
        JumpInput();

        //Bloque les actions pendant le sleep
        if (SleepSystem.isSleep)
        {
            _cameraPitch = Mathf.Clamp(_cameraPitch, 0, 0);
            movementSpeed = 0;
            jumpMultiplier = 0;
        }
        else if (SleepSystem.isSleep == false)
        {
            _cameraPitch = Mathf.Clamp(_cameraPitch, -80.0f, 80.0f);
            jumpMultiplier = 7;
        }
    }
     
    private void SetMovementSpeed()
    {
        if(Input.GetKey(runKey))
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        else
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
    }
    
    //terlerex 01/09/2021 Jump
    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !_isJumping)
        {
            _isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        _controller.slopeLimit = 90.0f;
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            _controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!_controller.isGrounded && _controller.collisionFlags != CollisionFlags.Above);
        _controller.slopeLimit = 45.0f;
        _isJumping = false;
    }
    
    private bool OnSlope()
    {
        if (_isJumping)
            return false;
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, _controller.height / 2));
        if (hit.normal != Vector3.up)
            return true;
        return false;
    }
}
