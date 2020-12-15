using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResearchConsole : MonoBehaviour
{
    public static int RESEARCH_POINTS = 0;

    public static int DRILL_LEVEL = 1; //energy use for drill
    public static int LASER_LEVEL = 1; //energy use for laser
    public static int ENERGY_LEVEL = 1; //energy use for movement + recharge rate

    public TextMeshProUGUI drillCost;
    public TextMeshProUGUI laserCost;
    public TextMeshProUGUI energyCost;

    public enum UpgradeType
    {
        DRILL,
        LASER,
        ENERGY
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Trying to open Research Console");
            GameManager.instance.researchWindow.SetActive(true);
            refreshCosts();
        }
    }

    public int GetCost(UpgradeType upgrade)
    {
        switch (upgrade)
        {
            case UpgradeType.DRILL:
                return DRILL_LEVEL * 4;
            case UpgradeType.LASER:
                return LASER_LEVEL * 4;
            case UpgradeType.ENERGY:
                return ENERGY_LEVEL * 4;
            default:
                return 999;
        }
    }

    void refreshCosts()
    {
        if(DRILL_LEVEL >= 5)
        {
            drillCost.text = $"DRILL - LVL {DRILL_LEVEL} - MAX";
        } else
        {
            drillCost.text = $"DRILL - LVL {DRILL_LEVEL} - COST: {GetCost(UpgradeType.DRILL)}";
        }

        if (LASER_LEVEL >= 5)
        {
            laserCost.text = $"LASER - LVL {LASER_LEVEL} - MAX";
        }
        else
        {
            laserCost.text = $"LASER - LVL {LASER_LEVEL} - COST: {GetCost(UpgradeType.LASER)}";
        }

        if (ENERGY_LEVEL >= 5)
        {
            energyCost.text = $"MOVER - LVL {ENERGY_LEVEL} - MAX";
        }
        else
        {
            energyCost.text = $"MOVER - LVL {ENERGY_LEVEL} - COST: {GetCost(UpgradeType.ENERGY)}";
        }
    }

    public void AttemptUpgrade(int upgradeInt)
    {
        UpgradeType upgrade = (UpgradeType)upgradeInt;

        if(RESEARCH_POINTS >= GetCost(upgrade)) //you have enough points
        {

            switch (upgrade)
            {
                case UpgradeType.DRILL:
                    if(DRILL_LEVEL < 5)
                    {
                        RESEARCH_POINTS -= GetCost(upgrade);
                        DRILL_LEVEL++;
                    }
                    break;
                case UpgradeType.LASER:
                    if (LASER_LEVEL < 5)
                    {
                        RESEARCH_POINTS -= GetCost(upgrade);
                        LASER_LEVEL++;
                    }
                        
                    break;
                case UpgradeType.ENERGY:
                    if (ENERGY_LEVEL < 5)
                    {
                        RESEARCH_POINTS -= GetCost(upgrade);
                        ENERGY_LEVEL++;
                    }
                    
                    break;
                default:
                    Debug.Log("Something broken..");
                    break;
            }

        }

        refreshCosts();
    }

    public static float calculateBonus(UpgradeType type)
    {
        //each upgrade level is basically 10% off energy cost..
        switch (type)
        {
            case UpgradeType.DRILL:
                return 1f - (((float)DRILL_LEVEL-1) / 10f);
            case UpgradeType.LASER:
                return 1f - (((float)LASER_LEVEL-1) / 10f);
            case UpgradeType.ENERGY:
                return 1f - (((float)ENERGY_LEVEL-1) / 10f);
            default:
                return 1f;
        }
    }

    public void ResearchWin()
    {
        if(RESEARCH_POINTS >= 100)
        {
            RESEARCH_POINTS -= 100;
            //you win
            PlayerManager.instance.uiManager.winScreen.SetActive(true);
        }
    }


}
