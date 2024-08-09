using System.Collections;
using System.Collections.Generic;
using COHENLI.DefenseBasic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class Weapon : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag(Const.ENEMY_TAG))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if(enemy)
                {
                    enemy.Die();
                }
            }
        }
    }
}

