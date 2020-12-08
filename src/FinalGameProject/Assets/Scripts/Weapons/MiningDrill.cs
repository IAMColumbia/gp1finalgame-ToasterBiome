using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningDrill : MonoBehaviour
{

    public float energyUse = 0.25f;
    public float energyUseBurst = 1f;

    public bool activated = false;

    public BoxCollider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(cursorPos.y - transform.position.y, cursorPos.x - transform.position.x);

        float angleDegrees = angle * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angleDegrees - 90f);

        if (Input.GetMouseButton(0))
        {
            if (GameManager.instance.player.battery.CanUse(energyUse))
            {
                activated = true;
                bool mined = CheckMine();
                if(mined)
                {
                    GameManager.instance.player.battery.charge -= energyUseBurst;
                }
                GameManager.instance.player.battery.charge -= energyUse * Time.deltaTime;
            }

        }
        else
        {
            activated = false;
        }
    }

    private bool CheckMine()
    {
        if (!activated)
        {
            Debug.Log("Not activated");
            return false;
        }
        Debug.Log("Colliding");

        if (Vector2.Distance(Cursor.instance.transform.position, transform.position) > 1.75f)
        {
            Debug.Log("Too far away");
            return false;
        }
        //Vector3Int tilePosition = GameManager.instance.grid.WorldToCell(collision.transform.position);

        Debug.Log("trying to destroy");

        Vector3Int location = GameManager.instance.wallMap.WorldToCell(Cursor.instance.transform.position);
        Debug.Log(location);
        TileBase tile = GameManager.instance.wallMap.GetTile(location);


        if (tile != null)
        {
            Debug.Log("Tile: " + tile.name);
            if (tile.name == "StoneWall")
            {
                GameManager.instance.wallMap.SetTile(location, null);
                return true;
            }



            if (tile.GetType() == typeof(DropTile))
            {
                DropTile dropTile = (DropTile)tile;
                Vector3 centeredLocation = new Vector2(location.x + 0.5f, location.y + 0.5f);
                Instantiate(dropTile.drop, centeredLocation, Quaternion.identity);
                GameManager.instance.wallMap.SetTile(location, null);
                return true;
            }

        }

        return false;
    }
}
