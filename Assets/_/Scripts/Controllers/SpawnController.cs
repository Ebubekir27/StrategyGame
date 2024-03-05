using System;
using TowerGame;
using UnityEngine;

public class SpawnController:MonoBehaviour
{
    private void OnEnable()
    {
        GridEvents.SpawnUnitRequest += OnSpawnUnitRequest;
    }
    private void OnDisable()
    {
        GridEvents.SpawnUnitRequest -= OnSpawnUnitRequest;
    }

    private void OnSpawnUnitRequest(ScriptableUnit scriptableUnit)
    {
        var newUnit = PoolController.Instance.PullFromPool(scriptableUnit.GetPrefab);
        scriptableUnit.InitUnit(newUnit);
    }
}