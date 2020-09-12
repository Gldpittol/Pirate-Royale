using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject cannonBullet;
    public GameObject enemyCannonBullet;
    public GameObject[] enemy;
    public GameObject[] enemyHealthBar;
    public GameObject playerHealthBar;

    [Header("VFX Prefabs")]
    public GameObject explosion;
    public GameObject bigExplosion;
    public GameObject fire;

    [Header("Player")]
    public float playerHealth;
    public float playerSpeed;
    public float playerRotationSpeed;
    public float playerBulletSpeed;
    public float playerAttackFrontDelay;
    public float playerAttackSideDelay;
    public float playerDamage;
    public float playerEnemyCollisionDamage;

    [Header("Shooter")]
    public float shooterMaxHealth;
    public float enemyBulletSpeed;
    public float shooterBulletDelay;
    public float shooterBulletDamage;

    [Header("Chaser")]
    public float chaserMaxHealth;
    public float chaserSpeed;

    [Header("Fixed Cannons")]
    public float fixedCannonBulletDelay;

    [Header("Canvas")]
    public Text scoreText;
    public Text timeText;

    [Header("Control")]
    public bool gameOver;
    public int score;

    [Header("Audio")]
    public AudioSource audSource;
    public AudioClip cannonShotClip;
    public AudioClip explosionClip;
    public AudioClip backgroundMusicClip;

    [Header("From Static Variables")]
    public float timeAvailable;
    public float spawnDelay;
    public float audioIntensity;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (StaticVariables.time < 60)
            timeAvailable = 90;
        else
            timeAvailable = StaticVariables.time;

        if (StaticVariables.spawnDelay < 0.1)
            spawnDelay = 3;
        else
            spawnDelay = StaticVariables.spawnDelay;

        if (StaticVariables.audioIntensity == 0)
            audioIntensity = 1f;
        else
            audioIntensity = StaticVariables.audioIntensity;

        if (StaticVariables.audioIntensity == 0.01)
            audioIntensity = 0;

        if (scoreText != null)
            scoreText.text = "Score: " + StaticVariables.currentScore.ToString() + " (Highscore: " + StaticVariables.highScore + ")";

        if(backgroundMusicClip != null)
        {
            audSource.PlayOneShot(backgroundMusicClip, audioIntensity * 0.16f);
        }

    }

    public void GameOver()
    {
        if (score > StaticVariables.highScore)
        {
            StaticVariables.highScore = score;
        }

       StaticVariables.currentScore = score;

       StartCoroutine(DelayedGameOver());
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString() + " (Highscore: " + StaticVariables.highScore + ")";
    }
    public void AssignControlScheme(int assignedScheme)
    {
        StaticVariables.controlScheme = assignedScheme;
    }

    public void PlayAudio(string audioName, float intensity)
    {
        switch (audioName)
        {
            case "Shoot":
                {
                    audSource.PlayOneShot(cannonShotClip, intensity * audioIntensity);
                    break;
                }
            case "Explosion":
                {
                    audSource.PlayOneShot(explosionClip, intensity * audioIntensity);
                    break;
                }
        }
    }

    public void PlayMusic(float intensity)
    {
        audSource.Stop();
        audSource.PlayOneShot(backgroundMusicClip, 0.16f * intensity);
    }

    public IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndMenu", LoadSceneMode.Single);
    }
}
