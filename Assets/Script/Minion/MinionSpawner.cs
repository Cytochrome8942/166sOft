using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject[] spawnPath;

    public float spawnClock = 0f;
    public float waveClock = -1f;

    public float spawnTime = 1f;
    public float waveTime = 15f;

    public int minionCount = 6;

    public GameObject minion;


    // Update is called once per frame
    void Update()
    {
        waveClock += Time.deltaTime;

        if(waveClock >= waveTime)
		{
            waveClock = 0f;
            StartCoroutine(SpawnWave());
		}
    }

    public IEnumerator SpawnWave()
	{
        for(int i=0; i<minionCount; i++)
		{
            spawnClock = 0f;
            while(spawnClock < spawnTime)
			{
                spawnClock += Time.deltaTime;
                yield return null;
			}
            var newMinion = Instantiate(minion, spawnPath[0].transform.position, transform.rotation, transform);
            newMinion.GetComponentInChildren<MinionControl>().Initialize(spawnPath);
		}
	}
}
