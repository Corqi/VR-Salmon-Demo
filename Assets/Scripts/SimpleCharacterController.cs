using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The speed at which the player moves.")]
    [SerializeField]
    private float speed = 6.0F;

    [Tooltip("The speed at which the player moves while crouching.")]
    [SerializeField]
    private float crouchSpeed = 3.0f;

    [Tooltip("The speed at which the player moves while sprinting.")]
    [SerializeField]
    private float sprintSpeed = 12.0F;

    [Tooltip("The key to press to sprint.")]
    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Jumping")]
    [Tooltip("The speed at which the player jumps vertically.")]
    [SerializeField]
    private float jumpSpeed = 8.0F;

    [Tooltip("The force of gravity applied to the player.")]
    [SerializeField]
    private float gravity = 20.0F;

    [Header("Crouching")]
    [Tooltip("Whether the player must hold the crouch key to crouch.")]
    [SerializeField]
    private bool holdToCrouch;

    [Tooltip("The height of the player when crouching.")]
    [SerializeField]
    private float crouchHeight = 1.0f;

    [Tooltip("The key to press to crouch.")]
    [SerializeField]
    private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Looking")]
    [Tooltip("The sensitivity of the mouse when looking around.")]
    [SerializeField]
    private Vector2 sensitivity = new(3.0f, 3.0f);

    private Camera _camera;
    private CharacterController _controller;
    private bool _isCrouching;
    private Vector3 _moveDirection = Vector3.zero;
    private float _standingHeight;

    private void Start()
    {
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
        _standingHeight = _controller.height;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Crouch();
        Movement();
        Look();
    }

    private void Movement()
    {
        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);

            if (Input.GetButton("Jump"))
            {
                _isCrouching = false;
                _moveDirection.y = jumpSpeed;
            }
            else if (_isCrouching)
            {
                _moveDirection *= crouchSpeed;
            }
            else if (Input.GetKey(sprintKey))
            {
                _moveDirection *= sprintSpeed;
            }
            else
            {
                _moveDirection *= speed;
            }
        }

        _moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            if (!holdToCrouch)
            {
                _isCrouching = !_isCrouching;
            }
            else
            {
                _isCrouching = true;
            }

            SetControllerHeight();
        }

        if (Input.GetKeyUp(crouchKey) && holdToCrouch)
        {
            _isCrouching = false;
            SetControllerHeight();
        }
    }

    private void SetControllerHeight()
    {
        _controller.height = _isCrouching ? crouchHeight : _standingHeight;
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * (mouseX * sensitivity.x));

        float newAngle = _camera.transform.localEulerAngles.x - mouseY * sensitivity.y;
        newAngle = (newAngle + 360) % 360;
        if (newAngle > 180) newAngle -= 360;
        newAngle = Mathf.Clamp(newAngle, -90, 90);

        _camera.transform.localRotation = Quaternion.Euler(newAngle, 0, 0);
    }
}