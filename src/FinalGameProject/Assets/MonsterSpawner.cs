using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public int spawnLimit = 3;
    public int spawned = 0;

    public GameObject monster;

    public float spawnTimer = 0f;
    public float spawnDelay = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnDelay)
        {
            spawnTimer -= spawnDelay;
            if (spawned < spawnLimit)
            {
                GameObject spawnedMonster = Instantiate(monster, transform.position, Quaternion.identity);
                spawnedMonster.GetComponent<Enemy>().spawner = this;
                spawned++;
            }
        }
    }
}
