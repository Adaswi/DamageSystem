using System.Collections.Generic;
using UnityEngine;

public interface IBodypart
{
    public void Hit(int damage, List<float> effects);

    public void Death();
}
