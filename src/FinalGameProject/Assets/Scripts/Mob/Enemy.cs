using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Standing,
        Attacking
    }

    RaycastHit2D sightRayHit;

    public EnemyState currentState = EnemyState.Standing;

    public float speed = 2f;

    public float sightRange = 5f;

    public MonsterSpawner spawner; //what spawner it spawned out of, if any.

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckForEnemy())
        {
            currentState = EnemyState.Attacking;
        } else
        {
            currentState = EnemyState.Standing;
        }

        //always try to move towards player
        switch (currentState)
        {
            case EnemyState.Standing:
                
                break;
            case EnemyState.Attacking:
                rb.velocity = (PlayerManager.instance.controller.transform.position - transform.position).normalized * speed;
                break;
            default:

                break;
        }
    }

    private bool CheckForEnemy()
    {
        sightRayHit = Physics2D.Raycast(transform.position, PlayerManager.instance.controller.gameObject.transform.position - transform.position, sightRange);

        if(!sightRayHit)
        {
            return false;
        }
        Debug.Log(sightRayHit.transform.name);
        if (sightRayHit.transform.name == "Player")
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Player")
        {
            PlayerManager.instance.health.Damage(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            PlayerManager.instance.health.Damage(1);
        }
    }

    public void Die()
    {
        if(spawner) //if it was spawned by a spawner
        {
            spawner.spawned--; //remove it from limit if it dies.
        }
        
    }

}
