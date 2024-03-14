using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


namespace StrategyGame
{

    public class SoldierUnit : AttackableUnit
    {

        Unit unitBase;
        public Unit GetUnitBase => unitBase;
        public void SetUnitBase(Unit unit) => unitBase = unit;
        private BarrackUnit _barrackUnit;
        private Vector2 _spawnPointPos;
        private Vector2 _clickPos;
        private Unit _targetUnit;
        private Node _targetNode;

        private bool _attackMode;
        public void Init(ScriptableSoldier scriptableSoldier)
        {
            base.Init(scriptableSoldier);
            _damage = scriptableSoldier.GetDamage;
            SetBuildedState(true);
            _gridManager = GridManager.Instance;

            _barrackUnit = unitBase.GetComponent<BarrackUnit>();
            _spawnPointPos = _barrackUnit.SpawnPointPosition;
        }


        //Soldier spawn from barrack
        public void SoldierSpawn()
        {
            Node spawnNode = EmptyNodeFinder.FindEmpty(_spawnPointPos);
            if (spawnNode == null)
            {
                Destroy(gameObject);
                return;
            }
            Vector3 tempPos = spawnNode.GetCoords.Pos;
            transform.position = tempPos;
            tempPos.z = -6;
            tempPos.x = Mathf.Round(tempPos.x);
            tempPos.y = Mathf.Round(tempPos.y);
            Build();
        }


        private void OnEnable()
        {
            UnitEvents.SelectedUnits += GetSelectedUnit;
            UnitEvents.AttackStopRequest += GetAttackStopRequest;
            GridEvents.SetProductColorRequest += GetProductColorRequest;

        }
        private void OnDisable()
        {
            UnitEvents.SelectedUnits -= GetSelectedUnit;
            UnitEvents.AttackStopRequest -= GetAttackStopRequest;
            GridEvents.SetProductColorRequest -= GetProductColorRequest;

        }
        void CheckAttackMode()
        {
            Node clickNode = _gridManager.GetCellAtPosition(_clickPos);
            if (clickNode.GetUnit != null)
            {
                if (clickNode.GetUnit.GetAttackable)
                {
                    _attackMode = true;
                }
                else
                {
                    _attackMode = false;
                }
            }
            else
            {
                _attackMode = false;
            }
        }
        // listen to selected units
        private void GetSelectedUnit(List<NodeBase> nodes, Vector2 clickPos)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == _gridManager.GetCellAtPosition(CurrentCellPos()))
                {
                    _clickPos = clickPos;
                    CheckAttackMode();
                    StartCoroutine(MoveUnit());
                    break;
                }
            }
        }

        private void GetAttackStopRequest(Unit unit)
        {
            if (unit == this)
            {
                StopCoroutine(CheckArea(_targetUnit));
            }
        }

        //  Product View Color Change
        private void GetProductColorRequest(NodeBase nodebase, CellColorState color)
        {
            List<Vector2> cellPositionList = CurrentCellsPos();
            if (cellPositionList.Contains(nodebase.GetCoords.Pos))
            {
                ColorChange(color);
            }
        }
        IEnumerator MoveUnit()
        {
            _targetNode = EmptyNodeFinder.FindEmpty(_clickPos);
            NodeBase startNode = _gridManager.GetCellAtPosition(CurrentCellPos());
            NodeBase endNode = _targetNode;
            List<NodeBase> newlist = FindPathController.FindPath(startNode, endNode);
            if (startNode.GetUnit == null || startNode.GetUnit == this)
            {
                startNode.CellState = CellStateType.Empty;
                startNode.SetUnit(null);
            }
            if (newlist == null)
            {
                int waitCounter = 0;
                while (waitCounter < 5)
                {
                    yield return new WaitForSeconds(1f);
                    waitCounter++;
                    _targetNode = EmptyNodeFinder.FindEmpty(_clickPos);
                    startNode = _gridManager.GetCellAtPosition(CurrentCellPos());
                    newlist = FindPathController.FindPath(startNode, _targetNode);
                    CheckAttackMode();
                    if (newlist != null)
                    {
                        StartCoroutine(MoveUnit());
                        break;
                    }
                }
                MoveDone();
            }
            else
            {
                if (newlist.Count > 0)
                {
                    newlist[newlist.Count - 1].CellState = CellStateType.InProcces;
                    newlist[newlist.Count - 1].SetUnit(this);

                    bool loop = true;
                    while (loop)
                    {
                        if ((Vector2)transform.position == newlist[newlist.Count - 1].GetCoords.Pos)
                        {
                            transform.position = CurrentCellPos();
                            if ((Vector2)transform.position != _targetNode.GetCoords.Pos)
                            {
                                StartCoroutine(MoveUnit());
                            }
                            else
                            {
                                MoveDone();
                            }

                            break;
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, newlist[newlist.Count - 1].GetCoords.Pos, .07f);

                        }
                        yield return new WaitForEndOfFrame();
                    }
                }
                else
                {
                    MoveDone();
                }
            }
        }
        void MoveDone()
        {
            ColorChange(CellColorState.Normal);
            NodeBase currentNode = _gridManager.GetCellAtPosition(CurrentCellPos());
            currentNode.CellState = CellStateType;
            currentNode.SetUnit(this);
            Node clickNode = _gridManager.GetCellAtPosition(_clickPos);
            if (clickNode.CellState != CellStateType.Empty && _attackMode)
            {

                StartCoroutine(CheckArea(clickNode.GetUnit));
            }
        }



    }
}

