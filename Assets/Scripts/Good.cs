using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Good : MonoBehaviour
{
    public int[] price;
    public int[] priceType;
    public string[] descriptions;
    public Sprite[] imagesMain;
    public Sprite[] imagesPrice;
    public int maxUpgrades = 3;
    public int upgradeTier = 0;
    
    public Player player;
    public TextMeshProUGUI textDescr;
    public TextMeshProUGUI textBuy;
    public Image imagePreview;
    public Image imageBuy;
    public void Update()
    {
        textBuy.text = price[upgradeTier].ToString();
        imagePreview.sprite = imagesMain[upgradeTier];
        imageBuy.sprite = imagesPrice[priceType[upgradeTier]];
        textDescr.text = descriptions[upgradeTier];
    }
    public void Buy()
    {
        if(player.crystalls[priceType[upgradeTier]] >= price[upgradeTier] && upgradeTier < maxUpgrades)
        {
            player.crystalls[priceType[upgradeTier]] -= price[upgradeTier];
            upgradeTier++;
        }
    }
}
