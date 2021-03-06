﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Grid grid;

    public Tilemap floorMap;
    public Tilemap wallMap;


    public PlayerController player;

    //public PlayerManager player;

    public GameObject inventoryWindow;
    public GameObject researchWindow;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryWindow.SetActive(!inventoryWindow.activeSelf);
        }
    }
}
