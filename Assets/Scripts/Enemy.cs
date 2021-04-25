using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float maxHP;
    public float hp = 50f;
    public float speed = 10f;
    public float damage = 10f;
    public Vector3 startPos;

    private Player player;
    private Rigidbody2D rb2d;
    private float maxDistance;
    private void Start()
    {
        startPos = transform.position;
        maxHP = hp;
        player = FindObjectOfType<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        maxDistance = Vector2.Distance(transform.position, player.transform.position) + 2f;
    }
    private void FixedUpdate()
    {
        Navigate();
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
    private void Navigate()
    {
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        Debug.Log("1: " + (Mathf.Log(Mathf.Abs(Mathf.Log(Mathf.Abs(maxDistance - Vector2.Distance(transform.position, player.transform.position)))))).ToString());
        rb2d.AddForce(speed * dir * Mathf.Abs(Mathf.Log(Mathf.Abs(Mathf.Log(Mathf.Abs(maxDistance - Vector2.Distance(transform.position, player.transform.position)))))));
    }
}
