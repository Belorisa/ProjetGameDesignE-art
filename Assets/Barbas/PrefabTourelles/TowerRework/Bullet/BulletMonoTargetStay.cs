using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMonoTargetStay : BulletMonoTaget
{
    void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log("ColFromBulletStay");
        if (col.gameObject.CompareTag("Enemy"))
        {
            myStats.effect.OnCollision(col);
        }
    }
}
