using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
