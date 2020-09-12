using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    private int shipState;

    public float health;

    public bool isAlive;
    private bool canFire;
    private bool isFirstFire;

    public GameObject cannonSide1;
    public GameObject fireSpawn1;
    public GameObject fireSpawn2;
    public GameObject HealthBar;
    private GameObject player;
    private GameObject temp;
    private GameObject tempVFX;

    public Rigidbody2D rb;

    private SpriteRenderer sr;

    public Sprite medHealth;
    public Sprite lowHealth;
    public Sprite zeroHealth;

    private GameControllerScript gc;

    void Start()
    {
        isFirstFire = false;
        isAlive = true;
        shipState = 0;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

        health = gc.shooterMaxHealth;
    }

    private void Update()
    {
        if(!isFirstFire)
        {
            canFire = true;
            isFirstFire = true;
        }
        
        if(canFire && !gc.gameOver && sr.isVisible && isAlive)
        {
            StartCoroutine(EnemyFire());
            canFire = false;
        }

        if(!isAlive)
        {
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && isAlive)
        {
            DamageShooter(collision.gameObject);
        }
    } 
    

    public void HealthUpdate()
    {
        health -= gc.playerDamage;

        if (health <= 0)
        {
            KillShooter();
            sr.sprite = zeroHealth;
            SpawnVisualEffects();
            gc.PlayAudio("Explosion", 0.2f);
        }
        else if ((health / gc.shooterMaxHealth) < 0.4 && shipState == 1)
        {
            shipState = 2;
            sr.sprite = lowHealth;
            SpawnVisualEffects();
            gc.PlayAudio("Explosion", 0.1f);
        }
        else if ((health / gc.shooterMaxHealth) < 0.7 && shipState == 0)
        {
            shipState = 1;
            sr.sprite = medHealth;
            SpawnVisualEffects();
            gc.PlayAudio("Explosion", 0.1f);
        }

    }

    public void SpawnVisualEffects()
    {
        tempVFX = Instantiate(gc.bigExplosion, transform.position, Quaternion.identity);
        tempVFX.transform.SetParent(this.transform);
        tempVFX = Instantiate(gc.fire, transform.position, Quaternion.identity);
        tempVFX.transform.SetParent(this.transform);
        tempVFX = Instantiate(gc.fire, fireSpawn1.transform.position, Quaternion.identity);
        tempVFX.transform.SetParent(this.transform);
        tempVFX = Instantiate(gc.fire, fireSpawn2.transform.position, Quaternion.identity);
        tempVFX.transform.SetParent(this.transform);
    }

    public IEnumerator EnemyFire()
    {
        temp = Instantiate(gc.enemyCannonBullet, cannonSide1.transform.position, Quaternion.identity);
        temp.transform.up = ((Vector2)player.transform.position - (Vector2)transform.position);
        temp.transform.rotation = Quaternion.Euler(temp.transform.localEulerAngles.x, temp.transform.localEulerAngles.y, temp.transform.localEulerAngles.z - 5);
        gc.PlayAudio("Shoot", 0.1f);

        temp = Instantiate(gc.enemyCannonBullet, cannonSide1.transform.position, Quaternion.identity);
        temp.transform.up = ((Vector2)player.transform.position - (Vector2)transform.position);
        temp.transform.rotation = Quaternion.Euler(temp.transform.localEulerAngles.x, temp.transform.localEulerAngles.y, temp.transform.localEulerAngles.z + 5);
        gc.PlayAudio("Shoot", 0.1f);

        yield return new WaitForSeconds(0.2f);

        temp = Instantiate(gc.enemyCannonBullet, cannonSide1.transform.position, Quaternion.identity);
        temp.transform.up = ((Vector2)player.transform.position - (Vector2)transform.position);
        gc.PlayAudio("Shoot", 0.1f);

        yield return new WaitForSeconds(gc.shooterBulletDelay);
        canFire = true;
    }

    public void DamageShooter(GameObject bullet)
    {
        HealthUpdate();
        HealthBar.gameObject.GetComponent<HealthBarScript>().UpdateHealthBarSize(health / gc.shooterMaxHealth);

        Instantiate(gc.explosion, bullet.transform.position, Quaternion.identity);
        Destroy(bullet.gameObject);

        gc.PlayAudio("Explosion", 0.2f);
    }

    public void KillShooter()
    {
        isAlive = false;
        Destroy(HealthBar);

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<FadeOutScript>().fadeStart = true;

        gc.score += 1;
        gc.UpdateScore();
    }
}
