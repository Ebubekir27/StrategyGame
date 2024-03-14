using StrategyGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpInfoView : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI hpText;
    private Unit _unit;


    public void SetUnit(Unit unit)
    {
        _unit=unit;  
    }

    private void Update()
    {
        if (_unit)
        {
            fillBar.fillAmount = _unit.GetCurrentHp / _unit.GetBaseHp;
            hpText.text = _unit.GetCurrentHp + " / " + _unit.GetBaseHp;
            if (_unit.GetCurrentHp <=0)
            {
                UIEvents.RequestClosePanel?.Invoke();
            }
        }
    }

}
