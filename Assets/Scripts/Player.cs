using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Range(0f, 100f)] [SerializeField] private float[] maxHP;
    [Tooltip("Don't change")] [SerializeField] private float hp = 100f; //
    [Range(0, 3)] public int upgradeTier = 0;
    public int[] crystalls;
    [SerializeField] private float immuneTime = 1f;
    private float immuneTimer = 0f;
    [HideInInspector] public bool immunity;
    [SerializeField] private int depth = 0;
    [SerializeField] private int maxDepth = 10;
    [SerializeField] private int depthMulti = 10;
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
        hp = maxHP[upgradeTier];
        startY = transform.position.y;
    }
    void Start()
    {
        transform.position = savePos;
    }
    void Update()
    {
        depth = -Mathf.FloorToInt((transform.position.y - startY)*depthMulti);
        textDepthCounter.text = depth.ToString();
        if (depth > maxDepth)
        {
            if (depthTimer < depthTime)
                depthTimer += Time.deltaTime;
            else
            {
                TakeDamage(depthDamage);
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
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
                immuneTimer = 0f;
            }
        }

        hpBar.fillAmount = hp / maxHP[upgradeTier];

        if(Input.GetKeyDown(KeyCode.R))
            TakeDamage(1000f);

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

    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;

        hp -= damage;

        if(hp <= 0)
            StartCoroutine(Death());
    }

    private void Restart()
    {
        hp = maxHP[upgradeTier];
        transform.position = savePos;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.Restart();
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private IEnumerator Death()
    {
        Restart();
        yield return null;
    }
}
