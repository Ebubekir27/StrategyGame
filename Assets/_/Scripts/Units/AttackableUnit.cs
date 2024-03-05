using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerGame;
public class AttackableUnit : DamageableUnit, IAttackable
{
    protected float _damage; 
   
    public IEnumerator CheckArea(Unit targetUnit)
    {

        NodeBase currentNode;
        Vector2 tempPos = transform.position;
        tempPos.x = Mathf.Round(tempPos.x);
        tempPos.y = Mathf.Round(tempPos.y);
        currentNode = _gridManager.GetCellAtPosition(tempPos);

        if (currentNode.Neighbors.Count > 0)
        {

            foreach (Node neighbor in currentNode.Neighbors)
            {
                if (neighbor.GetUnit != null)
                {
                    IDamageable damageable = neighbor.GetUnit.GetComponent<IDamageable>();
                    if (damageable != null && neighbor.GetUnit == targetUnit && targetUnit!=this)
                    {

                        damageable.GetDamaged(_damage, neighbor.GetUnit);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(CheckArea(targetUnit));

                        break;
                    }
                }
            }
        }


    }
}
