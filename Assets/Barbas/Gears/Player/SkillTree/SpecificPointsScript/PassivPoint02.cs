using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivPoint02 : PointScript
{
    public float damageIncreasePlayer;

    public new void Awake()
    {
        StartForEvryPoint();
    }

    public override void Effect()
    {
        skillTreeLinkedTo.playerDamageMultiplier += damageIncreasePlayer;
    }
}
