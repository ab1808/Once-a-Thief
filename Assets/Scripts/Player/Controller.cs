using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        // Adjustable Variables
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float playerSpeed = 2f;
        [SerializeField] private float sprintSpeed = 4f;
        [SerializeField] private float normalSpeed = 2f;
        [SerializeField] private float jumpSpeed = 5f;
        [SerializeField] private float gravity = -9.8f;
        [SerializeField] private Transform cameraTransform;

        // Non-Adjustable Variables
        private float _ySpeed;
        private Vector3 _velocity;
        private Vector3 _moveDirection;

        // Jump Grace Time
        [SerializeField] private float jumpButtonGracePeriod;
        private float? _lastGroundedTime;
        private float? _jumpButtonPressedTime;

        // Booleans
        public bool sprintPressed;
        public bool movePressed;
        public bool jumpPressed;
        private bool _isJumping;
        private bool _isGrounded;

        // References
        private CharacterController _characterController;
        public Animator animator;
        private PlayerInputMap _playerInputMap;

        // Animation IDs
        private static readonly int InputMagnitude = Animator.StringToHash("InputMagnitude");
        private static readonly int Speed = Animator.StringToHash("speed");

        // Audio
        public AudioSource footstepsSound, runningSound;
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");

        public Controller(Transform cameraTransform, float jumpButtonGracePeriod, bool isJumping)
        {
            this.cameraTransform = cameraTransform;
            this.jumpButtonGracePeriod = jumpButtonGracePeriod;
            this._isJumping = isJumping;
        }

        private void Awake()
        {
            _playerInputMap = new PlayerInputMap();
        }

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerInputMap.Enable();
        }

        private void OnDisable()
        {
            _playerInputMap.Disable();
        }

        private void Update()
        {
            Jumping();
            Move();
        }

        private void Move()
        {
            sprintPressed = _playerInputMap.Player.Sprint.IsPressed(); // Sprint Buttons

            Vector2 movementInput = _playerInputMap.Player.Move.ReadValue<Vector2>(); // WASD or movement
        
            _moveDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;

            float inputMagnitude = _moveDirection.magnitude; 
            inputMagnitude = Mathf.Clamp01(inputMagnitude); // Keeping the value of magnitude below 1.

            // Animator blend tree movement input calculation
            float targetSpeed = sprintPressed ? sprintSpeed : playerSpeed;
            float joggingSpeed = (normalSpeed + sprintSpeed) / 2f;
            float currentSpeed = Mathf.Lerp(0f, targetSpeed, inputMagnitude); 
            currentSpeed = Mathf.Clamp01(currentSpeed / joggingSpeed); 

            // Apply movement
            _characterController.Move(transform.TransformDirection(_moveDirection) * (currentSpeed * joggingSpeed * Time.deltaTime));

            // Set footstep sounds
            footstepsSound.enabled = currentSpeed > 0 && !sprintPressed && _isGrounded;
            runningSound.enabled = currentSpeed > 0 && sprintPressed && _isGrounded;

            // Set animator parameter "speed" for blend tree transition
            animator.SetFloat(Speed, currentSpeed); // Set the "speed" parameter of the blend tree
        }



        private void Jumping()
        {
            jumpPressed = _playerInputMap.Player.Jump.IsPressed();
        
            if (_characterController.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
                _isGrounded = true;
                animator.SetBool(IsGrounded, true);
                animator.SetBool(IsJumping, false); // Reset jumping animation
                animator.SetBool(IsFalling, false); // Reset falling animation
            }
            else
            {
                _isGrounded = false;
                animator.SetBool(IsGrounded, false);
            }

            _velocity.y += gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);

            // Jump Logic
            if (jumpPressed && _characterController.isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
                animator.SetBool(IsJumping, true);
                _isJumping = true;
            }
        }



        private void LateUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            Vector2 rotationInput = _playerInputMap.Player.Camera.ReadValue<Vector2>(); // Get camera rotation input

            if (rotationInput != Vector2.zero)
            {
                // Rotate the player character around y-axis
                var playerTransform = transform;
                Vector3 playerEuler = playerTransform.eulerAngles;
                playerEuler.y += rotationInput.x * rotationSpeed * Time.deltaTime;
                playerTransform.eulerAngles = playerEuler;

                // Rotate the camera around x-axis
                Vector3 cameraEuler = cameraTransform.eulerAngles;
                cameraEuler.x -= rotationInput.y * rotationSpeed * Time.deltaTime;
                cameraTransform.eulerAngles = cameraEuler;
            }
        }
    }
}
