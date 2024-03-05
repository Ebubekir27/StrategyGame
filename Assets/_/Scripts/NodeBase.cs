using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

namespace TowerGame
{
    public abstract class NodeBase : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _renderer;
        public List<NodeBase> Neighbors;
        public NodeBase Connection;
        public CellStateType CellState;
        public Unit Unitdata;
        public Unit GetUnit => Unitdata;
        public float G { get; private set; }
        public float H { get; private set; }
        public float F => G + H;
        public ICoords Coords;
        public ICoords GetCoords => Coords;
        public  abstract void CacheNeighbors();
        public float GetDistance(NodeBase other) => Coords.GetDistance(other.Coords);         
        public void SetConnection(NodeBase nodeBase)=>Connection = nodeBase;      
        public void SetUnit(Unit unitdata) => Unitdata = unitdata;
        public void SetG(float g) => G = g;
        public void SetH(float h) => H = h;
        public void SetCellStateType(CellStateType cellStateType) => CellState = cellStateType;

        public virtual void Init(ICoords coords)
        {                      
            Coords = coords;
            Vector3 vector3 = Coords.Pos;
            vector3.z = 5;
            transform.position = vector3;
            CellState = CellStateType.Empty;
        }

    }
}
public interface ICoords
{
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}
