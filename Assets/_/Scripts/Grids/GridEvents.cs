using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace TowerGame
{
    public class GridEvents  
    {
        public static UnityAction<int, int> SetCellRequest;
        public static UnityAction<List<Vector2>, CellStateType,Unit> BuildProductRequest;
        public static UnityAction<NodeBase, List<NodeBase>, Unit> ProductGoRequest;
        public static UnityAction<NodeBase, CellColorState> SetProductColorRequest;
        public static UnityAction NextProductGoRequest;
        public static UnityAction<ScriptableUnit> SpawnUnitRequest;
        public static UnityAction<Vector2> UnitPositionRequest;
        public static UnityAction<List<Vector2>> UnitPositionInfo;
    }
}