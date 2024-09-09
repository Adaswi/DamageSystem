using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HitDetection : MonoBehaviour
{
    [SerializeField]private Camera playerCam;
    [SerializeField]private LayerMask mask;

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

    public void UnreadyToAttack(GameObject item)
    {
        var weapon = item.GetComponent<Weapon>();
        if (weapon != null)
        {
            this.weapon = null;
            isReady = false;
            Debug.Log("Weapon isn't ready to attack");
        }
    }

    private void EnterAttack()
    {
        isAttacking = true;
    }

    private void ExitAttack()
    {
        isAttacking = false;
    }

    public void Attack()
    {
        if (isReady && !isAttacking && Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, weapon.data.range, mask)) //On hit when attack isn't being executed
        {
            EnterAttack();
            OnAttack.Raise(new HitData(hit.transform.gameObject, gameObject, weapon));
            Invoke(nameof(ExitAttack), weapon.data.speed);
        }
    }
}
