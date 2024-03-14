using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrategyGame;
public  class DamageableUnit : Unit, IDamageable 
{
    protected PoolController _poolController;
    private void Start()
    {
        _poolController=PoolController.Instance;
        _attackable = true;
    }

    // Get Damaged Unit method
    public  void GetDamaged(float damageValue, Unit sender)
    {
        _currentHp -= damageValue;
        if (_currentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(UnitVisualHit());
        }

    }
    // Node Reset
    public void NodeClean(Node node)
    {
        node.CellState = CellStateType.Empty;
        if (node.GetUnit!=null)
        {
            node.GetUnit.SetBuildedState(false);
            node.SetUnit(null);
        }
        
    }

    //unit Die method
    public virtual void Die()
    {
        List<Vector2> cellPositionList = CurrentCellsPos();
        for (int i = 0; i < cellPositionList.Count; i++)
        {
            var node = _gridManager.GetCellAtPosition(cellPositionList[i]);
            NodeClean(node);

        }
        _poolController.ReturnToPool(_scriptableUnit.GetPrefab, gameObject);
         
    }


}
