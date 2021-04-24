using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0f, 1000f)] [SerializeField] private float[] maxHP;
    [Tooltip("Don't change")] [SerializeField] private float hp = 100f; //
    [Range(0, 3)] public int upgradeTier = 0;
 
    void Awake()
    {
        hp = maxHP[upgradeTier];
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
