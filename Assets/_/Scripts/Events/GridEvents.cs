using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace StrategyGame
{
    public class GridEvents  
    {
        public static UnityAction<int, int> SetCellRequest;
        public static UnityAction<List<Vector2>, CellStateType,Unit> BuildProductRequest;    
        public static UnityAction<NodeBase, CellColorState> SetProductColorRequest;
      
    }
}