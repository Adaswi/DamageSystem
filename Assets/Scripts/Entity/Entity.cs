using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public virtual void Hit(int damage, List<float> effects)
    {
        Debug.Log("Entity " + gameObject.name + " hit!");
    }

    public virtual void Death()
    {
        Debug.Log("Entity " + gameObject.name + " died");
    }
}
