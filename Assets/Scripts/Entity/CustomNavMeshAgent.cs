using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CustomNavMeshAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public float destinationReach;
    [SerializeField] public float rotationalSpeed;
    public bool IsDestinationSet {  get; private set; }
    public Vector3 CurrentDestination {  get; private set; }

    public UnityEvent<MovementData> OnAgentMovement;
    public UnityEvent<Vector3> OndDestinationSet;
    public UnityEvent OndDestinationReached;

    private void Awake()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void Update()
    {
        agent.nextPosition = rb.position;

        if (!IsDestinationSet)
            return;

        OnAgentMovement?.Invoke(new MovementData(0, 1));
        var direction = agent.steeringTarget - rb.position;
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationalSpeed));

        if (agent.remainingDistance < destinationReach)
            ReachDestination();
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        CurrentDestination = destination;
        IsDestinationSet = true;
        OndDestinationSet?.Invoke(destination);
    }

    private void ReachDestination()
    {
        IsDestinationSet = false;
        OnAgentMovement?.Invoke(new MovementData(0, 0));
        OndDestinationReached?.Invoke();
    }
}
