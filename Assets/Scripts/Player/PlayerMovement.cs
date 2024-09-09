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
   
    private RaycastHit slope;
    private Vector3 slopeMovementDirection;
    private Vector3 movementDirection;

    public bool isOnSlope;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(IsOnSlope() && isOnSlope && groundCheck.isGrounded)
        {
            playerBody.drag = onGroundDrag;
            slopeMovementDirection = Vector3.ProjectOnPlane(movementDirection, slope.normal);
            playerBody.AddForce(slopeMovementDirection.normalized * movementSpeed, ForceMode.Force);
            playerBody.AddForce(-Physics.gravity, ForceMode.Acceleration);

        }
        //When player just entered slope
        else if(IsOnSlope() && !isOnSlope && groundCheck.isGrounded)
        {
            isOnSlope = true;
            playerBody.drag = onGroundDrag;
            OnSlopeEnter(playerBody);
            slopeMovementDirection = Vector3.ProjectOnPlane(movementDirection, slope.normal);
            playerBody.AddForce(slopeMovementDirection.normalized * movementSpeed, ForceMode.Force);
            playerBody.AddForce(-Physics.gravity, ForceMode.Acceleration);
        }
        //When player just exited slope
        else if(!IsOnSlope() && isOnSlope && groundCheck.isGrounded)
        {
            isOnSlope = false;
            playerBody.drag = onGroundDrag;
            OnSlopeExit(playerBody);
            playerBody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
        }
        //When player is on solid ground
        else if(!IsOnSlope() && !isOnSlope && groundCheck.isGrounded)
        {
            playerBody.drag = onGroundDrag;
            playerBody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
        }
        else
        {
            playerBody.drag = inAirDrag;
        }

        if (Input.GetKey(jumpKey) && groundCheck.isGrounded && isJumping == false)
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
        if (Physics.Raycast(transform.position, Vector3.down, out slope, transform.localScale.y + 0.8f))
        {
            if (slope.normal != Vector3.up)
            {
                angle = Vector3.Angle(slope.normal, Vector3.up);
                if (angle < 45f)
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void OnSlopeExit(Rigidbody player)
    {
        player.velocity = new Vector3(player.velocity.x, 0, playerBody.velocity.z);
    }

    public void OnSlopeEnter(Rigidbody player)
    {
        player.velocity = Vector3.ProjectOnPlane(player.velocity, slope.normal);
    }
}
