using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;

    public Vector2 respawnLocation;

    public float damageCooldown = 1.5f;
    public float maxDamageCooldown = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        damageCooldown += Time.deltaTime;
        if(damageCooldown < maxDamageCooldown)
        {
            PlayerManager.instance.spriteRenderer.color = Color.red;
        } else
        {
            PlayerManager.instance.spriteRenderer.color = Color.white;
        }
    }

    public float getHealthPercentage()
    {
        return (float)health / (float)maxHealth;
    }

    public void Damage(int amount)
    {
        if(damageCooldown >= maxDamageCooldown)
        {
            health -= amount;
            damageCooldown = 0;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
        
    }

    public void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Die()
    {
        PlayerManager.instance.uiManager.deathScreen.SetActive(true);
        PlayerManager.instance.controller.enabled = false;
    }

    public void Respawn()
    {
        damageCooldown = maxDamageCooldown;
        PlayerManager.instance.uiManager.deathScreen.SetActive(false);
        transform.position = respawnLocation;
        PlayerManager.instance.controller.enabled = true;
        PlayerManager.instance.inventory.Dump();
        Heal(maxHealth);
    }
}
