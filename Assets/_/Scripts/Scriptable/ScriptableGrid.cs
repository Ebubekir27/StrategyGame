using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace TowerGame
{
     public  class ScriptableGrid : ScriptableObject
    {
        [SerializeField] private Node _nodePrefab;      
        [SerializeField, Range(7, 25)] private int _gridWidth = 15;
        [SerializeField, Range(7, 25)] private int _gridHeight = 15;

        public int GetGridWidth => _gridWidth;
        public int GetGridheight => _gridHeight;
        public  Dictionary<Vector2, Node> GenerateGrid()
        {
            var cells = new Dictionary<Vector2, Node>();
            var grid = new GameObject
            {
                name = "Grid"
            };
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    var cell = Instantiate(_nodePrefab, grid.transform);
                    cell.Init(new CellCoords { Pos = new Vector3(x, y) });
                    Unit unit = new Unit();
                    cell.SetUnit(unit);
                    cells.Add(new Vector2(x, y), cell);
                    
                }
            }
            Camera.main.transform.position = new Vector3(((_gridWidth - 1) * .5f) , ((_gridHeight - 1) * .5f) , -10);

            return cells;
        }
        
    }

}
public struct CellCoords : ICoords
{

    public float GetDistance(ICoords other)
    {
        var dist = new Vector2Int(Mathf.Abs((int)Pos.x - (int)other.Pos.x), Mathf.Abs((int)Pos.y - (int)other.Pos.y));

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    public Vector2 Pos { get; set; }
}