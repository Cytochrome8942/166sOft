using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject towerBulletHolder;

    public GameObject[] redTowers;
    public GameObject[] blueTowers;

    public TowerInfo towerInfoBaseFirst;
    public TowerInfo towerInfoBaseSecond;
    public TowerInfo towerInfoBaseFinal;

    public EventData towerEvent;

	private void Awake()
	{
        towerInfoBaseFirst.towerEvent = towerEvent;
        towerInfoBaseFirst.team = 0;
        redTowers[0].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseFirst), towerBulletHolder);
        towerInfoBaseFirst.team = 1;
        blueTowers[0].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseFirst), towerBulletHolder);

        towerInfoBaseSecond.towerEvent = towerEvent;
        towerInfoBaseSecond.team = 0;
        redTowers[1].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseSecond), towerBulletHolder);
        towerInfoBaseSecond.team = 1;
        blueTowers[1].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseSecond), towerBulletHolder);

        towerInfoBaseFinal.towerEvent = towerEvent;
        towerInfoBaseFinal.team = 0;
        redTowers[2].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseFinal), towerBulletHolder);
        towerInfoBaseFinal.team = 1;
        blueTowers[2].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseFinal), towerBulletHolder);
    }
}
