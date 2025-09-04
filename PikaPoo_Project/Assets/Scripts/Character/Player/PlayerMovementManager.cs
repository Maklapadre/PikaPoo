using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovementManager : CharacterMovementManager
{

    [Tooltip("Speed ​​at which the character moves. It is not affected by gravity or jumping.")]
    public float velocity = 5f;
    [Tooltip("This value is added to the speed value while the character is sprinting.")]
    public float sprintAdittion = 3.5f;
    [Tooltip("The higher the value, the higher the character will jump.")]
    public float jumpForce = 18f;
    [Tooltip("Stay in the air. The higher the value, the longer the character floats before falling.")]
    public float jumpTime = 0.85f;
    [Space]
    [Tooltip("Force that pulls the player down. Changing this value causes all movement, jumping and falling to be changed as well.")]
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;

    // Player states
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    Animator animator;
    PlayerManager player;
    PlayerInputManager input;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
        input = GetComponent<PlayerInputManager>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        // Message informing the user that they forgot to add an animator
        if (animator == null)
            Debug.LogWarning("Hey buddy, you don't have the Animator component in your player. Without it, the animations won't work.");
    }


    // Update is only being used here to identify keys and trigger animations
    void Update()
    {

        // Check if you pressed the crouch input key and change the player's state
        if (input.CrouchValue > 0)
            isCrouching = !isCrouching;

        // Run and Crouch animation
        // If dont have animator component, this block wont run
        if (player.cc.isGrounded && animator != null)
        {

            // Crouch
            // Note: The crouch animation does not shrink the character's collider
            animator.SetBool("crouch", isCrouching);

            // Run
            float minimumSpeed = 0.9f;
            animator.SetBool("run", player.cc.velocity.magnitude > minimumSpeed);

            // Sprint
            isSprinting = player.cc.velocity.magnitude > minimumSpeed && input.SprintValue > 0;
            animator.SetBool("sprint", isSprinting);

        }

        // Jump animation
        if (animator != null)
            animator.SetBool("air", player.cc.isGrounded == false);

        // Handle can jump or not
        if (input.JumpValue > 0 && player.cc.isGrounded)
        {
            isJumping = true;
            // Disable crounching when jumping
            //isCrouching = false; 
        }

    }

    public void FixedUpdate()
    {
        HandleAllMovement();
    }

    public void HandleAllMovement()
    {
        HandleBaseMovement();
        HeadHittingDetect();
    }

    private void HandleBaseMovement()
    {
        // Sprinting velocity boost or crounching desacelerate
        float velocityAdittion = 0;
        if (isSprinting)
            velocityAdittion = sprintAdittion;
        if (isCrouching)
            velocityAdittion = -(velocity * 0.50f); // -50% velocity

        // Direction movement
        float directionX = input.MoveValue.x * (velocity + velocityAdittion) * Time.deltaTime;
        float directionZ = input.MoveValue.y * (velocity + velocityAdittion) * Time.deltaTime;
        float directionY = 0f;

        // Jump handler
        if (isJumping)
        {

            // Apply inertia and smoothness when climbing the jump
            // It is not necessary when descending, as gravity itself will gradually pulls
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;

            // Jump timer
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        // Add gravity to Y axis
        directionY = directionY - gravity * Time.deltaTime;


        // --- Character rotation --- 

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Relate the front with the Z direction (depth) and right with X (lateral movement)
        forward = forward * directionZ;
        right = right * directionX;

        if (directionX != 0 || directionZ != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        // --- End rotation ---


        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = forward + right;

        Vector3 movement = verticalDirection + horizontalDirection;
        player.cc.Move(movement);
    }



    //This function makes the character end his jump if he hits his head on something
    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(player.cc.center);
        float hitCalc = player.cc.height / 2f * headHitDistance;

        // Uncomment this line to see the Ray drawed in your characters head
        // Debug.DrawRay(ccCenter, Vector3.up * headHeight, Color.red);

        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }

}
