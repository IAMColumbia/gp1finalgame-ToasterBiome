using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchConsole : MonoBehaviour
{
    public static int RESEARCH_POINTS = 0;

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
        }
    }
}
