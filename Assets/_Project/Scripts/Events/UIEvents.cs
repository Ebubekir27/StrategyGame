using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using StrategyGame;

public class UIEvents  
{
    public static UnityAction<BarrackUnit> RequestBarrackInfoPanel;
    public static UnityAction<PowerPlantUnit> RequestPowerPlantInfoPanel;
    public static UnityAction RequestClosePanel;
}
