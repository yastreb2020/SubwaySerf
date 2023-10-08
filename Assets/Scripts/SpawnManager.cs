using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    public GameObject[] powerups;
    [SerializeField] GameObject[] buildings;

    bool[] currentPowerups;

    private float[] xSpawnLocations = {-2.5f, 0, 2.5f};
    private float zSpawnLocation = 70;
    private float xBuildingSpawnLocation = -7.7f;

    [SerializeField] float obstacleSpawnTime;
    [SerializeField] float powerupSpawnTime;
    [SerializeField] float buildingSpawnTime;


    [SerializeField] float speed = 5;
    public float boundaryZ = -20;
    private float speedIncreaseTime = 3;
    private float speedIncreaseValue = 0.5f;
    private float maxSpeed = 30.0f;
    int score = 0;
    int scoreValue = 1;

    public UIHandler uiHandler;

    void Start()
    {
        currentPowerups = new bool[powerups.Length];
        InitializeCurrentPowerups();

        StartCoroutine(SpawnCoroutine(obstacles, obstacleSpawnTime));
        StartCoroutine(SpawnCoroutine(powerups, powerupSpawnTime));

        InvokeRepeating("SpawnBuilding", 1.0f, buildingSpawnTime);
        StartCoroutine(IncreaseGameSpeed());

        uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();
    }

    void InitializeCurrentPowerups()
    {
        for(int i = 0; i < currentPowerups.Length; i++)
        {
            currentPowerups[i] = false;
            // Debug.Log("currnt pwerups: " + currentPowerups[i]);
        }
    }

    private void FixedUpdate()
    {
        score += scoreValue;
        uiHandler.SetScoreText(score);
        Debug.Log("Speed: " + speed);
    }

    // 10 sec
    public void ScoreMultiplier()
    {
        scoreValue = 2;
        Debug.Log("Score doubled");
        StartCoroutine(uiHandler.ShowPowerupBar(powerups[1], 10));
        scoreValue = 1;
        Debug.Log("Score normal");
    }

    private IEnumerator IncreaseGameSpeed()
    {
        while (speed < maxSpeed) { 
            yield return new WaitForSeconds(speedIncreaseTime);
            speed += speedIncreaseValue;
        }
    }

    IEnumerator SpawnCoroutine(GameObject[] objectArray, float spawnTimeIndex)
    {
        float oldSpeed = speed;
        while (true)
        {
            yield return new WaitForSeconds(spawnTimeIndex);
            SpawnObject(objectArray);
            if (speed / oldSpeed > 5)
            {
                spawnTimeIndex -= 1;
                oldSpeed = speed;
            }
        }
    }

    private void SpawnObject(GameObject[] objectGroup)
    {
        int randomX = Random.Range(0, xSpawnLocations.Length);
        int randomObject = Random.Range(0, objectGroup.Length);
        Vector3 objectPosition = new Vector3(xSpawnLocations[randomX], objectGroup[randomObject].gameObject.transform.position.y, zSpawnLocation);
        
        Instantiate(objectGroup[randomObject], objectPosition, objectGroup[randomObject].gameObject.transform.rotation);
    }
    private void SpawnBuilding()
    {
        int randomBuilding = Random.Range(0, buildings.Length);
        Vector3 buildingPosition = new Vector3(xBuildingSpawnLocation,
            buildings[randomBuilding].gameObject.transform.position.y, zSpawnLocation);
        Instantiate(buildings[randomBuilding], buildingPosition, buildings[randomBuilding].gameObject.transform.rotation);
    }

    public float GetCurrentSpeed()
    {
        return speed;
    }

}
