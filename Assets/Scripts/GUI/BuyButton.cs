using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public DisplayShop shop;
    public PlayerStats player;
    public Text buttonText;
    public Text moneyAmount;

    private void Update()
    {
        moneyAmount.text = $"{player.money}";
        if (shop.itemToBuy)
        {
            if (shop.itemToBuy.id > 0)
                buttonText.text = $"Buy - {shop.itemToBuy.price} coins";
        }
        else buttonText.text = "";
    }

    public void OnClick()
    {
        if (shop.itemToBuy)
        {
            if (player.ChangeMoney(-shop.itemToBuy.price))
                if (!player.inventory.inventory.AddItem(shop.itemToBuy))
                    player.ChangeMoney(shop.itemToBuy.price);
        }
    }
}