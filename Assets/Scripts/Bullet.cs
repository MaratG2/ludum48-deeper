using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesExplosion;

    public float damage;
    public float travelTime;
    public float travelSpeed;

    private float timer = 0f;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * travelSpeed * transform.localScale.x, ForceMode2D.Impulse);
    }
    void Update()
    {
        if (timer < travelTime)
            timer += Time.deltaTime;
        else
            Vanish();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().hp -= damage;
            if (collision.GetComponent<Enemy>().hp <= 0)
                collision.GetComponent<Enemy>().Death();
        }

        Vanish();
    }

    void Vanish()
    {
        ParticleSystem ps = Instantiate(particlesExplosion, transform.position, transform.rotation);
        ps.Play();
        Destroy(gameObject);
    }
}
