using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject[] powerups;
    public GameObject[] buildings;

    private float[] xSpawnLocations = {-2.5f, 0, 2.5f};
    private float zSpawnLocation = 40;
    private float xBuildingSpawnLocation = -7.7f;


    public float obstacleSpawnTime;
    public float buildingSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1.0f, obstacleSpawnTime);
        InvokeRepeating("SpawnBuilding", 1.0f, buildingSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle()
    {
        int randomX = Random.Range(0, xSpawnLocations.Length);
        int randomObstacle = Random.Range(0, obstacles.Length);
        Vector3 obstaclePosition = new Vector3(xSpawnLocations[randomX], obstacles[randomObstacle].gameObject.transform.position.y, zSpawnLocation);

        Instantiate(obstacles[randomObstacle], obstaclePosition, obstacles[randomObstacle].gameObject.transform.rotation);
    }

    private void SpawnPowerup()
    {

    }
    private void SpawnBuilding()
    {
        int randomBuilding = Random.Range(0, buildings.Length);
        Vector3 buildingPosition = new Vector3(xBuildingSpawnLocation,
            buildings[randomBuilding].gameObject.transform.position.y, zSpawnLocation);
        Instantiate(buildings[randomBuilding], buildingPosition, buildings[randomBuilding].gameObject.transform.rotation);
    }
}
