using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPointButtonUIController : MonoBehaviour 
{
    [SerializeField] private BarrackInfoPanel barrackInfoPanel;
    [SerializeField] private  TextMeshProUGUI spawnPointButtonText;
    [SerializeField] private SpawnPointSpawner spawnPointSpawner;
    private bool _spawnPointAvailable;

    private void Update()
    {
        _spawnPointAvailable = barrackInfoPanel.GetBarrackUnit.SpawnPointAvailable;

        if (!_spawnPointAvailable)
        {
            transform.localScale = Vector3.one;
            spawnPointButtonText.text = "SetPoint";
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
        barrackInfoPanel.CheckVirtual();

    }

    public void OnClickButton()
    {
        if (!_spawnPointAvailable)
        {
            spawnPointSpawner.Spawn(barrackInfoPanel.GetBarrackUnit);
            
        }
    }
}
