using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject shopUIDetailflame;
    public GameObject shopUIDetailBoot;
    public GameObject shopUIDetailKey;
    

    public int selectedItem;
    public int selectedItemPrice;

    private Player player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            if (player != null)
            {
                UIManager.Instance.Openshop(player.diamond);
            }
            shopUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shopUI.SetActive(false);
        }
    }

    public void selectItem(int ButNumber)
    {
        Debug.Log("Button Tapped");

        shopUIDetailflame.SetActive(false);
        shopUIDetailBoot.SetActive(false);
        shopUIDetailKey.SetActive(false);

        switch (ButNumber)
        {
            case 0:
                UIManager.Instance.updateShopSelection(116);
                selectedItem = 0;
                selectedItemPrice = 150;
                shopUIDetailflame.SetActive(true);
                break;
            case 1:
                UIManager.Instance.updateShopSelection(10);
                selectedItem = 1;
                selectedItemPrice = 100;
                shopUIDetailBoot.SetActive(true);
                break;
            case 2:
                UIManager.Instance.updateShopSelection(-98);
                selectedItem = 2;
                selectedItemPrice = 200;
                shopUIDetailKey.SetActive(true);
                break;
        }
    }

    public void BuyItem()
    {
        if (selectedItemPrice <= player.diamond)
        {
            // for Key
            if (selectedItem == 2)
            {
                GameManager.Instance.HasKeyToTheCastle = true;

            }

            // for Boots
            if (selectedItem == 1)
            {
                player._jumpForce = 10.0f;
            }

            if (selectedItem == 0)
            {
                player.Health = 6;
                player.isHealthPlus = true;
                UIManager.Instance.incrementHealth(5);
            }

            player.diamond -= selectedItemPrice;
            Debug.Log("Item Purchased :" + selectedItem);
        }
        else
        {
            Debug.Log("Cannot Purchase the item, Closing the Shop Window");
            shopUI.SetActive(false);
        }

        shopUIDetailflame.SetActive(false);
        shopUIDetailBoot.SetActive(false);
        shopUIDetailKey.SetActive(false);
    }
}
