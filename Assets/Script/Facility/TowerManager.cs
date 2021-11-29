using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject towerBulletHolder;

    public GameObject[] redTowers;
    public GameObject[] blueTowers;

    public TowerInfo towerInfoBaseCapital;
    public TowerInfo towerInfoBaseStart;
    public TowerInfo towerInfoBaseEnd;

    public EventData towerEvent;

	private void Awake()
	{
        towerInfoBaseCapital.towerEvent = towerEvent;
        towerInfoBaseCapital.team = 0;
        redTowers[0].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital), towerBulletHolder);
        redTowers[1].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital), towerBulletHolder);
        towerInfoBaseCapital.team = 1;
        blueTowers[0].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital), towerBulletHolder);
        blueTowers[1].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital), towerBulletHolder);

        towerInfoBaseStart.towerEvent = towerEvent;
        for (int i = 2; i < 5; i++)
        {
            towerInfoBaseStart.team = 0;
            redTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseStart), towerBulletHolder);
            towerInfoBaseStart.team = 1;
            blueTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseStart), towerBulletHolder);
        }

        towerInfoBaseEnd.towerEvent = towerEvent;
        for (int i = 5; i < 8; i++)
        {
            towerInfoBaseEnd.team = 0;
            redTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseEnd), towerBulletHolder, true);
            towerInfoBaseEnd.team = 1;
            blueTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseEnd), towerBulletHolder, true);
        }
    }
}
