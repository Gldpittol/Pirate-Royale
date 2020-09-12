using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollisions : MonoBehaviour
{
    private int shipState;

    public float currentHealth;

    public GameObject temp;
    public GameObject tempVFX;
    public GameObject fireSpawn1;
    public GameObject fireSpawn2;
    public GameObject fireSpawn3;
    public GameObject fireSpawn4;
    public GameObject HealthBar;

    private SpriteRenderer sr;

    public Sprite medHealth;
    public Sprite lowHealth;
    public Sprite zeroHealth;

    private GameControllerScript gc;

    private void Start()
    {
        shipState = 0;

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        sr = GetComponent<SpriteRenderer>();

        currentHealth = gc.playerHealth;

        temp = Instantiate(gc.playerHealthBar, transform.position, Quaternion.Euler(0, 0, 90));
        temp.GetComponent<HealthBarScript>().AttachHealthBar(this.gameObject);
        HealthBar = temp;

        StaticVariables.currentScore = 0;
        gc.UpdateScore();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Chaser") && collision.gameObject.GetComponent<ChaserScript>().isAlive)
        {
            currentHealth -= gc.playerEnemyCollisionDamage;
            collision.gameObject.GetComponent<ChaserScript>().health = 0;
            collision.gameObject.GetComponent<ChaserScript>().HealthUpdate();
            HealthUpdate();
            HealthBar.gameObject.GetComponent<HealthBarScript>().UpdateHealthBarSize(currentHealth / gc.playerHealth);

            gc.PlayAudio("Explosion", 0.7f);
        }

        if (collision.gameObject.CompareTag("Shooter") && collision.gameObject.GetComponent<ShooterScript>().isAlive)
        {
            currentHealth -= gc.playerEnemyCollisionDamage;
            collision.gameObject.GetComponent<ShooterScript>().health = 0;
            collision.gameObject.GetComponent<ShooterScript>().HealthUpdate();
            HealthUpdate();
            HealthBar.gameObject.GetComponent<HealthBarScript>().UpdateHealthBarSize(currentHealth / gc.playerHealth);

            gc.PlayAudio("Explosion", 0.7f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && !gc.gameOver)
        {
            DamagePlayer(collision.gameObject);       
        }
    }

    private void HealthUpdate()
    {
        currentHealth -= gc.shooterBulletDamage;

        if (currentHealth <= 0)
        {
            KillPlayer();
            sr.sprite = zeroHealth;
            SpawnVisualEffects();
            gc.PlayAudio("Explosion", 0.2f);
        }
        else if ((currentHealth / gc.playerHealth) < 0.4 && shipState == 1)
        {
            shipState = 2;
            sr.sprite = lowHealth;
            SpawnVisualEffects();
            gc.PlayAudio("Explosion", 0.1f);
        }
        else if ((currentHealth / gc.playerHealth) < 0.7 && shipState == 0)
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
        tempVFX = Instantiate(gc.fire, fireSpawn3.transform.position, Quaternion.identity);
        tempVFX.transform.SetParent(this.transform);
        tempVFX = Instantiate(gc.fire, fireSpawn4.transform.position, Quaternion.identity);
        tempVFX.transform.SetParent(this.transform);
    }

    public void DamagePlayer(GameObject bullet)
    {
        HealthUpdate();
        HealthBar.gameObject.GetComponent<HealthBarScript>().UpdateHealthBarSize(currentHealth / gc.playerHealth);

        Instantiate(gc.explosion, bullet.transform.position, Quaternion.identity);
        Destroy(bullet.gameObject);

        gc.PlayAudio("Explosion", 0.2f);
    }

    public void KillPlayer()
    {
        gc.gameOver = true;
        gc.GameOver();

        Destroy(HealthBar);

        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<FadeOutScript>().fadeStart = true;
    }

}
