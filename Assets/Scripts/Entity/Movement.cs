using System;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerOrientation;
    [SerializeField] private GroundCollider groundCheck;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float onGroundDrag = 5;
    [SerializeField] private float inAirDrag = 0.1f;

    [SerializeField] private bool isJumping;
    private float angle;
    private float angle2;
    private RaycastHit slope;
    private RaycastHit slope2;
    private Vector3 projectedMovementDirection;
    private Vector3 movementDirection;

    public bool IsJumping
    {
        get { return isJumping; }
        set { isJumping = value; }
    }

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

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (groundCheck == null)
            groundCheck = GetComponent<GroundCollider>();
    }

    private void OnEnable()
    {
        OnAngleChange += AngleChange;
    }

    private void OnDisable()
    {
        OnAngleChange -= AngleChange;
    }

    void FixedUpdate()
    {
        if (groundCheck.IsGrounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slope, 0.5f))
                Angle = Vector3.Angle(slope.normal, Vector3.up);
            else
                Angle = 0;

            if (Physics.Raycast(transform.position, movementDirection, out slope2, 0.5f))
                angle2 = Vector3.Angle(slope2.normal, Vector3.up);
            else
                angle2 = 0;

            if (Angle < 45 && Angle > 0)
                rb.useGravity = false;
            else
                rb.useGravity = true;

            rb.drag = onGroundDrag;
            projectedMovementDirection = Vector3.ProjectOnPlane(movementDirection, slope.normal);
            rb.AddForce((float)Math.Pow(Mathf.Clamp(angle2, 45, 90) - 44f, -0.8) * movementSpeed * projectedMovementDirection.normalized, ForceMode.Force); //This math equation smoothens player's speed drop in slope angles above 45

            Debug.Log("Is on ground");
        }
        else if (!groundCheck.IsGrounded)
        {
            rb.drag = inAirDrag;
            rb.useGravity = true;

            Debug.Log("Is in air");
        }
    }

    //Get movement direction for player
    public void ReadDirection(MovementData data)
    {
        movementDirection = (playerOrientation.right * data.horizontal + playerOrientation.forward * data.vertical).normalized;
    }

    //Stops player from launching off slopes
    public void ResetUpwardVelocity()
    {
        if (!groundCheck.IsGrounded && rb.velocity.y > 0 && !IsJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            Debug.Log("Reset upward velocity");
        }
    }

    //Roughly preserves speed when player gets on new slope (doesn't work perfect because of collider shape)
    private void AngleChange()
    {
        if (groundCheck.IsGrounded && !IsJumping)
            rb.velocity = Vector3.ProjectOnPlane(rb.velocity, slope.normal);
    }

    public void Impair()
    {
        movementSpeed /= 2;
    }
}
