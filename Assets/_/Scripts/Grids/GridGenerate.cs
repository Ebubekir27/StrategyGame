using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StrategyGame
{


public class GridGenerate : MonoBehaviour
{  
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float rowSize;
    [SerializeField] private float columnSize;
    [SerializeField] private float cellSpace=1f;
  

        //Generate Game Board Grid
    private void Start()
    {
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                GameObject grid = Instantiate(cellPrefab);
                grid.transform.parent = transform;
                grid.transform.position = new Vector3(j* cellSpace, i* cellSpace, 0);
                GridEvents.SetCellRequest?.Invoke(j,i);
            }
        }
        Camera.main.transform.position=new Vector3(((rowSize-1) *.5f)* cellSpace, ((columnSize-1) * .5f) * cellSpace, -10);
    }
}
}