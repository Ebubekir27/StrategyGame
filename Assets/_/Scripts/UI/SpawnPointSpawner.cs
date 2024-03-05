using System.Collections;
using System.Collections.Generic;
using TowerGame;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPointSpawner : MonoBehaviour
{
    [SerializeField] SpawnPointUnit SpawnPointUnit;
    [SerializeField] ScriptableSpawnPoint SpawnPointConfig;
    PoolController _poolController;
    private void OnEnable()
    {
        _poolController = PoolController.Instance;
    }
    public void Spawn( BarrackUnit barrackUnit)
    {
        var newSpawnPoint = _poolController.PullFromPool(SpawnPointUnit.gameObject).GetComponent<SpawnPointUnit>();

        //var newSpawnPoint = Instantiate(SpawnPointUnit);
        newSpawnPoint.Init(SpawnPointConfig);
        newSpawnPoint.SetUnitBase(barrackUnit);
        barrackUnit.SetSpawnPointUnit(newSpawnPoint);
    }
}
