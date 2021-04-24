using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Application.targetFrameRate != 60)
            Application.targetFrameRate = 60;
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseShop()
    {
        shop.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<Player>().TakeDamage(1000f);
    }
}
