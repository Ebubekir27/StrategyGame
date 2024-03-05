using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TowerGame;

public class UIEvents  
{
    public static UnityAction<BarrackUnit> RequestBarrackInfoPanel;
    public static UnityAction<PowerPlantUnit> RequestPowerPlantInfoPanel;
    public static UnityAction RequestClosePanel;
}
