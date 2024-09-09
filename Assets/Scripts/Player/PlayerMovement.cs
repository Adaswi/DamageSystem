using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UIElements;

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
    private Vector3 slopeMovementDirection;
    private Vector3 movementDirection;

    public bool isOnSlope;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        OnAngleChange += OnSlopeEnter;
    }

    // Update is called once per frame
    private void Update()
    {
        ReadDirection();
    }

    // FixedUpdate is called once per physics event
    void FixedUpdate()
    {
       //When player is on slope
        if(IsOnSlope() && groundCheck.isGrounded)
        {
            playerBody.drag = onGroundDrag;
            slopeMovementDirection = Vector3.ProjectOnPlane(movementDirection, slope.normal);
            playerBody.AddForce(slopeMovementDirection.normalized * movementSpeed, ForceMode.Force);
            playerBody.AddForce(-Physics.gravity, ForceMode.Acceleration);
        }
        else if(!IsOnSlope() && groundCheck.isGrounded)
        {
            playerBody.drag = onGroundDrag;
            playerBody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
        }
        else if (!groundCheck.isGrounded)
        {
            playerBody.drag = inAirDrag;
        }

        if (Input.GetKey(jumpKey) && groundCheck.isGrounded && !isJumping)
        {
            isJumping = true;
            Jump();
            Invoke(nameof(JumpExit), jumpCooldown);
        }
    }

    //Get movement direction for player
    private void ReadDirection()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementDirection = (playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput) * Time.deltaTime;
    }

    private void Jump()
    {
        playerBody.velocity = new Vector3(playerBody.velocity.x, 0f, playerBody.velocity.z);
        playerBody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    public void JumpExit()
    {
        isJumping = false;
    }

    public bool IsOnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slope, transform.localScale.y + 0.5f))
        {
            Angle = Vector3.Angle(slope.normal, Vector3.up);
            if (angle < 45f)
                return true;
        }
        return false;
    }

    public void OnSlopeExit(Rigidbody player)
    {
        player.velocity = new Vector3(player.velocity.x, 0, playerBody.velocity.z);
    }

    public void OnSlopeEnter()
    {
        playerBody.velocity = Vector3.ProjectOnPlane(playerBody.velocity, slope.normal);
    }
}
