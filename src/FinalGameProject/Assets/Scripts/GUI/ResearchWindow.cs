using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResearchWindow : MonoBehaviour
{

    public TextMeshProUGUI pointText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = "Total research points: " + ResearchConsole.RESEARCH_POINTS;
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
