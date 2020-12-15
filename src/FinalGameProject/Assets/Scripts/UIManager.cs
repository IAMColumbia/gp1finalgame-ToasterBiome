using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI batteryText;
    public TextMeshProUGUI healthText;

    public GameObject deathScreen;
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        batteryText.text = "Battery: " + (PlayerManager.instance.inventory.GetTotalCharge().ToString("F2") + " / " +  PlayerManager.instance.inventory.GetMaxCharge().ToString("F2"));
        healthText.text = "Health: " + PlayerManager.instance.health.getHealthPercentage().ToString("P");
    }

}
