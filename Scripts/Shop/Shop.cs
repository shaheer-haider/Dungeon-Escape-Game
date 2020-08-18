using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopUI;
    public Image selectorImg;
    RectTransform rectTransform;
    public enum Selected { fireSword, flyingBoots, Key };
    int ItemPrice;
    public Selected selected;
    void Start()
    {
        rectTransform = selectorImg.GetComponent<RectTransform>();
        ItemPrice = 200;
    }


    public void ItemSelection(int height)
    {
        selectorImg.rectTransform.anchoredPosition = new Vector2(selectorImg.rectTransform.anchoredPosition.x, height);
        if (height == 83)
        {
            selected = Selected.fireSword;
            ItemPrice = 200;
        }
        else if (height == -29)
        {
            selected = Selected.flyingBoots;
            ItemPrice = 400;
        }
        else
        {
            selected = Selected.Key;
            ItemPrice = 100;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShopUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShopUI.SetActive(false);
        }
    }

    public void BuyItem()
    {
        if (PlayerPrefs.GetInt("TotalDiamonds") > ItemPrice)
        {
            if (selected == Selected.fireSword)
            {
                if (GameManager.instance.hasFireSword != "True")
                {
                    PlayerPrefs.SetInt("TotalDiamonds", PlayerPrefs.GetInt("TotalDiamonds") - ItemPrice);
                    GameManager.instance.hasFireSword = "True";
                }
                else
                {
                    UIManager.instance.cannotPurchase();
                }
            }
            else if (selected == Selected.flyingBoots)
            {
                if (GameManager.instance.hasFlyingBoots != "True")
                {
                    GameManager.instance.hasFlyingBoots = "True";
                    PlayerPrefs.SetInt("TotalDiamonds", PlayerPrefs.GetInt("TotalDiamonds") - ItemPrice);
                }
                else
                {
                    UIManager.instance.cannotPurchase();
                }
            }
            else
            {
                if (GameManager.instance.hasKey != "True")
                {
                    GameManager.instance.hasKey = "True";
                    PlayerPrefs.SetInt("TotalDiamonds", PlayerPrefs.GetInt("TotalDiamonds") - ItemPrice);
                }
                else
                {
                    UIManager.instance.cannotPurchase();
                }
            }
            GameManager.instance.updatePurchases();
        }
        else
        {
            UIManager.instance.cannotPurchase();
        }
    }
}
