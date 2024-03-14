using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
namespace StrategyGame
{
    public  class ClickListener : MonoBehaviour
    {
        public static ClickListener Instance;
        [SerializeField] private List<NodeBase> startPos;
        [SerializeField] private List<NodeBase> checkNeightborList;
        
        private GridManager _gridManager;
        

        void Awake() => Instance = this;
       
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
            
            if (Input.GetMouseButtonDown(1)&& startPos.Count>0)
            {
                Node clickNode=_gridManager.GetCellAtPosition(GetClickPos());
                if (!startPos.Contains(clickNode))
                {
                    UnitEvents.SelectedUnits?.Invoke(startPos, GetClickPos());
                    startPos.Clear();
                }
                
            }
        }

         

        //Detect mouse click position
        bool ClickGameBoardDetect()
        {
            Vector2 clickPosition = GetClickPos();
            if (_gridManager._scriptableGrid.GetGridWidth >= clickPosition.x && clickPosition.x >= 0 &&
                _gridManager._scriptableGrid.GetGridHeight >= clickPosition.y && clickPosition.y >= 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Check click which unit
        private void CheckClickCell()
        {
            Vector2 clickPosition = GetClickPos();
            if (ClickGameBoardDetect())
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
                if (clickedNodeCellState == CellStateType.Barrack)
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

        // Return mouse click position
        private Vector2 GetClickPos()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 tempPosition = mousePosition;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = Mathf.Round(tempPosition.y);
            return tempPosition;
        }
 
    }
}