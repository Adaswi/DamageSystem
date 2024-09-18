using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smooth;

    public UnityEvent<MovementData> OnChasePlayer;

    public void ChasePlayer()
    {
        var rotation = Quaternion.LookRotation(player.position - transform.position);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
        OnChasePlayer.Invoke(new MovementData(0,1));
    }

    private void Update()
    {
        ChasePlayer();
    }
}
