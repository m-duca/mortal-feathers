using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCollision : MonoBehaviour
{
    // References
    [SerializeField]
    private GameObject hudCanvas;

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private GameObject enemySpawner;

    [SerializeField]
    private AudioSource deathAudioSource;

    private CameraShake cameraShake;



    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat") && !PlayerMovement.isDashing)
        {
            if (!deathAudioSource.isPlaying)
            {
                deathAudioSource.Play();
            }
            
            cameraShake.ApplyScreenShake();
            Player.rb.velocity = Vector2.zero;
            enemySpawner.SetActive(false);
            hudCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            HUDManager hudScript = hudCanvas.GetComponent<HUDManager>();
            GameOverManager gameOverScript = gameOverCanvas.GetComponent<GameOverManager>();

            gameOverScript.SetUIValues(hudScript.GetKillCount(), hudScript.GetTime());

            Player.spr.enabled = false;
            Player.canPlay = false;
        }

        if (collision.CompareTag("Wall"))
        {
            switch (collision.gameObject.name) 
            {
                case "Wall Right":
                    // x -10.18
                    Player.transf.position = new Vector2(-10.18f, Player.transf.position.y);
                    break;

                case "Wall Left":
                    // x 10.22
                    Player.transf.position = new Vector2(10.22f, Player.transf.position.y);
                    break;

                case "Wall Up":
                    // y -5.45
                    Player.transf.position = new Vector2(Player.transf.position.x, -5.45f);
                    break;


                case "Wall Down":
                    // y 5.88
                    Player.transf.position = new Vector2(Player.transf.position.x, 5.88f);
                    break;
            }
        }
    }
}
