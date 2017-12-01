using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blocks;
    public List<Transform> spawnSpots;

    public float spawnDelay;
    public float spawnCooldown;

    public int spawnAmount;

    public bool isStarted = false;
	
	void Update ()
    {
        if (isStarted)
        {
            spawnCooldown -= Time.deltaTime;

            if (spawnCooldown <= 0)
            {
                SpawnBlocks();
                spawnCooldown = spawnDelay;
            }
        }
       
	}
    
    void SpawnBlocks()
    {
        List<Transform> usableSpots = new List<Transform>();
        for(int i = 0; i < spawnSpots.Count; i++)
        {
            usableSpots.Add(spawnSpots[i]);
        }
        
        for (int i = 0; i < spawnAmount; i++)
        {
            int index = Random.Range(0, usableSpots.Count);
            GameObject go = (GameObject)Instantiate(blocks[Random.Range(0, blocks.Length)], usableSpots[index].position, Quaternion.identity);
            go.transform.localScale *= 0.2f;
            usableSpots.RemoveAt(index);
        }

        if(spawnDelay > 0.3f)
        {
            spawnDelay *= 0.99f;
        }
        else
        {
            spawnDelay *= 0.999f;
        }
        
    }
}
