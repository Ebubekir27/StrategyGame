using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrategyGame;
[CreateAssetMenu]
public class ScriptableSoldier : ScriptableUnit
{
    [SerializeField]  float _damage;

    public float GetDamage => _damage;

   
}
