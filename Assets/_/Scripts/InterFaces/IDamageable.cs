using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrategyGame;
public  interface IDamageable 
{
    public void GetDamaged(float damageValue, Unit sender);
  
}
