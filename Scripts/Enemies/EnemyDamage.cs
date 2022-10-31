using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //waarde
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //We controleren of de enemy gebotst is tegen de speler
        if (collision.tag == "Player")
            //We verlagen de health van de player door de schade die hij oploopt van de enemy
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}