using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask mask;

    private Weapon weapon;
    private RaycastHit hit;
    private bool isReady;
    private bool isAttacking;

    public UnityEvent OnAttack;

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
        if (isReady && !isAttacking && Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, weapon.range.value, mask)) //On hit when attack isn't being executed
        {
            isAttacking = true;
            var bodypart = hit.transform.gameObject.GetComponent<Bodypart>();
            if (bodypart != null)
                bodypart.Hit(weapon.attack.value, weapon.effects.values);
            OnAttack?.Invoke();
            Invoke(nameof(ExitAttack), weapon.speed.value);
        }
    }
}
