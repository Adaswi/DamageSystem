using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask mask;

    private Weapon weapon;
    private RaycastHit hit;
    private bool isReady;
    private bool isAttacking;

    public UnityEvent<Bodypart, Weapon> OnAttack;
    public UnityEvent OnAttackReady;
    public UnityEvent OnAttackUnready;

    public bool IsAttacking
    {
        get { return isReady; }
        set { isAttacking = value; }
    }

    //Ready to attack when weapon is equipped
    public void ReadyToAttack(GameObject item)
    {
        var weapon = item.GetComponent<Weapon>();
        if (weapon == null)
            return;
        
        this.weapon = weapon;
        isReady = true;
        OnAttackReady?.Invoke();      
    }

    public void UnreadyToAttack()
    {
        this.weapon = null;
        isReady = false;
        OnAttackUnready?.Invoke();
    }

    public void Attack()
    {
        if (isReady && !isAttacking && Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, weapon.range.value, mask)) //On hit when attack isn't being executed
        {
            var bodypart = hit.transform.gameObject.GetComponent<Bodypart>();
            if (bodypart == null)
                return;
            OnAttack?.Invoke(bodypart, weapon);
        }
    }
}
