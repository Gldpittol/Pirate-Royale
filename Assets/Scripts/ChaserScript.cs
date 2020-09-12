using UnityEngine;

public class ChaserScript : MonoBehaviour
{
    private int shipState;

    public float health;

    public bool isAlive;

    public GameObject HealthBar;
    public GameObject fireSpawn1;
    public GameObject fireSpawn2;
    private GameObject player;
    private GameObject tempVFX;

    public Rigidbody2D rb;

    private SpriteRenderer sr;

    public Sprite medHealth;
    public Sprite lowHealth;
    public Sprite zeroHealth;

    private GameControllerScript gc;


    void Start()
    {
        isAlive = true;
        shipState = 0;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

        health = gc.chaserMaxHealth;
    }

    void Update()
    {
        if (!gc.gameOver && isAlive)
        { 
            transform.up = player.transform.position - transform.position; 
        }

        rb.AddForce(transform.up * gc.chaserSpeed);  

        if(!isAlive)
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.angularVelocity = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && isAlive)
        {
            DamageChaser(collision.gameObject);
        }
    }

    public void HealthUpdate()
    {   
        health -= gc.playerDamage;

        if (health  <= 0)
        {
            KillChaser();
            sr.sprite = zeroHealth;
            SpawnVisualEffects();
            gc.PlayAudio("Explosion", 0.2f);
        }
        else if ((health / gc.chaserMaxHealth) < 0.4 && shipState == 1)
        {
            shipState = 2;
            sr.sprite = lowHealth;
            SpawnVisualEffects();
            gc.PlayAudio("Explosion", 0.1f);
        }
        else if ((health / gc.chaserMaxHealth) < 0.7 && shipState == 0)
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

    public void DamageChaser(GameObject bullet)
    {
        HealthUpdate();
        HealthBar.gameObject.GetComponent<HealthBarScript>().UpdateHealthBarSize(health / gc.chaserMaxHealth);

        Instantiate(gc.explosion, bullet.transform.position, Quaternion.identity);
        Destroy(bullet.gameObject);

        gc.PlayAudio("Explosion", 0.2f);
    }

    public void KillChaser()
    {
        isAlive = false;
        Destroy(HealthBar);

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<FadeOutScript>().fadeStart = true;

        gc.score += 1;
        gc.UpdateScore();
    }
}
