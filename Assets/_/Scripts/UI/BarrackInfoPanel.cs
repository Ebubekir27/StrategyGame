using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarrackInfoPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI barrackName;
  
    [SerializeField] List<Image> soldierSpawnButtonImages;

    BarrackUnit _barrackUnit;
    public BarrackUnit GetBarrackUnit => _barrackUnit;
    

    public void GetBarrackInfoPanelRequest(BarrackUnit barrackUnit)
    {
        _barrackUnit = barrackUnit;
        CheckVirtual();
    }

   public void CheckVirtual()
    {
        barrackName.text = _barrackUnit.GetName;
        if (!_barrackUnit.SpawnPointAvailable)
        {
            for (int i = 0; i < soldierSpawnButtonImages.Count; i++)
            {
                soldierSpawnButtonImages[i].color = Color.grey;
            }
         
        }
        else
        {
            for (int i = 0; i < soldierSpawnButtonImages.Count; i++)
            {
                soldierSpawnButtonImages[i].color = Color.green;
            }
            
        }
    }
}
