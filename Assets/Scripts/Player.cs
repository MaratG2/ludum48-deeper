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
    public int[] crystalls;
    public bool isHarvesting;
    public PlayerWeapon weapon;
    public Crystal crystal;
    [SerializeField] private float immuneTime = 1f;
    private float immuneTimer = 0f;
    [HideInInspector] public bool immunity;
    public int depth = 0;
    [SerializeField] private int[] maxDepth;
    [SerializeField] private float depthDamage = 10f;
    [SerializeField] private float depthTime = 1f;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI textDepth;
    [SerializeField] private TextMeshProUGUI textDepthCounter;
    [SerializeField] private TextMeshProUGUI[] resQs;
    public Vector3 savePos;
    private float depthTimer = 0f;
    private float startY = 0f;
   

    void Awake()
    {
        hp = maxHP[upgradeTierHealth];
        startY = transform.position.y;
    }
    void Start()
    {
        savePos = transform.position;
    }
    void Update()
    {
        depth = -Mathf.FloorToInt((transform.position.y - startY) * 10);
        if (depth < 0)
            depth = 0;
        textDepthCounter.text = depth.ToString();
        if (depth > maxDepth[upgradeTierDepth])
        {
            if (depthTimer < depthTime)
                depthTimer += Time.deltaTime;
            else
            {
                TakeDamage(depthDamage, true);
                textDepth.gameObject.SetActive(true);
                Debug.Log("DepthDamage");
                depthTimer = 0f;
            }
        }
        else
        {
            depthTimer = 0f;
            textDepth.gameObject.SetActive(false);
        }

        if(immunity)
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

        hpBar.fillAmount = hp / maxHP[upgradeTierHealth];

        if(Input.GetKeyDown(KeyCode.R))
            TakeDamage(1000f, false);

        for(int i = 0; i < 3; i++)
        {
            resQs[i].text = crystalls[i].ToString();
            if (crystalls[i] != 0)
                resQs[i].transform.parent.gameObject.SetActive(true);
            else
                resQs[i].transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
        }
    }

    public void TakeDamage(float damage, bool death)
    {
        if (hp <= 0) return;

        hp -= damage;

        if(hp <= 0)
            StartCoroutine(Death(death));
    }

    private void Restart(bool death)
    {
        hp = maxHP[upgradeTierHealth];
        transform.position = savePos;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            //enemy.Restart();
        }
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
        }
    }

    private IEnumerator Death(bool death)
    {
        Restart(death);
        yield return null;
    }
}
