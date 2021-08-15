using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject barSprite;
    private float enemyHealth;
    private float enemyHealthMax;

    private void Start()
    {
        enemyHealth = enemy.health;
        enemyHealthMax = enemyHealth;
        enemy.OnSetDamage = () => { UpdateBar(); };
    }

    private void UpdateBar()
    {
        enemyHealth = enemy.health;
        barSprite.transform.localScale = new Vector3(1,enemyHealth / enemyHealthMax,1);
    }
}
