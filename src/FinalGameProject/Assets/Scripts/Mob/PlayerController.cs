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

    public float passiveEnergyUse = 0.01f; //in seconds
    public float movementEnergyUse = 0.15f;

    public TileBase currentTile;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager.instance.inventory.LazyCharge(-passiveEnergyUse * Time.deltaTime);
        Vector3Int location = GameManager.instance.floorMap.WorldToCell(transform.position);
        currentTile = GameManager.instance.floorMap.GetTile(location);

        if(currentTile.name == "Charger")
        {
            PlayerManager.instance.inventory.LazyCharge((4 + (1 + ResearchConsole.calculateBonus(ResearchConsole.UpgradeType.ENERGY))) * Time.deltaTime);
        }
        if(currentTile.name == "Lava")
        {
            PlayerManager.instance.health.Damage(4);
        }


        
    }

    private void FixedUpdate()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            //battery.charge -= movementEnergyUse * Time.deltaTime;
            PlayerManager.instance.inventory.LazyCharge(-movementEnergyUse * Time.deltaTime * ResearchConsole.calculateBonus(ResearchConsole.UpgradeType.ENERGY));
        }
        
        if(PlayerManager.instance.inventory.GetTotalCharge() != 0)
        {
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        } else
        {
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 0.5f;
        }
        
    }
}
