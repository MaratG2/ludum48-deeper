using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float maxHP;
    public float hp = 50f;
    
    public float damage = 10f;
    public Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        maxHP = hp;
    }

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
    public void Restart()
    {
        hp = maxHP;
        transform.position = startPos;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void Death()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
