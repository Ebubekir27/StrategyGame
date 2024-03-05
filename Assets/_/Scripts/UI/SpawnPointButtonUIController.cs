using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPointButtonUIController : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] BarrackInfoPanel barrackInfoPanel;
    [SerializeField] TextMeshProUGUI spawnPointButtonText;
    [SerializeField] SpawnPointSpawner spawnPointSpawner;
    bool _spawnPointAvailable;
   
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
            spawnPointButtonText.text = "ChangePoint";
        }
      
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_spawnPointAvailable)
        {
            spawnPointSpawner.Spawn(barrackInfoPanel.GetBarrackUnit);
            barrackInfoPanel.GetBarrackUnit.SetSpawnPointState(true);
            barrackInfoPanel.CheckVirtual();
            _spawnPointAvailable = true;
        }
    }
}
