using System.Collections;
using System.Collections.Generic;
using TowerGame;
using UnityEngine;

public class ScriptableUnit : ScriptableObject
{
    [SerializeField]  CellStateType _productType;
    [SerializeField]  GameObject _prefab;
    [SerializeField]  Sprite _sprite;
    [SerializeField]  string _name;
    [SerializeField]  int _width;
    [SerializeField]  int _height;
    [SerializeField]  float _hp;
    public CellStateType GetProductType => _productType;
    public GameObject GetPrefab => _prefab;
    public Sprite GetSprite=> _sprite;
    public string GetName => _name;
    public int GetWidth => _width;
    public int Getheight => _height;
    public float GetHp => _hp;


    public virtual void InitUnit(GameObject unit)
    {
        unit.GetComponent<Unit>().Init(this);
    }
}
