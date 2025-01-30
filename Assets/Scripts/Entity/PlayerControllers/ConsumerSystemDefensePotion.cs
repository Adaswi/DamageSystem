public class ConsumerSystemDefensePotion : ConsumerSystem<DefensePotion, HealthSystem>
{
    new public void ReadyToConsume(Item item)
    {
        base.ReadyToConsume(item);
        if (!isReady)
            return;
        if (instance == null)
        {
            consumable.component = this.component;
            isReady = true;
        }
        else
            UnreadyToConsume();
    }
}
