public class PlayerConsumeRegenerationPotion : PlayerConsume<RegenerationPotion, HealthSystem>
{
    new public void ReadyToConsume(Item item)
    {
        base.ReadyToConsume(item);
        if (!isReady)
            return;
        if (instance == null && component.Health < component.HealthMax)
        {
            consumable.component = this.component;
            isReady = true;
        }
        else
            UnreadyToConsume();
    }
}
