using System.Collections;
using System.Collections.Generic;
using StrategyGame;
using UnityEngine;


public class BarrackSpawner : MonoBehaviour
{
    [SerializeField] BarrackUnit barrackUnit;
    [SerializeField] ScriptableBarrack scriptableBarrack;
    PoolController _poolController;
    private void OnEnable()
    {
        _poolController = PoolController.Instance;
    }
    public void OnClickButton()
    {
        var newBarrack = _poolController.PullFromPool(barrackUnit.gameObject).GetComponent<BarrackUnit>();
         newBarrack.Init(scriptableBarrack);
       
    }
}
