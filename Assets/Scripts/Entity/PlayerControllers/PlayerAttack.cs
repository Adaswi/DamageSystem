using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask mask;

    private Weapon weapon;
    private RaycastHit hit;
    private bool isReady;

    public UnityEvent<Bodypart, Weapon> OnAttack;
    public UnityEvent OnAttackReady;
    public UnityEvent OnAttackUnready;

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
        if (isReady && Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, weapon.range.value, mask)) //On hit when attack isn't being executed
        {
            var bodypart = hit.collider.GetComponent<Bodypart>();
            if (bodypart == null)
                return;
            OnAttack?.Invoke(bodypart, weapon);
        }
    }
}
