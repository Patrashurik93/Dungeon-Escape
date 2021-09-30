using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    public int itemSelected;
    public int itemCost;
    private const int key = 2;
    private const int boots = 1;
    private const int sword = 0;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();

            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }
            shopPanel.transform.DOLocalMoveY(Vector3.zero.y, 1.0f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.transform.DOLocalMoveY(1055f, 1.0f);
        }
    }

    public void Selection(int item)
    {
        Debug.Log("Selection() " + item );

        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(79);
                itemSelected = 0;
                itemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-45);
                itemSelected = 1;
                itemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-150);
                itemSelected = 2;
                itemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if (player.diamonds >= itemCost)
        {
            //award
            if (itemSelected == key)
            {
                GameManager.Instance.HasKeyToCastle = true;
                player.diamonds -= itemCost;
                UIManager.Instance.UpdateGemCount(player.diamonds);
            }          
            else if (itemSelected == boots)
            {
                GameManager.Instance.HasFlyingBoots = true;
                player.diamonds -= itemCost;
                UIManager.Instance.UpdateGemCount(player.diamonds);
            }
            else if(itemSelected == sword)
            {
                GameManager.Instance.HasFlameSword = true;
                player.diamonds -= itemCost;
                UIManager.Instance.UpdateGemCount(player.diamonds);
            }
        }
        else
        {
            Debug.Log("You don't have enugh gems.");
        }
    }
}
