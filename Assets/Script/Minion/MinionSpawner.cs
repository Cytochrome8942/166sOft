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

    private int minionCount = 7;

    public GameObject minionMelee;
    public GameObject minionSiege;
    public GameObject minionCaster;

    public GameObject casterBulletHolder;
    public GameObject siegeBulletHolder;

    public int team;


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
            GameObject newMinion = null;
            if (i < 3)
            {
                newMinion = Instantiate(minionMelee, spawnPath[0].transform.position, transform.rotation, transform);
            }
            else if(i == 3)
            {
                newMinion = Instantiate(minionSiege, spawnPath[0].transform.position, transform.rotation, transform);
                newMinion.GetComponentInChildren<MinionSiegeAttack>().siegeBulletHolder = siegeBulletHolder;
            }
			else
            {
                newMinion = Instantiate(minionCaster, spawnPath[0].transform.position, transform.rotation, transform);
                newMinion.GetComponentInChildren<MinionCasterAttack>().casterBulletHolder = casterBulletHolder;
            }
            newMinion.GetComponentInChildren<MinionControl>().Initialize(spawnPath, team);
        }
	}
}
