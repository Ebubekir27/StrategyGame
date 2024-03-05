using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerGame;
public  class DamageableUnit : Unit, IDamageable 
{
    PoolController _poolController;
    private void Start()
    {
        _poolController=PoolController.Instance;    
    }
    public  void GetDamaged(float damageValue, Unit sender)
    {
        _hp -= damageValue;
        if (_hp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(UnitVisualHit());

        }

    }
    public void NodeClean(Node node)
    {
        node.CellState = CellStateType.Empty;

        node.SetUnit(null);
    }
    public virtual void Die()
    {
        List<Vector2> cellPositionList = CurrentCellPos();
        for (int i = 0; i < cellPositionList.Count; i++)
        {
            var node = _gridManager.GetCellAtPosition(cellPositionList[i]);
            NodeClean(node);

        }
        _poolController.ReturnToPool(_scriptableUnit.GetPrefab, gameObject);
         
    }


}
