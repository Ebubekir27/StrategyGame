using System.Collections;
using System.Collections.Generic;
using StrategyGame;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPointSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPointUnit spawnPointUnit;
    [SerializeField] private ScriptableSpawnPoint spawnPointConfig;
    private PoolController _poolController;
    private void OnEnable()
    {
        _poolController = PoolController.Instance;
    }
    public void Spawn( BarrackUnit barrackUnit)
    {
        var newSpawnPoint = _poolController.PullFromPool(spawnPointUnit.gameObject).GetComponent<SpawnPointUnit>();
        newSpawnPoint.Init(spawnPointConfig);
        barrackUnit.SetSpawnPointState(true);
        newSpawnPoint.SetUnitBase(barrackUnit);
        barrackUnit.SetSpawnPointUnit(newSpawnPoint);
    }
     
}
