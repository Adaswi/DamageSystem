using System;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private Transform playerOrientation;
    [SerializeField] private GroundCollider groundCheck;
    [SerializeField] private JumpDetection jumpDetection;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float onGroundDrag = 5;
    [SerializeField] private float inAirDrag = 0.1f;

    private float angle;
    private float angle2;
    private RaycastHit slope;
    private RaycastHit slope2;
    private Vector3 projectedMovementDirection;
    private Vector3 movementDirection;

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

    void Start()
    {
        OnAngleChange += AngleChange;
        groundCheck.OnGroundedChange += GroundedChange;
    }

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
            playerBody.AddForce(projectedMovementDirection.normalized * movementSpeed * (float)Math.Pow(Mathf.Clamp(angle2, 45, 90) - 44f, -0.8), ForceMode.Force); //This math equation smoothens player's speed drop in slope angles above 45

            Debug.Log("Is on ground");
        }
        else if (!groundCheck.IsGrounded)
        {
            playerBody.drag = inAirDrag;
            playerBody.useGravity = true;

            Debug.Log("Is in air");
        }
    }

    //Get movement direction for player
    public void ReadDirection(MovementData data)
    {
        movementDirection = (playerOrientation.right * data.horizontal + playerOrientation.forward * data.vertical).normalized;
    }

    //Stops player from launching off slopes
    private void GroundedChange()
    {
        if (!groundCheck.IsGrounded && playerBody.velocity.y > 0 && !jumpDetection.isJumping)
        {
            playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);

            Debug.Log("Changed ground state");
        }
    }

    //Roughly preserves speed when player gets on new slope (doesn't work perfect because of collider shape)
    private void AngleChange()
    {
        if (groundCheck.IsGrounded && !jumpDetection.isJumping)
            playerBody.velocity = Vector3.ProjectOnPlane(playerBody.velocity, slope.normal);
    }
}
