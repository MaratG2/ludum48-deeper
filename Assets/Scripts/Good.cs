using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Good : MonoBehaviour
{
    public int goodType = 0;
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
        if (upgradeTier >= maxUpgrades)
            return;
        
        if (price[upgradeTier] != 0)
            textBuy.text = price[upgradeTier].ToString();
        else
            textBuy.transform.parent.gameObject.SetActive(false);

        imagePreview.sprite = imagesMain[upgradeTier];
        if (price[upgradeTier] != 0)
            imageBuy.sprite = imagesPrice[upgradeTier];
        else
            imageBuy.enabled = false;
        textDescr.text = descriptions[upgradeTier];
    }
    public void Buy()
    {
        if (upgradeTier >= maxUpgrades)
            return;

        if (player.crystalls[priceType[upgradeTier]] >= price[upgradeTier] && upgradeTier < maxUpgrades)
        {
            player.crystalls[priceType[upgradeTier]] -= price[upgradeTier];
            switch(goodType)
            {
                case 0:
                    player.upgradeTierHealth++;
                    break;
                case 1:
                    player.upgradeTierDepth++;
                    break;
                case 2:
                    player.weapon.upgradeTier++;
                    break;
            }
            upgradeTier++;
        }
    }
}
