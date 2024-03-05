using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using UnityEngine.EventSystems;
namespace TowerGame
{
public class MoveListener : MonoBehaviour 
{
    [SerializeField] List<NodeBase> startPos;
    [SerializeField] List<NodeBase> checkNeightborList;
    NodeBase  _targetPos;
  
    GridManager _gridManager;
 

        private void OnEnable()
        {
            GridEvents.UnitPositionInfo += GetUnitPositionInfo;
            GridEvents.NextProductGoRequest += GetNextUnitPositionInfo;
            SpawnEvents.SoldierSpawnRequest += GetSoldierSpawnRequest;
        }
        private void OnDisable()
        {
            GridEvents.UnitPositionInfo -= GetUnitPositionInfo;
            GridEvents.NextProductGoRequest -= GetNextUnitPositionInfo;
            SpawnEvents.SoldierSpawnRequest -= GetSoldierSpawnRequest;
        }
        public void GetSoldierSpawnRequest(NodeBase node,Vector2 startPosition)
        {
            for (int i = 0; i < startPos.Count; i++)
            {
                GridEvents.SetProductColorRequest?.Invoke(startPos[i], CellColorState.Normal);
            }
            Node startNode;
            _gridManager.Cells.TryGetValue(startPosition,out startNode);
            startPos.Clear();
            startPos.Add(startNode);
            checkNeightborList.Clear();
            if (!checkNeightborList.Contains(node))
            {
                checkNeightborList.Add(node);
            }
             
            GridEvents.UnitPositionRequest?.Invoke(node.GetCoords.Pos);

          CheckEmptyNeighbors(startPos[0], checkNeightborList, startPosition,null);
           
        }
        private void GetNextUnitPositionInfo()
        {
            Vector2 clickPosition = GetClickPos();
            if (_gridManager._scriptableGrid.GetGridWidth >= clickPosition.x && clickPosition.x >= 0 &&
                _gridManager._scriptableGrid.GetGridheight >= clickPosition.y && clickPosition.y >= 0
                )
            {
                if (startPos.Count > 0)
                {
                     Node clickedNode;
                    _gridManager.Cells.TryGetValue(clickPosition, out clickedNode);
                    checkNeightborList.Clear();
                    if (!checkNeightborList.Contains(clickedNode))
                    {
                        checkNeightborList.Add(clickedNode);
                    }
                    GridEvents.UnitPositionRequest?.Invoke(clickedNode.GetCoords.Pos);
                    CheckEmptyNeighbors(startPos[0], checkNeightborList, clickPosition, clickedNode.GetUnit);
               
                       

                }

            }
           

        }

        private void GetUnitPositionInfo(List<Vector2> positions)
        {
             for(int i = 0; i < positions.Count; i++)
            {
                if (!checkNeightborList.Contains(_gridManager.GetCellAtPosition(positions[i])))
                {
                    checkNeightborList.Add(_gridManager.GetCellAtPosition(positions[i]));
                }
                 
            }
          
        }

        private void Start()
    {
            _gridManager = GridManager.Instance;
            
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                CheckClickCell();
               
        }
            if (Input.GetMouseButtonDown(1) && startPos.Count>0)
            {
                GetNextUnitPositionInfo();
               
             }   
    }


        void CheckClickCell()
        {
            Vector2 clickPosition = GetClickPos();
            if (_gridManager._scriptableGrid.GetGridWidth >= clickPosition.x && clickPosition.x >= 0 &&
                _gridManager._scriptableGrid.GetGridheight >= clickPosition.y && clickPosition.y >= 0
                )
            {
                Node clickedNode;
                _gridManager.Cells.TryGetValue(clickPosition, out clickedNode);
                CellStateType clickedNodeCellState = clickedNode.CellState;
                if (clickedNodeCellState == CellStateType.Soldier)
                {
                    if (startPos.Contains(clickedNode))
                    {
                        startPos.Remove(clickedNode);

                        GridEvents.SetProductColorRequest?.Invoke(clickedNode, CellColorState.Normal);
                    }
                    else
                    {
                        startPos.Add(clickedNode);

                        GridEvents.SetProductColorRequest?.Invoke(clickedNode, CellColorState.CanMove);
                    }
                }
                else if (clickedNodeCellState == CellStateType.Barrack)
                {

                    UIEvents.RequestBarrackInfoPanel?.Invoke(clickedNode.GetUnit.gameObject.GetComponent<BarrackUnit>());
                }
                else if (clickedNodeCellState == CellStateType.PowerPlant)
                {
                    UIEvents.RequestPowerPlantInfoPanel?.Invoke(clickedNode.GetUnit.gameObject.GetComponent<PowerPlantUnit>());
                }
                else
                {
                    UIEvents.RequestClosePanel?.Invoke();
                }
            }
        }
        Vector2 GetClickPos()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 tempPosition = mousePosition;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = Mathf.Round(tempPosition.y);
            return tempPosition;
        }

        void CheckEmptyNeighbors(NodeBase startNode, List<NodeBase> checkNeightborList, Vector2 clickPos, Unit targetNode)
        {
            List<NodeBase> emptyNeightborList = new List<NodeBase>();
            List<NodeBase> NeightborList = new List<NodeBase>();
            List<NodeBase> NeightborsNeightborList = new List<NodeBase>();
            NodeBase node;

            for (int i = 0; i < checkNeightborList.Count; i++)
            {
                if (checkNeightborList[i].CellState == CellStateType.Empty || checkNeightborList[i].CellState == CellStateType.SpawnPoint)
                {
                    emptyNeightborList.Add(checkNeightborList[i]);
                }
                foreach (var neighbor in checkNeightborList[i].Neighbors)
                {
                    if (neighbor.CellState == CellStateType.Empty || neighbor.CellState == CellStateType.SpawnPoint)
                    {
                        emptyNeightborList.Add(neighbor);

                    }
                    else
                    {
                        NeightborList.Add(neighbor);
                    }

                }
            }

            if (emptyNeightborList.Count <= 0)
            {
                for (int i = 0; i < NeightborList.Count; i++)
                {
                    foreach (var neighborsneighbor in NeightborList[i].Neighbors)
                    {

                        NeightborsNeightborList.Add(neighborsneighbor);
                    }
                }

                CheckEmptyNeighbors(startNode, NeightborsNeightborList, clickPos, targetNode);
                _targetPos = null;

            }
            else
            {
                node = emptyNeightborList[0];
                for (int i = 1; i < emptyNeightborList.Count; i++)
                {
                    float distanceNode = Mathf.Abs(Vector2.Distance(node.GetCoords.Pos, clickPos));
                    float distanceEmptyNeightborList = Mathf.Abs(Vector2.Distance(emptyNeightborList[i].GetCoords.Pos, clickPos));
                    if (node.F > emptyNeightborList[i].F && distanceNode > distanceEmptyNeightborList)
                    {
                        node = emptyNeightborList[i];
                    }
                }
                _targetPos = node;
                List<NodeBase> nodeBases;
                nodeBases = FindPathController.FindPath(startPos[0], _targetPos);
                startPos.RemoveAt(0);
                GridEvents.ProductGoRequest?.Invoke(startNode, nodeBases, targetNode);


            }

        }

    }
}