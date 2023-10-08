using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject powerupTable;
    [SerializeField] TextMeshProUGUI scoreTextObject;
    [SerializeField] TextMeshProUGUI gemsTextObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowPowerupBar()
    {
        GameObject newPowerupBar = Instantiate(powerupTable, GameObject.Find("Canvas").transform);
        Slider powerupSlider = newPowerupBar.GetComponentInChildren<Slider>();
        powerupSlider.value = 1;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            powerupSlider.value -= 0.1f;
        }
        Destroy(newPowerupBar);
    }

    public void SetScoreText(int score)
    {
        scoreTextObject.text = "Score: " + score;
    }

    public void UpdateGemsText(int gems)
    {
        gemsTextObject.text = $"Gems: {gems}";
    }
}
