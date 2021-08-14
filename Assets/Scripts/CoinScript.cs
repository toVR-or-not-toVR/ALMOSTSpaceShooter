using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameControl.moneyAmount += 1;
            PlayerPrefs.SetInt("MoneyAmount", GameControl.moneyAmount);
            Destroy(gameObject);
        }

    }

    /*private void Update()
    {
        transform.rotation = new Vector2
    }*/
}
