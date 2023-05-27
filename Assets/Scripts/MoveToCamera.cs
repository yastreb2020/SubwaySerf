using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCamera : MonoBehaviour
{
    SpawnManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * -gameManager.GetCurrentSpeed() * Time.deltaTime, Space.World);
        if(transform.position.z < gameManager.boundaryZ)
        {
            Destroy(gameObject);
        }
    }
}
