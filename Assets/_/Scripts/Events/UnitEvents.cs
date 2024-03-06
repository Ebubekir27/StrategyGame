using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace StrategyGame
{
    public class UnitEvents  
    {    
        public static UnityAction<NodeBase, List<NodeBase>, Unit> ProductGoRequest;        
        public static UnityAction NextProductGoRequest;
        public static UnityAction<ScriptableUnit> SpawnUnitRequest;
        public static UnityAction<Vector2> UnitPositionRequest;
        public static UnityAction<List<Vector2>> UnitPositionInfo;
    }
}