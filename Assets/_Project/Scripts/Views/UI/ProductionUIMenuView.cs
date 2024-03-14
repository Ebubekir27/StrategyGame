using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionUIMenuView : MonoBehaviour
{
    [SerializeField] private UnitSpawnButton spawnerButton;
    [SerializeField] private List<ScriptableUnit> scriptableUnits;
    [SerializeField] private GameObject layoutObje;
    [SerializeField] private int itemCount = 5;
   private int _columnCount = 2;

    private void Awake()
    {
        int tempUnitIndex=0;
        for (int j = 0; j < itemCount; j++)
        {
            var newLayoutParent = Instantiate(layoutObje, transform);
            for (int i = 0; i < _columnCount; i++)
            {
                var newSpawnButton = Instantiate(spawnerButton, newLayoutParent.transform);
                newSpawnButton.Init(scriptableUnits[tempUnitIndex]);
                tempUnitIndex++;
                if (tempUnitIndex>= scriptableUnits.Count)
                {
                    tempUnitIndex = 0;
                }

            }

        }           
    }

}
