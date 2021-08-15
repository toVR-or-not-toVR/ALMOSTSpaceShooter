using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{

    int moneyAmount;
    int isShopSold;
    int shipPrice = 5;

    [SerializeField] TMP_Text moneyAmountText;
    [SerializeField] TMP_Text shipPriceText;
    public Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmountText.text = "MONEY: " + moneyAmount.ToString() + "$";

        isShopSold = PlayerPrefs.GetInt("IsShipSold");

        if (moneyAmount >= 5 && isShopSold == 0)
            buyButton.interactable = true;
        else
            buyButton.interactable = false;
    }

    /*public void BuyShip()
    {
        moneyAmount -= shipPrice;
        PlayerPrefs.SetInt("IsShipSold", 1);

        shipPriceText.text = "BOUGHT!";
        buyButton.gameObject.SetActive(false);
    }*/

    public void BuyShip()
    {
        moneyAmount -= shipPrice;
        PlayerPrefs.SetInt("IsShipSold", StartMenu.pageNow);

        shipPriceText.text = "BOUGHT!";
        buyButton.gameObject.SetActive(false);
    }

    public void ExitShop()
    {
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        SceneManager.LoadScene(0);
    }

    public void resetPlayerPrefs()
    {
        moneyAmount = 0;
        buyButton.gameObject.SetActive(true);
        shipPriceText.text = "5$";
        PlayerPrefs.DeleteAll();
    }
}
