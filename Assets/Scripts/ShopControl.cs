using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{

    public int moneyAmount;
    public int isShopSold;
    int shipPrice = 5;

    [SerializeField] TMP_Text moneyAmountText;
    [SerializeField] TMP_Text shipPriceText;

    [SerializeField] GameObject ship_1;
    [SerializeField] GameObject ship_2;


    [SerializeField] bool stage_1;
    [SerializeField] bool stage_2;
    [SerializeField] bool stage_3;
    [SerializeField] bool stage_4;

   public int isShip1Sold;
    public int isShip2Sold;

    float  x_pos_ship_1;
    float  x_pos_ship_2;

    public Button buyButton;

    public int currentShip;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        isShip1Sold = PlayerPrefs.GetInt("isShip1Sold");
        isShip2Sold = PlayerPrefs.GetInt("isShip2Sold");
        
    }

    // Update is called once per frame
    void Update()
    {

        x_pos_ship_1 = ship_1.transform.position.x;
        x_pos_ship_2 = ship_2.transform.position.x;



        if (x_pos_ship_1 == 0)
        {
            currentShip = 0;
        }
        else
        {
            currentShip = 1;
        }



        if (currentShip == 0)
        {
            if (isShip1Sold == 0)
            {
                buyButton.gameObject.SetActive(true);
                buyButton.interactable = true;
                Debug.Log("stage_1");
                shipPriceText.text = "5$";
            }
            else
            {
                buyButton.gameObject.SetActive(false);
                Debug.Log("stage_2");
                buyButton.interactable = false;
                shipPriceText.text = "BOUGHT!";
            }     
         }


        if (currentShip == 1)
        {
            if (isShip2Sold == 0)
            {
                buyButton.gameObject.SetActive(true);
                Debug.Log("stage_3");
                buyButton.interactable = true;
                shipPriceText.text = "5$";
            }
            else
            {
                buyButton.gameObject.SetActive(false);
                Debug.Log("stage_4");
                buyButton.interactable = false;
                shipPriceText.text = "BOUGHT!";
            }
        }





        Debug.Log(moneyAmount);
        moneyAmountText.text = "MONEY: " + moneyAmount.ToString() + "$";

        //isShopSold = PlayerPrefs.GetInt("IsShipSold");

  /*      if (moneyAmount >= 5 && isShopSold == 0)
            buyButton.interactable = true;
        else
            buyButton.interactable = false;*/
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

    

       
        
      //  PlayerPrefs.SetInt("IsShipSold", StartMenu.pageNow);

        if (moneyAmount >= 5)
        {
            moneyAmount -= shipPrice;
         

            if (currentShip == 0)
            {
                isShip1Sold = 1;
            }
            if (currentShip == 1)
            {
                isShip2Sold = 1;
            }
        }



       // shipPriceText.text = "BOUGHT!";
       // buyButton.gameObject.SetActive(false);
    }

    public void ExitShop()
    {
        if(isShip1Sold == 0 && isShip2Sold ==0)
        {
            isShopSold = 0;
        }
        else if(isShip1Sold == 1 && isShip2Sold == 0)
        {
            isShopSold = 1;
        }
        else if(isShip2Sold == 1 && isShip1Sold == 0)
        {
            isShopSold = 2;
        }
        else
        {
            isShopSold = 2;
        }

        PlayerPrefs.SetInt("isShip1Sold", isShip1Sold);
        PlayerPrefs.SetInt("isShip2Sold", isShip2Sold);
        PlayerPrefs.SetInt("IsShipSold", isShopSold);
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        FindObjectOfType<SceneLoader>().LoadStartMenu();
    }

    public void resetPlayerPrefs()
    {
        moneyAmount = 0;
        buyButton.gameObject.SetActive(true);
        shipPriceText.text = "5$";
        PlayerPrefs.DeleteAll();
    }
}
