using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 50f;
    public float damage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player pl = collision.gameObject.GetComponent<Player>();
        if (pl)
        {
            if (!pl.immunity)
            {
                pl.TakeDamage(damage);
                pl.immunity = true;
                Debug.Log("EnemyDamage");
                pl.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            } 
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Player pl = collision.gameObject.GetComponent<Player>();
        if (pl)
        {
            if (!pl.immunity)
            {
                pl.TakeDamage(damage);
                pl.immunity = true;
                Debug.Log("EnemyDamage");
                pl.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public void Death()
    {

        Destroy(gameObject);
    }
}
