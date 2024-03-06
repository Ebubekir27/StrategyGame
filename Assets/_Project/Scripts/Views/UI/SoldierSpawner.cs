using System.Collections;
using System.Collections.Generic;
using StrategyGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoldierSpawner : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] BarrackInfoPanel barrackInfoPanel;
    [SerializeField] SoldierUnit soldierUnit;
    [SerializeField] ScriptableSoldier soldierConfig;
    PoolController _poolController;
    float _clickDelay=.2f;
    bool _clicked;
    private void OnEnable()
    {
        _poolController = PoolController.Instance;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(ClickCheck());
    }

    IEnumerator ClickCheck()
    {
        if (!_clicked && barrackInfoPanel.GetBarrackUnit.SpawnPointAvailable)
        {
            var newSoldier=_poolController.PullFromPool(soldierUnit.gameObject).GetComponent<SoldierUnit>();         
            newSoldier.Init(soldierConfig);

           
            newSoldier.SetUnitBase(barrackInfoPanel.GetBarrackUnit);
            newSoldier.SoldierSpawn();
            _clicked = true;
            yield return new WaitForSeconds(_clickDelay);
            _clicked = false;
        }
       
    }
}
 