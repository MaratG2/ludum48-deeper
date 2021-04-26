using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesExplosion;

    public float damage;
    public float travelTime;
    public float travelSpeed;
    private AudioSource audioSource;
    public AudioClip explodeSound;

    private float timer = 0f;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * travelSpeed * transform.localScale.x, ForceMode2D.Impulse);
        audioSource = GetComponent<AudioSource>();
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
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
        if(collision.tag != "Player" && collision.tag != "Ignore")
            Vanish();
    }

    void Vanish()
    {
        ParticleSystem ps = Instantiate(particlesExplosion, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explodeSound, transform.position);
        ps.Play();
        Destroy(gameObject);
    }
}
