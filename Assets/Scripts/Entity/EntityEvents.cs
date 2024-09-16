using System;
using System.Collections.Generic;

public struct EntityEvents
{
    public Action<int, List<float>> OnHit;
    public Action OnHit0arg;
    public Action OnDeath;

    public Action OnLegDeath;

    public Action OnItemEquip;
    public Action OnItemDrop;

    public Action OnHealthIncreased;
    public Action OnHealthDecreased;

    public Action OnMove;

}
