using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
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
        redTowers[0].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital));
        redTowers[1].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital));
        towerInfoBaseCapital.team = 1;
        blueTowers[0].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital));
        blueTowers[1].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseCapital));

        towerInfoBaseStart.towerEvent = towerEvent;
        for (int i = 2; i < 5; i++)
        {
            towerInfoBaseStart.team = 0;
            redTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseStart));
            towerInfoBaseStart.team = 1;
            blueTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseStart));
        }

        towerInfoBaseEnd.towerEvent = towerEvent;
        for (int i = 5; i < 8; i++)
        {
            towerInfoBaseEnd.team = 0;
            redTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseEnd), true);
            towerInfoBaseEnd.team = 1;
            blueTowers[i].GetComponentInChildren<TowerControl>().Initialize(Instantiate(towerInfoBaseEnd), true);
        }
    }
}
