using System.Collections;
using System.Collections.Generic;
using TowerGame;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantSpawner : MonoBehaviour 
{
    [SerializeField] PowerPlantUnit powerPlantUnit;
    [SerializeField] ScriptablePowerPlant scriptablePowerPlant;
    PoolController _poolController;
    private void OnEnable()
    {
        _poolController = PoolController.Instance;
    }
    public void OnClickButton()
    {
        var newPowerPlant = _poolController.PullFromPool(powerPlantUnit.gameObject).GetComponent<PowerPlantUnit>();

        //var newBarrack = Instantiate(powerPlantUnit);
        newPowerPlant.Init(scriptablePowerPlant);
    }
}

