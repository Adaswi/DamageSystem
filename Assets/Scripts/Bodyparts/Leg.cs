public class Leg : Bodypart
{
    protected override void OnHit()
    {

    }

    protected override void OnDeath()
    {
        entityID.events.OnLegDeath?.Invoke();
    }
}
