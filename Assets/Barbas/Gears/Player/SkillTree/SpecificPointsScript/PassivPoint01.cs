using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivPoint01 : PointScript
{
    public float damageIncreaseTower;
    
    public new void Awake()
    {
        StartForEvryPoint();
    }

    public override void Effect()
    {
        skillTreeLinkedTo.towerDamageBoost += damageIncreaseTower;
    }
}
