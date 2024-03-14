using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrategyGame;
[CreateAssetMenu(menuName = "Unit/Soldier")]
public class ScriptableSoldier : ScriptableUnit
{
    [SerializeField]  float _damage;

    public float GetDamage => _damage;

   
}
