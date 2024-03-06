using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
 

namespace StrategyGame
{
     
    public class SoldierUnit : AttackableUnit
    {
              
        Unit unitBase;
        public Unit GetUnitBase => unitBase;
        public void SetUnitBase(Unit unit) => unitBase = unit;
        bool _spawned;
        List<NodeBase> _neighborlist =new List<NodeBase>();
        List<Vector2> _currentPositionList;
        Node _nodeSpawnPoint, _nodeUnit;
        public void Init(ScriptableSoldier scriptableSoldier)
        {
            base.Init(scriptableSoldier);
            _damage = scriptableSoldier.GetDamage;
            SetBuildedState(true);
            _gridManager = GridManager.Instance;        
        }
       
      
        //Soldier spawn from barrack
       public void SoldierSpawn()
        {
            CheckEmptyNeighbors();
            _gridManager.Cells.TryGetValue(unitBase.GetComponent<BarrackUnit>().SpawnPointPosition, out _nodeSpawnPoint);
            Vector3 tempPos = _neighborlist[0].GetCoords.Pos;
            for (int i = 0; i < _neighborlist.Count; i++)
            {
                float distemp = Mathf.Abs(Vector2.Distance(tempPos, unitBase.transform.position));
                float disneighborlist = Mathf.Abs(Vector2.Distance(_neighborlist[i].GetCoords.Pos, unitBase.transform.position));
                if (distemp > disneighborlist)
                {
                    tempPos = _neighborlist[i].GetCoords.Pos;
                }
            }
         
            transform.position = tempPos;
            tempPos.z = -6;
            tempPos.x = Mathf.Round(tempPos.x);
            tempPos.y = Mathf.Round(tempPos.y);
            SpawnEvents.SoldierSpawnRequest?.Invoke(_nodeSpawnPoint, tempPos);
        }

        //Check  empty cell  for Spawn position
        void CheckEmptyNeighbors()
        {
            _gridManager.Cells.TryGetValue(unitBase.transform.position, out _nodeUnit);

            _currentPositionList = unitBase.CurrentCellPos();
            for (int i = 0; i < _currentPositionList.Count; i++)
            {
                _gridManager.Cells.TryGetValue(_currentPositionList[i], out _nodeUnit);

                foreach (var neighbor in _nodeUnit.Neighbors.Where(t => (t.CellState == CellStateType.Empty || t.CellState == CellStateType.SpawnPoint)))
                {
                    _neighborlist.Add(neighbor);
                }
            }
         
        }
        private void OnEnable()
        {
            UnitEvents.ProductGoRequest += GetProductMoveRequest;
            GridEvents.SetProductColorRequest += GetProductColorRequest;

        }
        private void OnDisable()
        {
            UnitEvents.ProductGoRequest -= GetProductMoveRequest;
            GridEvents.SetProductColorRequest -= GetProductColorRequest;

        }

        //  Product View Color Change
        private void GetProductColorRequest(NodeBase nodebase, CellColorState color)
        {
            List<Vector2> cellPositionList = CurrentCellPos();

            if (cellPositionList.Contains(nodebase.GetCoords.Pos))
            {

                ColorChange(color);

            }
        }

        //Product Mouse Right Click Move Request
        private void GetProductMoveRequest(NodeBase startproducts, List<NodeBase> targetPath,Unit targetUnit)
        {
            Vector2 currentPosition;
            currentPosition.x = Mathf.Round(transform.position.x);
            currentPosition.y = Mathf.Round(transform.position.y);
            if (targetPath!=null)
            {
                if (targetPath.Count > 0)
                {
                    Vector3[] pathSteps = new Vector3[targetPath.Count];
                    for (int i = 0; i < pathSteps.Length; i++)
                    {
                        pathSteps[i] = targetPath[targetPath.Count - i - 1].GetCoords.Pos;
                        pathSteps[i].z = -6;
                    }

                    if (startproducts.GetCoords.Pos == currentPosition)
                    {
                        _gridManager.Cells[currentPosition].CellState = CellStateType.Empty;
                        _gridManager.Cells[targetPath[0].GetCoords.Pos].CellState = CellStateType.InProcces;
                        float speed = pathSteps.Length * .2f;
                        ColorChange(CellColorState.Normal);
                        transform.DOPath(pathSteps, speed, PathType.Linear).OnComplete(() =>
                        {
                            _gridManager.Cells[targetPath[0].GetCoords.Pos].CellState = ProductType;
                            _gridManager.Cells[targetPath[0].GetCoords.Pos].SetUnit(this);

                            //Check near area for attack
                            if (_spawned)
                            {
                                StartCoroutine(CheckArea( targetUnit));
                            }
                           
                            _spawned = true;
                        });


                        UnitEvents.NextProductGoRequest?.Invoke();
                    }
                }
            }
            else
            {
                ColorChange(CellColorState.Normal);
                UnitEvents.NextProductGoRequest?.Invoke();
            }          
        }    
    }
}

