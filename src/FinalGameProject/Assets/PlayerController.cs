using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    public float speed = 4f;


    public Battery battery;

    public float passiveEnergyUse = 0.01f; //in seconds
    public float movementEnergyUse = 0.15f;

    public TextMeshProUGUI batteryText;

    public TileBase currentTile;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        battery = new Battery();
    }

    // Update is called once per frame
    void Update()
    {
        battery.charge -= passiveEnergyUse * Time.deltaTime;
        batteryText.text = "Battery: " + battery.charge.ToString("F2") + "/" + battery.maxCharge;
        Vector3Int location = GameManager.instance.floorMap.WorldToCell(transform.position);
        currentTile = GameManager.instance.floorMap.GetTile(location);

        if(currentTile.name == "Charger")
        {
            battery.charge += 4 * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            battery.charge -= movementEnergyUse * Time.deltaTime;
        }
        
        if(battery.charge != 0)
        {
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        } else
        {
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 0.5f;
        }
        
    }
}
