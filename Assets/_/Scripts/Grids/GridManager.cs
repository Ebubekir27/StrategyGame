using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StrategyGame
{ 
public class GridManager : MonoBehaviour
{
        public static GridManager Instance;

        [SerializeField] public ScriptableGrid _scriptableGrid;
        public Dictionary<Vector2, Node> Cells;
        public Node GetCellAtPosition(Vector2 pos) => Cells.TryGetValue(pos, out var tile) ? tile : null;

        void Awake() => Instance = this;

        private void Start()
        {
            Cells = _scriptableGrid.GenerateGrid();

            foreach (var cell in Cells.Values) cell.CacheNeighbors();

        
        }
        private void OnEnable()
        {
            GridEvents.BuildProductRequest += ChangeNodeState;
        }
        private void OnDisable()
        {
            GridEvents.BuildProductRequest -= ChangeNodeState;
        }
        public void ChangeNodeState(List<Vector2> vector2s, CellStateType cellStateType,Unit unit)
        {
            for (int i = 0; i < vector2s.Count; i++)
            {
                Cells[vector2s[i]].SetCellStateType(cellStateType);
                Cells[vector2s[i]].SetUnit(unit);
                if (cellStateType==CellStateType.SpawnPoint)
                {
                    unit.gameObject.GetComponent<SpawnPointUnit>().GetUnitBase.gameObject.GetComponent<BarrackUnit>().SetSpawnPointPosition(vector2s[i]);
                }
            }
        }
        [ContextMenu("show")]
        void ShowCells()
        {
            foreach (var cell in Cells)
            {
                Debug.LogError(cell.Key.x+","+ cell.Key.y + cell.Value.CellState.ToString() );

            }

        }
      
    }
}