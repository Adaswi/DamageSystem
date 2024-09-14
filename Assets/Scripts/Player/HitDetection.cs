using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask mask;

    private Weapon weapon;
    private RaycastHit hit;
    private bool isReady;
    private bool isAttacking;

    public GameEvent<HitData> OnAttack;

    private void Awake()
    {
        isReady = false;
    }

    //Ready to attack when weapon is equipped
    public void ReadyToAttack(GameObject item)
    {
        var weapon = item.GetComponent<Weapon>();
        if (weapon != null)
        {
            this.weapon = weapon;
            isReady = true;
            Debug.Log("Weapon is ready to attack!");
        }
    }

    public void UnreadyToAttack()
    {
        this.weapon = null;
        isReady = false;
        Debug.Log("Weapon isn't ready to attack");
    }

    private void ExitAttack()
    {
        isAttacking = false;
    }

    public void Attack()
    {
        if (isReady && !isAttacking && Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, weapon.data.range, mask)) //On hit when attack isn't being executed
        {
            isAttacking = true;
            var bodypart = hit.transform.gameObject.GetComponent<Bodypart>();
            bodypart.Hit(weapon.data.attack, weapon.data.effects);
            Invoke(nameof(ExitAttack), weapon.data.speed);
        }
    }
}
