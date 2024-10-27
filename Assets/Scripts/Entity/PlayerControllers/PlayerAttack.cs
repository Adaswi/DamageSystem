using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask mask;

    private Weapon weapon;
    private RaycastHit hit;
    private bool isReady;

    public UnityEvent<IBodypart, Weapon> OnPlayerAttack;
    public UnityEvent<Weapon> OnPlayerMiss;
    public UnityEvent OnPlayerAttackReady;
    public UnityEvent OnPlayerAttackUnready;

    //Ready to attack when weapon is equipped
    public void ReadyToAttack(GameObject item)
    {
        var weapon = item.GetComponent<Weapon>();
        if (weapon == null)
            return;
        
        this.weapon = weapon;
        isReady = true;
        OnPlayerAttackReady?.Invoke();      
    }

    public void UnreadyToAttack()
    {
        this.weapon = null;
        isReady = false;
        OnPlayerAttackUnready?.Invoke();
    }

    public void Attack()
    {
        Debug.Log(isReady);
        if (!isReady)
            return;

        Debug.Log(weapon.Range);

        if (Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, weapon.Range, mask)) //On hit when attack isn't being executed
        {
            var bodypart = hit.collider.GetComponent<Bodypart>();
            if (bodypart == null)
                return;
            Debug.Log("hit");
            OnPlayerAttack?.Invoke(bodypart, weapon);
        }
        else
        {
            Debug.Log("miss");
            OnPlayerMiss?.Invoke(weapon);
        }
    }
}
