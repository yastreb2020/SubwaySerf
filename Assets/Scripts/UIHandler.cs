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

    public IEnumerator ShowPowerupBar(GameObject powerup, float time)
    {
        GameObject newPowerupBar = Instantiate(powerupTable, GameObject.Find("Canvas").transform);
        Slider powerupSlider = newPowerupBar.GetComponentInChildren<Slider>();
        //newPowerupBar.//GetComponentInChildren(Image);
        newPowerupBar.GetComponentsInChildren<Image>()[4].color = powerup.GetComponent<Renderer>().sharedMaterial.color;
        //Debug.Log($"{powerup.GetComponent<Renderer>().sharedMaterial.color.r} {powerup.GetComponent<Renderer>().sharedMaterial.color.g} {powerup.GetComponent<Renderer>().sharedMaterial.color.b} {powerup.GetComponent<Renderer>().sharedMaterial.color.a}");
        powerupSlider.value = 1;
        for (int i = 0; i < time; i++)
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
