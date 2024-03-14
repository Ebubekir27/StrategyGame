using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace StrategyGame
{
    public class UnitEvents  
    {    
        public static UnityAction<NodeBase, List<NodeBase>, Unit,int> ProductGoRequest;        
        public static UnityAction NextProductGoRequest;
        public static UnityAction<Unit> AttackStopRequest;
        public static UnityAction<ScriptableUnit> SpawnUnitRequest;
        public static UnityAction<Vector2> UnitPositionRequest;
        public static UnityAction<List<Vector2>> UnitPositionInfo;
        public static UnityAction<List<NodeBase>,Vector2> SelectedUnits;
    }
}