using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
 
namespace StrategyGame
{ 

public struct CellUnit
{
    public CellStateType cellState;
    public Vector2Int cellPosition;
}

public class GridData : MonoBehaviour
{   
   
    public static GridData Instance { get; private set; }

    public Dictionary<Vector2, NodeBase> Cells { get; private set; }
    
    [SerializeField] List<CellUnit> units = new List<CellUnit>();
    public List<CellUnit> Getunits => units;

    private void Awake()
    {
        if (Instance!=null)
        {
            Debug.Log("Handle me!");
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {
        GridEvents.SetCellRequest += SetCellRequest;
        GridEvents.BuildProductRequest += GetBuildProductRequest;
    }

    private void OnDisable()
    {
        GridEvents.SetCellRequest -= SetCellRequest;
        GridEvents.BuildProductRequest -= GetBuildProductRequest;
    }

  

    [ContextMenu("debug")]
    void DebugUnits()
    {
        for (int i = 0; i < units.Count; i++)
        {
            Debug.LogError(units[i].cellPosition + "\n" + units[i].cellState + "\n\n");
        }
    }

    private void SetCellRequest(int x, int y)
    {
        CellUnit newunit = new CellUnit();
        newunit.cellPosition=new Vector2Int(x, y);
        newunit.cellState=CellStateType.Empty;
        units.Add(newunit);        
    }

    private void GetBuildProductRequest(List<Vector2> productPositionList, CellStateType cellState, Unit unit)
    {
        for (int i = 0; i < productPositionList.Count; i++)
        {
            ChangesCellState(productPositionList[i], cellState);
            
        }
    }

    void ChangesCellState(Vector2 productPosition, CellStateType newState)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].cellPosition== productPosition)
            {
                CellUnit newunit = units[i];
                 newunit.cellState = newState;
                units[i] = newunit;
                break;
            }
        }
    }
}
}