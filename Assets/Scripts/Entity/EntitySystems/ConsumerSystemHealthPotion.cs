public class ConsumerSystemHealthPotion : ConsumerSystem<HealthPotion ,HealthSystem>
{
    new public void ReadyToConsume(Item item)
    {
        base.ReadyToConsume(item);
        if (!isReady)
            return;
        if (consumable.component.Health < consumable.component.HealthMax)
        {
            consumable.component = this.component;
            isReady = true;
        }
        else
            UnreadyToConsume();
    }
}
