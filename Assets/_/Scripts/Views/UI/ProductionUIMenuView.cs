using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionUIMenuView : MonoBehaviour
{
    [SerializeField] private UnitSpawnButton spawnerButton;
    [SerializeField] private List<ScriptableUnit> scriptableUnits;
    [SerializeField] private int itemCount = 5;
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
