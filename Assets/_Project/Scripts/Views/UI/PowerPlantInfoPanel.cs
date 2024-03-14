using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI powerPlantName;
    [SerializeField] private UnitHpInfoView unitHpInfoView;
    private PowerPlantUnit _powerPlantUnit;
    public PowerPlantUnit GetPowerPlantUnit => _powerPlantUnit;
    

    public void GetPowerPlantInfoPanelRequest(PowerPlantUnit powerPlantUnit)
    {
        _powerPlantUnit = powerPlantUnit;
        unitHpInfoView.SetUnit(_powerPlantUnit);
        CheckVirtual();
    }

    public void CheckVirtual()
    {
        powerPlantName.text = _powerPlantUnit.GetName;
        
    }
}
