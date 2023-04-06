using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCamera : MonoBehaviour
{
    public float speed = 5;
    public float boundaryZ = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);
        if(transform.position.z < boundaryZ)
        {
            Destroy(gameObject);
        }
    }
}
