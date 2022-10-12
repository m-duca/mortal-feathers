using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings:")]

    [SerializeField]
    private float moveSpeed;


    // Components
    private Rigidbody2D rb;


    // Movement
    private Vector2 curDirection;

    #region Engine Functions
    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        curDirection = SetInitialDirection();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveSpeed * curDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemySpawnerTrigger"))
        {
            curDirection = collision.gameObject.GetComponent<EnemySpawnerTrigger>().direction;
        }
    }

    #endregion

    #region My Functions

    private Vector2 SetInitialDirection() 
    {

        if (Random.Range(0, 50) <= 50)
        {
            return Vector2.right;
        }
        else 
        {
            return Vector2.left;
        }
        
    }

    #endregion
}
