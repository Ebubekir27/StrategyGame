using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace StrategyGame
{
    public class Node : NodeBase
    {
        private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };
        public override void CacheNeighbors()
        {
            Neighbors = new List<NodeBase>();

            foreach (var cell in Dirs.Select(dir => GridManager.Instance.GetCellAtPosition(Coords.Pos + dir)).Where(cell => cell != null))
            {
                Neighbors.Add(cell);
            }
        }
         
    }
}