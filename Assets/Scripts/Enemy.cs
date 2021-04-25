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
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer childSpriteRenderer;
    private float maxDistance;

    private void Start()
    {
        startPos = transform.position;
        maxHP = hp;
        player = FindObjectOfType<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxDistance = Vector2.Distance(transform.position, player.transform.position) + 2f;
    }
    private void FixedUpdate()
    {
        Navigate();
    }
    private void Update()
    {
        transform.right = player.transform.position - transform.position;
        if (transform.position.x > player.transform.position.x)
        {
            spriteRenderer.flipY = true;
            childSpriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
            childSpriteRenderer.flipY = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player pl = collision.gameObject.GetComponent<Player>();
        if (pl)
        {
            if (!pl.immunity)
            {
                pl.TakeDamage(damage, true);
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
                pl.TakeDamage(damage, true);
                pl.immunity = true;
                Debug.Log("EnemyDamage");
                SpriteRenderer[] sprites = pl.GetComponentsInChildren<SpriteRenderer>();
                foreach (var s in sprites)
                {
                    if(s.tag != "Flashlight")
                        s.color = Color.red;
                }
            }
        }
    }
    public void Restart()
    {
        hp = maxHP;
        transform.position = startPos;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.layer = 9;
    }

    public void Death()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var rend in renderers)
        {
            rend.enabled = false;
        }
        gameObject.layer = 2;
    }
    private void Navigate()
    {
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        
        //Debug.Log("1: " + (Mathf.Log(Mathf.Abs(Mathf.Log(Mathf.Abs(maxDistance - Vector2.Distance(transform.position, player.transform.position)))))).ToString());
        //rb2d.AddForce(speed * dir * Mathf.Abs(Mathf.Log(Mathf.Abs(Mathf.Log(Mathf.Abs(maxDistance - Vector2.Distance(transform.position, player.transform.position)))))));
        rb2d.velocity = speed * dir.normalized * 35f;
    }
}
