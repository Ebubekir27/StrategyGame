using System.Collections;
using System.Collections.Generic;
using StrategyGame;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoldierSpawner : MonoBehaviour 
{
    [SerializeField] BarrackInfoPanel barrackInfoPanel;
    [SerializeField] SoldierUnit soldierUnit;
    [SerializeField] ScriptableSoldier soldierConfig;
    [SerializeField] TextMeshProUGUI soldierHpText;
    [SerializeField] TextMeshProUGUI soldierDamageText;
    PoolController _poolController;
    GridManager _gridManager;
    private WaitForSeconds _waitForSeconds;
    float _clickDelay=.2f;
    bool _clicked;
    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_clickDelay);
    }
    private void OnEnable()
    {
        _poolController = PoolController.Instance;
        _gridManager = GridManager.Instance;
        soldierHpText.text = "Hp: " + soldierConfig.GetHp;
        soldierDamageText.text = "Damage:" + soldierConfig.GetDamage;
    }
    public void OnClickButton()
    {
        StartCoroutine(ClickCheck());
    }

    IEnumerator ClickCheck()
    {
        if (!_clicked && barrackInfoPanel.GetBarrackUnit.SpawnPointAvailable)
        {
            bool checkEmpty = false;
            foreach (var cell in _gridManager.Cells)
            {
                if (cell.Value.CellState==CellStateType.Empty)
                {
                    checkEmpty = true;
                    break;
                }               
            }
            if (checkEmpty)
            {
                var newSoldier = _poolController.PullFromPool(soldierUnit.gameObject).GetComponent<SoldierUnit>();
                newSoldier.SetUnitBase(barrackInfoPanel.GetBarrackUnit);
                newSoldier.Init(soldierConfig);
                newSoldier.SoldierSpawn();

            }

            _clicked = true;
            yield return _waitForSeconds;
            _clicked = false;
        }
       
    }
}
 