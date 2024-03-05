using TowerGame;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnButton : MonoBehaviour
{
    [SerializeField] Image image;
    private Button _button;
    private ScriptableUnit _scriptableUnit;
    private void Awake()
    {
        _button=GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
    }
    public void Init(ScriptableUnit scriptableUnit)
    {
        _scriptableUnit = scriptableUnit;
        image.sprite = _scriptableUnit.GetSprite;
        
    }
    private void OnClickButton()
    {
        GridEvents.SpawnUnitRequest?.Invoke(_scriptableUnit);
    }

}