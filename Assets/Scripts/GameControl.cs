using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControl : MonoBehaviour
{

    [SerializeField] TMP_Text moneyText;
    public static int moneyAmount;
    int isShipSold;
    public GameObject ship1;
    public GameObject ship2;


    

    private void Awake()
    {
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        isShipSold = PlayerPrefs.GetInt("IsShipSold");

        if  (isShipSold == 0)
        {
            ship1.SetActive(true);
            ship2.SetActive(false);
        }

        else
        {
            ship1.SetActive(false);
            ship2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        moneyText.text = "BANK: " + moneyAmount.ToString() + "$";
    }
}
