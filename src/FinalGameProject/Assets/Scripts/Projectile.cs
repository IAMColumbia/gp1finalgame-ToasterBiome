using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    public float projectileSpeed = 16f;

    public TileBase testTile;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Walls")
        {

            //Vector3Int tilePosition = GameManager.instance.grid.WorldToCell(collision.transform.position);

            Debug.Log("trying to destroy");

            Vector3Int location = GameManager.instance.wallMap.WorldToCell(collision.GetContact(0).point);
            TileBase tile = GameManager.instance.wallMap.GetTile(location);

            if(tile != null)
            {
                if(tile.name == "StoneWall")
                {
                    GameManager.instance.wallMap.SetTile(location, null);
                }



                if (tile.GetType() == typeof(DropTile))
                {
                    DropTile dropTile = (DropTile)tile;
                    Vector3 centeredLocation = new Vector2(location.x + 0.5f, location.y + 0.5f);
                    Instantiate(dropTile.drop, centeredLocation, Quaternion.identity);
                    GameManager.instance.wallMap.SetTile(location, null);
                }
            }


        }

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Die();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.name != "Player")
        {
            Destroy(gameObject);
        }


    }
}
