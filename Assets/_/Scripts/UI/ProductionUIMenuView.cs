using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionUIMenuView : MonoBehaviour
{
    [SerializeField] UnitSpawnButton spawnerButton;
    [SerializeField] List<ScriptableUnit> scriptableUnits;
    [SerializeField] int itemCount = 5;
    private void Awake()
    {
        for (int j = 0; j < itemCount; j++)
        {
            for (int i = 0; i < scriptableUnits.Count; i++)
            {
                var newSpawnButton = Instantiate(spawnerButton, transform);
                newSpawnButton.Init(scriptableUnits[i]);
            }
        }           
    }

}
