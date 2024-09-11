using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody playerBody;
    public Transform playerOrientation;
    public GroundCollider groundCheck;

    public float movementSpeed;
    public float horizontalInput;
    public float verticalInput;
    public float jumpPower;
    public float jumpCooldown;
    public float onGroundDrag = 5;
    public float inAirDrag = 0.1f;
    public KeyCode jumpKey = KeyCode.Space;

    private float angle;
    private float angle2;

    public float Angle
    {
        get
        {
            return angle;
        }
        set
        {
            if (angle != value)
            {
                angle = value;
                OnAngleChange?.Invoke();
            }
        }
    }

    public Action OnAngleChange;
   
    private RaycastHit slope;
    private RaycastHit slope2;
    private Vector3 projectedMovementDirection;
    private Vector3 movementDirection;

    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        OnAngleChange += AngleChange;
        groundCheck.OnGroundedChange += GroundedChange;
    }

    // Update is called once per frame
    private void Update()
    {
        ReadDirection();
    }

    // FixedUpdate is called once per physics event
    void FixedUpdate()
    {

        if (groundCheck.IsGrounded)
        {
            if (Physics.Raycast(transform.position + Vector3.down, Vector3.down, out slope, 0.5f))
                Angle = Vector3.Angle(slope.normal, Vector3.up);
            else
                Angle = 0;

            if (Physics.Raycast(transform.position + Vector3.down, movementDirection, out slope2, 0.5f))
                angle2 = Vector3.Angle(slope2.normal, Vector3.up);
            else
                angle2 = 0;

            if (Angle < 45 && Angle > 0)
                playerBody.useGravity = false;
            else
                playerBody.useGravity = true;

            playerBody.drag = onGroundDrag;
            projectedMovementDirection = Vector3.ProjectOnPlane(movementDirection, slope.normal);
            playerBody.AddForce(projectedMovementDirection.normalized * movementSpeed * (float)Math.Pow(Mathf.Clamp(angle2, 45, 90) - 44f, -0.8), ForceMode.Force);

            Debug.Log("Is on ground");
        }
        else if (!groundCheck.IsGrounded)
        {
            playerBody.drag = inAirDrag;
            playerBody.useGravity = true;

            Debug.Log("Is in air");
        }

        if (Input.GetKey(jumpKey) && groundCheck.IsGrounded && !isJumping)
        {
            isJumping = true;
            Jump();
            Invoke(nameof(JumpExit), jumpCooldown);

            Debug.Log("Jumping");
        }
    }

    //Get movement direction for player
    private void ReadDirection()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementDirection = (playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput);
    }

    private void Jump()
    {
        playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);
        playerBody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    public void JumpExit()
    {
        isJumping = false;
    }

    public void GroundedChange()
    {
        if (!groundCheck.IsGrounded && !isJumping && playerBody.velocity.y > 0)
        {
            playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);

            Debug.Log("Changed ground state");
        }
    }

    public void AngleChange()
    {
        if (groundCheck.IsGrounded && !isJumping)
            playerBody.velocity = Vector3.ProjectOnPlane(playerBody.velocity, slope.normal);
    }
}
