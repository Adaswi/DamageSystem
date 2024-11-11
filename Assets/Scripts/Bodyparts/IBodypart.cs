using System.Collections.Generic;

public interface IBodypart
{
    public void Hit(int damage, List<float> effects);

    public void Death();
}
