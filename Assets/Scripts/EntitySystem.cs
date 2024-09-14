using UnityEngine;

public abstract class EntitySystem : MonoBehaviour
{
    [SerializeField] private EntityIdInitializer entityIdInitializer;
    protected EntityID entityID;

    protected virtual void Awake()
    {
        if (entityIdInitializer == null)
            entityIdInitializer = GetComponentInParent<EntityIdInitializer>();
        entityID = entityIdInitializer.EntityId;
    }
}
