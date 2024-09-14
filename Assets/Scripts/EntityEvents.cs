using System;
using System.Collections.Generic;

public struct EntityEvents
{
    public Action OnEntityHit;
    public Action OnEntityDeath;

    public Action<int, List<float>> OnBodypartHit;
    public Action OnBodypartDeath;

    public Action OnItemEquip;
    public Action OnItemDrop;

    public Action OnHealthIncreased;
    public Action OnHealthDecreased;
    public Action OnDeath;
}
