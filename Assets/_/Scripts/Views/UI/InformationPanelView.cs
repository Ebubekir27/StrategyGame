using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPanelView : MonoBehaviour
{
    [SerializeField] private BarrackInfoPanel barrackPanel;
    [SerializeField] private PowerPlantInfoPanel powerPlantPanel;
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
        barrackPanel.GetBarrackInfoPanelRequest(barrackUnit);
        barrackPanel.gameObject.SetActive(true);
    }

   
}
