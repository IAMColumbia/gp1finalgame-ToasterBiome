using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public GameObject projectile;
    public float shotCooldown = 0;
    public float maxShotCooldown = 0.15f;

    public GameObject offset;

    public float energyUse = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);

        float angleDegrees = angle * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angleDegrees - 90f);



        //firing
        shotCooldown -= Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            if(GameManager.instance.player.battery.CanUse(energyUse))
            {
                if (shotCooldown <= 0)
                {
                    Instantiate(projectile, offset.transform.position, transform.rotation);
                    shotCooldown = maxShotCooldown;
                    GameManager.instance.player.battery.charge -= energyUse;
                }
            }
                
        }
    }
}
