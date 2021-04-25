﻿using System.Collections;
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
    private void Awake()
    {
        circle = GetComponentInChildren<Image>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player pl = collision.GetComponent<Player>();
        if (!pl)
        {
            timerHarvest = 0f;
            circle.fillAmount = timerHarvest / timeHarvest;
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(pl.weapon.bulletPivotPoint.position * -pl.transform.localScale.x, pl.transform.right, 5f);
        //Debug.DrawRay(pl.transform.position, pl.transform.right * 5f * -pl.transform.localScale.x, Color.red, 1f);
        if (!hit)
        {
            timerHarvest = 0f;
            circle.fillAmount = timerHarvest / timeHarvest;
            return;
        }
        //Debug.Log(hit.collider.gameObject + " | " + gameObject + " | " + Input.GetMouseButton(0));
        if (pl && hit.collider.gameObject == gameObject && Input.GetMouseButton(1))
        {
            if (timerHarvest < timeHarvest)
            {
                timerHarvest += Time.deltaTime;
                pl.crystal = this;
                pl.isHarvesting = true;
            }
            else
            {
                pl.crystalls[crystalType] += quantity;
                pl.isHarvesting = false;
                pl.crystal = null;
                circle.fillAmount = timerHarvest / timeHarvest;
                Destroy(gameObject);
            }
        }
        else
        {
            timerHarvest = 0f;
            pl.isHarvesting = false;
        }
        circle.fillAmount = timerHarvest / timeHarvest;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player pl = collision.GetComponent<Player>();
        if (pl)
            timerHarvest = 0f;
    }
}