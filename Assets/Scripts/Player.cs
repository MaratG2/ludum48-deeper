using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Range(0f, 100f)] [SerializeField] private float[] maxHP;
    [Tooltip("Don't change")] [SerializeField] private float hp = 100f; //
    [Range(0, 4)] public int upgradeTierHealth = 0;
    [Range(0, 5)] public int upgradeTierDepth = 0;
    public bool isFlashlightOn;
    public Transform lasetPoint;
    [SerializeField] private GameObject flashlightObject;
    public float knockbackForce = 8000f;
    public int[] crystalls;
    public bool isHarvesting;
    public PlayerWeapon weapon;
    public Crystal crystal;
    [SerializeField] private float immuneTime = 1f;
    private float immuneTimer = 0f;
    [HideInInspector] public bool immunity;
    private bool wasImmunity;
    public int depth = 0;
    [SerializeField] private int[] maxDepth;
    [SerializeField] private float depthDamage = 10f;
    [SerializeField] private float depthTime = 1f;
    [SerializeField] private Image hpBar;
    public Image deathImage;
    [SerializeField] private TextMeshProUGUI textDepth;
    [SerializeField] private TextMeshProUGUI textDepthCounter;
    [SerializeField] private TextMeshProUGUI[] resQs;
    public Vector3 savePos;
    private float depthTimer = 0f;
    private float startY = 0f;
    public float deathScreenLong = 0.5f;
    public float deathScreenFadeMulti = 1f;
    [Header("Audio")]
    public AudioSource moveAudioSource;
    public AudioSource damageAudioSource;
    public AudioClip[] takeDamageSound;
    public AudioClip collideSound;
    public AudioClip[] randomSounds;
    public float depthRandomSounds = 350f;
    public float minTimeRandomSound = 10f;
    public float maxTimeRandomSound = 30f;
    private float timerRandomSound = 0f;
    private float timeRandomSound = 0f;
    private bool setTimer;
    private Animator animator;
    public bool inCr;

    void Awake()
    {
        hp = maxHP[upgradeTierHealth];
        startY = transform.position.y;
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        savePos = transform.position;
    }
    void Update()   
    {
        depth = -Mathf.FloorToInt((transform.position.y - startY) * 2);
        if (depth < 0)
            depth = 0;

        //FLASHLIGHT
        if (isFlashlightOn)
            flashlightObject.SetActive(true);
        else
            flashlightObject.SetActive(false);

        //DEPTH
        textDepthCounter.text = depth.ToString();
        if (depth > maxDepth[upgradeTierDepth])
        {
            textDepth.gameObject.SetActive(true);
            if (depthTimer < depthTime)
                depthTimer += Time.deltaTime;
            else
            {
                TakeDamage(depthDamage, true);
                StartCoroutine(PushBack());    
                Debug.Log("DepthDamage");
                depthTimer = 0f;
            }
        }
        else
        {
            depthTimer = 0f;
            textDepth.gameObject.SetActive(false);
        }

        

        //ANIMS
        if (animator.GetFloat("speed") > 0 && !moveAudioSource.isPlaying)
            moveAudioSource.Play();
        else if(animator.GetFloat("speed") == 0)
            moveAudioSource.Pause();

        //IMMUNITY
        if (immunity)
        {
            if (immuneTimer < immuneTime)
                immuneTimer += Time.deltaTime;
            else
            {
                immunity = false;
                SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
                foreach (var s in sprites)
                {
                    if (s.tag != "Flashlight") 
                        s.color = Color.white;
                }
                immuneTimer = 0f;
            }
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //    TakeDamage(1000, false);

        //UI
        hpBar.fillAmount = hp / maxHP[upgradeTierHealth];
        for(int i = 0; i < 3; i++)
        {
            resQs[i].text = crystalls[i].ToString();
            if (crystalls[i] != 0)
                resQs[i].transform.parent.gameObject.SetActive(true);
            else
                resQs[i].transform.parent.gameObject.SetActive(false);
        }

        //POST
        wasImmunity = immunity;

        //RANDOM SOUNDS
        if (depth >= depthRandomSounds)
        {
            if (!setTimer)
            {
                timerRandomSound = Random.Range(minTimeRandomSound, maxTimeRandomSound);
                setTimer = true;
            }
            else
            {
                if (timeRandomSound < timerRandomSound)
                    timeRandomSound += Time.deltaTime;
                else
                {
                    if (randomSounds.Length != 0)
                        damageAudioSource.PlayOneShot(randomSounds[Mathf.FloorToInt(Random.Range(0f, randomSounds.Length - 0.01f))]);

                    timeRandomSound = 0f;
                    setTimer = false;
                }
            }
        }
    }

    private IEnumerator PushBack()
    {
        GetComponent<PlayerController>().movementBlock = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(knockbackForce * Vector2.up, ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<PlayerController>().movementBlock = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Enemy")
            moveAudioSource.PlayOneShot(collideSound);
    }

    public void TakeDamage(float damage, bool death)
    {
        if(death)
            damageAudioSource.PlayOneShot(takeDamageSound[Mathf.FloorToInt(Random.Range(0f, takeDamageSound.Length - 0.01f))]);
        
        hp -= damage;

        if (hp <= 0 && !inCr)
        {
            StopAllCoroutines();
            StartCoroutine(Death(death));
        }
    }

    private void Restart(bool death)
    {
        hp = maxHP[upgradeTierHealth];
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var s in sprites)
        {
            if (s.tag != "Flashlight")
                s.color = Color.white;
        }
        transform.position = savePos;
        FindObjectOfType<GameManager>().timerSpawn = 0f;
        FindObjectOfType<GameManager>().isRandomedTimer = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if(death)
        {
            Crystal[] cs = FindObjectsOfType<Crystal>();
            foreach (var c in cs)
            {
                if (c.resetting)
                    c.Reset();
            }
            for (int i = 0; i < 3; i++)
            {
                crystalls[i] = 0;
            }
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    private IEnumerator Death(bool death)
    {
        Restart(death);

        if (!death)
            yield return null;
        else
        {
            inCr = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            moveAudioSource.mute = true;
            deathImage.color = new Color(deathImage.color.r, deathImage.color.g, deathImage.color.b, 1);
            Time.timeScale = 0f;

            yield return new WaitForSecondsRealtime(deathScreenLong);
            Time.timeScale = 1f;
            moveAudioSource.mute = false;
            inCr = false;
            while (deathImage.color.a != 0)
            {
                deathImage.color = new Color(deathImage.color.r, deathImage.color.g, deathImage.color.b, deathImage.color.a - 0.004f * deathScreenFadeMulti);
                yield return new WaitForSecondsRealtime(0.016f);
            }
        }
    }
}
