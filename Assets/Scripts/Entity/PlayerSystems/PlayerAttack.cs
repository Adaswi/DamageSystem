using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackSystem : AttackSystem
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private LayerMask mask;

    public void DetermineBodypart()
    {
        if (!IsReady || IsAttacking)
            return;

        RaycastHit hit;
        if (Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, Weapon.Range, mask))
        {
            var bodypart = hit.collider.GetComponent<IBodypart>();
            if (bodypart == null)
                return;

            Attack(bodypart);
        }
        else
        {
            Miss();
        }
    }
}
