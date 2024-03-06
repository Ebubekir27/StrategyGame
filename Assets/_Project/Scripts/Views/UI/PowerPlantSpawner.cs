using System.Collections;
using System.Collections.Generic;
using StrategyGame;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantSpawner : MonoBehaviour 
{
    [SerializeField] private PowerPlantUnit powerPlantUnit;
    [SerializeField] private ScriptablePowerPlant scriptablePowerPlant;
    private PoolController _poolController;
    private void OnEnable()
    {
        _poolController = PoolController.Instance;
    }
    public void OnClickButton()
    {
        var newPowerPlant = _poolController.PullFromPool(powerPlantUnit.gameObject).GetComponent<PowerPlantUnit>();
        newPowerPlant.Init(scriptablePowerPlant);
    }
}

