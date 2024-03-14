using System.Collections;
using System.Collections.Generic;
using StrategyGame;
using UnityEngine;

public class BarrackUnit : DamageableUnit
{
    Vector2 _spawnPointPosition;
    SpawnPointUnit _spawnPointUnit;
    bool _canProduct;
    bool _spawnPointAvailable;
    public Vector2 SpawnPointPosition => _spawnPointPosition;
    public bool CanProduct=> _canProduct;
    public bool SpawnPointAvailable => _spawnPointAvailable;
    public SpawnPointUnit GetSpawnPointUnit => _spawnPointUnit;
    public void SetSpawnPointUnit(SpawnPointUnit spawnPointUnit) => _spawnPointUnit = spawnPointUnit;
    public void SetSpawnPointState(bool state) => _spawnPointAvailable = state;
    public void SetSpawnPointPosition(Vector2 position) => _spawnPointPosition = position;

    public override void Die()
    {
        if (_spawnPointAvailable)
        {
            _poolController.ReturnToPool(_spawnPointUnit.GetScriptableUnit.GetPrefab, _spawnPointUnit.gameObject);            
            _spawnPointUnit = null;
            _spawnPointAvailable = false;
            var spawnPointNode = _gridManager.GetCellAtPosition(_spawnPointPosition);
            NodeClean(spawnPointNode);
        }
          
        base.Die();
    }

}
