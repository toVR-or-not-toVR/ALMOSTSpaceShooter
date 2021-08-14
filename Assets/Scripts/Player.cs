using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Phone Movement")]
    [SerializeField] float moveSpeedPhone = 10f;
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;

    [Header("Shield")]
    [SerializeField] GameObject shield;
    [SerializeField] bool shielded;

    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Sounds")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;


    Coroutine firingCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        shielded = false;
 
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
       // MobileMove();
       // MobileFire();
        Move(); 
        Fire();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Meteor")
        {
            
            if (!shielded)
            {
                Destroy(gameObject);
                FindObjectOfType<SceneLoader>().LoadGameOver();
            }
        }


        if (collision.gameObject.tag == "LifePill")
        {
            Debug.Log("Lifepill");
            health += 100;
        }



        if (collision.gameObject.tag == "Shield")
        {
            CheckShield();
        }

     

        if(!shielded)
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }



        /*if(collision.gameObject.tag == "Coin")
        {
            Destroy();
        }
        */
    }

    private void CheckShield()
    {
        shield.SetActive(true);
        shielded = true;
        Invoke("NoShield", 8f);
    }

    private void NoShield()
    {
        shield.SetActive(false);
        shielded = false;
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<SceneLoader>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
       
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
       
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void MobileMove()
    {
        Touch touch = Input.GetTouch(0);
        touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        touchPosition.z = 0;
        direction = (touchPosition - transform.position);
        rb.velocity = new Vector2(direction.x, direction.y) * moveSpeedPhone;

        if(touch.phase == TouchPhase.Ended)
        {
            rb.velocity = Vector2.zero;
        }

    }



    private void MobileFire()
    {
            firingCoroutine = StartCoroutine(FireContinuously());
    }



    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

    }


    IEnumerator FireContinuously()
    {
        while (true)    
        {
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    public int GetHealth()
    {
        return health;
    }

}