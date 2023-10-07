using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    [SerializeField] GameObject[] powerups;
    [SerializeField] GameObject[] buildings;

    [SerializeField] GameObject powerupTable;
    [SerializeField] TextMeshProUGUI scoreTextObject;

    bool[] currentPowerups;

    private float[] xSpawnLocations = {-2.5f, 0, 2.5f};
    private float zSpawnLocation = 70;
    private float xBuildingSpawnLocation = -7.7f;

    [SerializeField] float obstacleSpawnTime;
    [SerializeField] float powerupSpawnTime;
    [SerializeField] float buildingSpawnTime;


    [SerializeField] float speed = 5;
    public float boundaryZ = -20;
    [SerializeField] private float speedIncreaseTime = 1;
    [SerializeField] private float speedIncreaseValue = 1;
    int score = 0;
    int scoreValue = 1;



    void Start()
    {
        currentPowerups = new bool[powerups.Length];
        InitializeCurrentPowerups();

        StartCoroutine(SpawnCoroutine(obstacles, obstacleSpawnTime));
        StartCoroutine(SpawnCoroutine(powerups, powerupSpawnTime));

        InvokeRepeating("SpawnBuilding", 1.0f, buildingSpawnTime);
        InvokeRepeating("IncreaseGameSpeed", speedIncreaseTime, speedIncreaseTime);
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
        SetScoreText(score);
        //Debug.Log("Score: " + score);
    }

    // 10 sec
    public IEnumerator ScoreMultiplier()
    {
        scoreValue = 2;
        Debug.Log("Score doubled");
        GameObject newPowerupBar = Instantiate(powerupTable, GameObject.Find("Canvas").transform);
        Slider powerupSlider = newPowerupBar.GetComponentInChildren<Slider>();
        powerupSlider.value = 1;
        for (int i = 0; i < 10; i++)
        {
            powerupSlider.value -= 0.1f;
            yield return new WaitForSeconds(1);
        }
        Destroy(newPowerupBar);
        scoreValue = 1;
        Debug.Log("Score normal");
    }

    void IncreaseGameSpeed()
    {
        speed += speedIncreaseValue;
    }

    IEnumerator SpawnCoroutine(GameObject[] objectArray, float spawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnObject(objectArray);
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

    private void SetScoreText(int score)
    {
        scoreTextObject.text = "Score: " + score;
    }
}
