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
        private float ySpeed;
        private Vector3 velocity;
        private Vector3 moveDirection;

        // Jump Grace Time
        [SerializeField] private float jumpButtonGracePeriod;
        private float? lastGroundedTime;
        private float? jumpButtonPressedTime;

        // Booleans
        public bool sprintPressed;
        public bool movePressed;
        public bool jumpPressed;
        private bool isJumping;
        private bool isGrounded;

        // References
        private CharacterController characterController;
        public Animator animator;
        private PlayerInputMap playerInputMap;

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
            this.isJumping = isJumping;
        }

        private void Awake()
        {
            Cursor.visible = false;
            playerInputMap = new PlayerInputMap();
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            playerInputMap.Enable();
        }

        private void OnDisable()
        {
            playerInputMap.Disable();
        }

        private void Update()
        {
            Jumping();
            Move();
        }

        private void Move()
        {
            sprintPressed = playerInputMap.Player.Sprint.IsPressed(); // Sprint Buttons

            Vector2 movementInput = playerInputMap.Player.Move.ReadValue<Vector2>(); // WASD or movement
        
            moveDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;

            float inputMagnitude = moveDirection.magnitude; 
            inputMagnitude = Mathf.Clamp01(inputMagnitude); // Keeping the value of magnitude below 1.

            // Animator blend tree movement input calculation
            float targetSpeed = sprintPressed ? sprintSpeed : playerSpeed;
            float joggingSpeed = (normalSpeed + sprintSpeed) / 2f;
            float currentSpeed = Mathf.Lerp(0f, targetSpeed, inputMagnitude); 
            currentSpeed = Mathf.Clamp01(currentSpeed / joggingSpeed); 

            // Apply movement
            characterController.Move(transform.TransformDirection(moveDirection) * (currentSpeed * joggingSpeed * Time.deltaTime));

            // Set footstep sounds
            footstepsSound.enabled = currentSpeed > 0 && !sprintPressed && isGrounded;
            runningSound.enabled = currentSpeed > 0 && sprintPressed && isGrounded;

            // Set animator parameter "speed" for blend tree transition
            animator.SetFloat(Speed, currentSpeed); // Set the "speed" parameter of the blend tree
        }



        private void Jumping()
        {
            jumpPressed = playerInputMap.Player.Jump.IsPressed();
        
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                isGrounded = true;
                animator.SetBool(IsGrounded, true);
                animator.SetBool(IsJumping, false); // Reset jumping animation
                animator.SetBool(IsFalling, false); // Reset falling animation
            }
            else
            {
                isGrounded = false;
                animator.SetBool(IsGrounded, false);
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            // Jump Logic
            if (jumpPressed && characterController.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
                animator.SetBool(IsJumping, true);
                isJumping = true;
            }
        }



        private void LateUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            Vector2 rotationInput = playerInputMap.Player.Camera.ReadValue<Vector2>(); // Get camera rotation input

            if (rotationInput != Vector2.zero)
            {
                var transform1 = transform;
                Vector3 euler = transform1.eulerAngles;
                euler.y += rotationInput.x * rotationSpeed * Time.deltaTime;
                transform1.eulerAngles = euler;
            }
        }
    }
}
