using UnityEngine;

public class Binder : MonoBehaviour
{
    [SerializeField] private Transform bodypart;

    void Update()
    {
        transform.position = bodypart.position;
        transform.rotation = bodypart.rotation;
        transform.localScale = bodypart.localScale;
    }
}
