using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPointButtonUIController : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private BarrackInfoPanel barrackInfoPanel;
    [SerializeField] private  TextMeshProUGUI spawnPointButtonText;
    [SerializeField] private SpawnPointSpawner spawnPointSpawner;
    private bool _spawnPointAvailable;

    private void OnEnable()
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
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_spawnPointAvailable)
        {
            spawnPointSpawner.Spawn(barrackInfoPanel.GetBarrackUnit);
            barrackInfoPanel.CheckVirtual();
            _spawnPointAvailable = true;
        }
    }
}
