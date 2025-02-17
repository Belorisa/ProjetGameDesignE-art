using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMonoTaget : BulletV2
{
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("ColFromBulletEnter");
        if (col.gameObject.CompareTag("Enemy"))
        {
            myStats.effect.OnCollision(col);
        }
    }
}
