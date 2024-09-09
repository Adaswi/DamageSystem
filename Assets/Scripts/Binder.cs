using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binder : MonoBehaviour
{
    public Transform part;
    public Transform item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        item.position = part.position;
        item.rotation = part.rotation;
    }
}
