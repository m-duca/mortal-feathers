using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings:")]

    [SerializeField, Range(0, 100)]
    private float spawnChance;

    [SerializeField, Range(0, 100)]
    private float incrementSpawnChance;

    [SerializeField, Range(0, 100)]
    private float maxSpawnChance;

    [SerializeField]
    private float spawnInterval;

    [SerializeField]
    private float maxSpawnInterval;

    [SerializeField]
    private float decrementSpawnInterval;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float maxMoveSpeed;

    [SerializeField]
    private float incrementMoveSpeed;

    [SerializeField]
    private GameObject catPrefab;

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

        StartCoroutine(SpawnCat(spawnInterval));
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

        if (Random.Range(0, 100) <= 50)
        {
            return Vector2.right;
        }
        else 
        {
            return Vector2.left;
        }
        
    }

    private IEnumerator SpawnCat(float interval)
    {
        if (Random.Range(0, 100) <= spawnChance)
        {
            Instantiate(catPrefab, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(interval);
        StartCoroutine(SpawnCat(spawnInterval));
    }

    #endregion
}
