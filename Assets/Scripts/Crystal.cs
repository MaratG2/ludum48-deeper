using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    public int crystalType = 0;
    public int quantity = 2;
    public float timeHarvest = 2f;
    public float timerHarvest = 0f;
    public Image circle;
    public bool resetting;
    private bool vanished;
    private Vector3 startPos;
    private Player player;
    private AudioSource drillAudioSource;
    private void Awake()
    {
        circle = GetComponentInChildren<Image>();
        startPos = transform.position;
        drillAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player pl = collision.GetComponent<Player>();
        if (pl)
        {
            player = pl;
        }
    }
    void Update()
    {
        if (!player || vanished)
        {
            timerHarvest = 0f;
            circle.fillAmount = timerHarvest / timeHarvest;
            return;
        }
        //RaycastHit2D hit = Physics2D.Raycast(pl.weapon.bulletPivotPoint.position * -pl.transform.localScale.x, pl.transform.right, 5f);
        RaycastHit2D hit = Physics2D.Raycast(player.lasetPoint.transform.position, player.lasetPoint.transform.right * 5f * -player.transform.localScale.x, 5f);
        //RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.transform.right * 5f * -player.transform.localScale.x, 5f);
        //if (hit)
            //Debug.Log("Hit: " + hit.collider.gameObject);
        Debug.DrawRay(player.lasetPoint.transform.position, player.lasetPoint.transform.right * 5f * -player.transform.localScale.x, Color.red, 1f);
        if (!hit)
        {
            timerHarvest = 0f;
            circle.fillAmount = timerHarvest / timeHarvest;
            return;
        }
        //Debug.Log(hit.collider.gameObject + " | " + gameObject + " | " + Input.GetMouseButton(1));
        if (player && hit.collider.gameObject == gameObject && Input.GetMouseButton(1))
        {
            if (timerHarvest < timeHarvest)
            {
                timerHarvest += Time.deltaTime;
                if (!drillAudioSource.isPlaying)
                    drillAudioSource.Play();

                player.crystal = this;
                player.isHarvesting = true;
            }
            else
            {
                player.crystalls[crystalType] += quantity;
                drillAudioSource.Pause();
                player.isHarvesting = false;
                player.crystal = null;
                circle.fillAmount = timerHarvest / timeHarvest;
                Vanish();
            }
        }
        else
        {
            timerHarvest = 0f;
            player.isHarvesting = false;
            drillAudioSource.Pause();
        }
        circle.fillAmount = timerHarvest / timeHarvest;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player pl = collision.GetComponent<Player>();
        if (pl)
        {
            timerHarvest = 0f;
            drillAudioSource.Pause();
            player = null;
        }
    }

    private void Vanish()
    {
        vanished = true;
        GetComponent<SpriteRenderer>().enabled = false;
        drillAudioSource.Pause();
        gameObject.layer = 2;
    }

    public void Reset()
    {
        vanished = false;
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = startPos;
        gameObject.layer = 8;
    }
}