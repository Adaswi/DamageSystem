using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAttackSystem : AttackSystem
{
    [SerializeField] private Transform enemyView;
    [SerializeField] private LayerMask mask;

    public void DetermineBodypart()
    {
        if (!IsReady || IsAttacking)
            return;

        var objects = Physics.OverlapSphere(enemyView.position, Weapon.Range, mask);
        var bodyparts = new List<IBodypart>();
        foreach (var obj in objects)
        {
            var bodypart = obj.GetComponent<Bodypart>();
            if (bodypart != null)
                bodyparts.Add(bodypart);
        }
        var index = Random.Range(0, bodyparts.Count);

        if (!bodyparts.Any() || bodyparts[index] == null)
            Miss();
        else
            Attack(bodyparts[index]);
    }
}
