using System;
using StrategyGame;
using UnityEngine;

public class SpawnController:MonoBehaviour
{
    private void OnEnable()
    {
        UnitEvents.SpawnUnitRequest += OnSpawnUnitRequest;
    }
    private void OnDisable()
    {
        UnitEvents.SpawnUnitRequest -= OnSpawnUnitRequest;
    }

    private void OnSpawnUnitRequest(ScriptableUnit scriptableUnit)
    {
        var newUnit = PoolController.Instance.PullFromPool(scriptableUnit.GetPrefab);
        scriptableUnit.InitUnit(newUnit);
    }
}