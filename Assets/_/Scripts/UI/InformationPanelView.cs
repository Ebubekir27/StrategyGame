using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPanelView : MonoBehaviour
{
    [SerializeField] BarrackInfoPanel barrackPanel;
    [SerializeField] PowerPlantInfoPanel powerPlantPanel;
    private void OnEnable()
    {
        UIEvents.RequestBarrackInfoPanel += GetBarrackInfoPanelRequest;
        UIEvents.RequestPowerPlantInfoPanel += GetPowerPlantInfoPanelRequest;
        UIEvents.RequestClosePanel += GetClosePanelRequest;
    } 

    private void OnDisable()
    {
        UIEvents.RequestBarrackInfoPanel -= GetBarrackInfoPanelRequest;
        UIEvents.RequestPowerPlantInfoPanel -= GetPowerPlantInfoPanelRequest;
        UIEvents.RequestClosePanel -= GetClosePanelRequest;
    }

    private void GetPowerPlantInfoPanelRequest(PowerPlantUnit powerPlantUnit)
    {
        GetClosePanelRequest();
        powerPlantPanel.gameObject.SetActive(true);
        powerPlantPanel.GetPowerPlantInfoPanelRequest(powerPlantUnit);
    }

    private void GetClosePanelRequest()
    {
        barrackPanel.gameObject.SetActive(false);
        powerPlantPanel.gameObject.SetActive(false);
    }
    private void GetBarrackInfoPanelRequest(BarrackUnit barrackUnit)
    {
        GetClosePanelRequest();
        barrackPanel.gameObject.SetActive(true);
        barrackPanel.GetBarrackInfoPanelRequest(barrackUnit);
    }

   
}
