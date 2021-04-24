using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0f, 100f)] [SerializeField] private float[] maxHP;
    [Tooltip("Don't change")] [SerializeField] private float hp = 100f; //
    [Range(0, 3)] public int upgradeTier = 0;
    [SerializeField] private float immuneTime = 1f;
    private float immuneTimer = 0f;
    [HideInInspector] public bool immunity;
    [SerializeField] private float depth = 0f;
    [SerializeField] private float maxDepth = 10f;
    [SerializeField] private float depthDamage = 10f;
    [SerializeField] private float depthTime = 1f;
    private float depthTimer = 0f;
    private float startY = 0f;

    void Awake()
    {
        hp = maxHP[upgradeTier];
        //startY = transform.position.y;
    }
    void Update()
    {
        depth = transform.position.y - startY;
        if (depth < -maxDepth)
        {
            if (depthTimer < depthTime)
                depthTimer += Time.deltaTime;
            else
            {
                TakeDamage(depthDamage);
                Debug.Log("DepthDamage");
                depthTimer = 0f;
            }
        }
        else
            depthTimer = 0f;

        if(immunity)
        {
            if (immuneTimer < immuneTime)
                immuneTimer += Time.deltaTime;
            else
            {
                immunity = false;
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
                immuneTimer = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
        }
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;

        hp -= damage;

        if(hp <= 0)
            StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        hp = 0;
        Destroy(gameObject);
        yield return null;
    }
}
