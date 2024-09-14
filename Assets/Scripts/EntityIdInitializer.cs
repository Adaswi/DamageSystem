using UnityEngine;

[DefaultExecutionOrder(-50)] //This causes the awake method to execute before entity system's awake method
public class EntityIdInitializer : MonoBehaviour
{
    public EntityID EntityId {  get; private set; }

    private void Awake()
    {
        EntityId = ScriptableObject.CreateInstance<EntityID>();
    }
}
