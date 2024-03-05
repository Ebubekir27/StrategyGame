using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerGame;
[CreateAssetMenu]
public class ScriptableSoldier : ScriptableUnit
{
    [SerializeField]  float _damage;

    public float GetDamage => _damage;

   
}
