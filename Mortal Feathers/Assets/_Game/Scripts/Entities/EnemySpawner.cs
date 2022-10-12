using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings:")]

    [SerializeField, Range(0, 100)]
    private float initialSpawnChance;

    [SerializeField, Range(0, 100)]
    private float incrementSpawnChance;

    [SerializeField, Range(0, 100)]
    private float maxSpawnChance;

    [SerializeField]
    private float initialSpawnInterval;

    [SerializeField]
    private float minSpawnInterval;

    [SerializeField]
    private float decrementSpawnInterval;

    [SerializeField]
    private float initialMoveSpeed;

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

    [SerializeField]
    private float curSpawnChance;

    private float curSpawnInterval;

    private float curMoveSpeed;

    // Change Coroutines
    private float changeMoveSpeedT = 9f;
    private float changeSpawnChanceT = 15f;
    private float changeSpawnIntervalT = 21f;

    #region Engine Functions
    // Start is called before the first frame update
    private void Start()
    {
        SetInitialValues();

        //gameObject.GetComponent<SpriteRenderer>().enabled = false;

        rb = gameObject.GetComponent<Rigidbody2D>();
        curDirection = SetInitialDirection();

        StartCoroutine(SpawnCat(curSpawnInterval));
        StartCoroutine(ChangeMoveSpeed(changeMoveSpeedT));
        StartCoroutine(ChangeSpawnChance(changeSpawnChanceT));
        StartCoroutine(ChangeSpawnInterval(changeSpawnIntervalT));
    }

    private void FixedUpdate()
    {
        rb.velocity = curMoveSpeed * curDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemySpawnerTrigger"))
        {
            gameObject.transform.position = collision.gameObject.transform.position;
            SetNewDirection(collision.gameObject.name);
        }
    }

    #endregion

    #region My Functions

    private Vector2 SetInitialDirection() 
    {

        if (Random.Range(0, 100f) <= 50f)
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
        if (Random.Range(0, 100) <= curSpawnChance)
        {
            Instantiate(catPrefab, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(interval);
        StartCoroutine(SpawnCat(curSpawnInterval));
    }

    private void SetNewDirection(string name)
    {
        switch (name) 
        {
            case "Enemy Spawner Trigger (RightU)":
                if (curDirection == Vector2.right)
                {
                    curDirection = Vector2.down;
                }
                else // Vector2.up
                {
                    curDirection = Vector2.left;
                }
                break;


            case "Enemy Spawner Trigger (RightD)":
                if (curDirection == Vector2.right)
                {
                    curDirection = Vector2.up;
                }
                else // Vector2.down
                {
                    curDirection = Vector2.left;
                }
                break;

            case "Enemy Spawner Trigger (LeftU)":
                if (curDirection == Vector2.left)
                {
                    curDirection = Vector2.down;
                }
                else // Vector2.up
                {
                    curDirection = Vector2.right;
                }
                break;

            case "Enemy Spawner Trigger (LeftD)":
                if (curDirection == Vector2.left)
                {
                    curDirection = Vector2.up;
                }
                else // Vector2.up
                {
                    curDirection = Vector2.right;
                }
                break;

        }

    }

    private void SetInitialValues()
    {
        curSpawnChance = initialSpawnChance;
        curSpawnInterval = initialSpawnInterval;
        curMoveSpeed = initialMoveSpeed;
    }

    public IEnumerator ChangeMoveSpeed(float time)
    {

        yield return new WaitForSeconds(time);
        if (curMoveSpeed < maxMoveSpeed)
        {
           curMoveSpeed += incrementMoveSpeed;
        }
        else
        {
           curMoveSpeed = maxMoveSpeed;
        }

        StartCoroutine(ChangeMoveSpeed(changeMoveSpeedT));
    }

    private IEnumerator ChangeSpawnChance(float time)
    {

        yield return new WaitForSeconds(time);
        if (curSpawnChance < maxSpawnChance)
        {
           curSpawnChance += incrementSpawnChance;
        }
        else
        {
           curSpawnChance = maxSpawnChance;
        }

        StartCoroutine(ChangeSpawnChance(changeSpawnChanceT));
    }

    public IEnumerator ChangeSpawnInterval(float time)
    {

        yield return new WaitForSeconds(time);
        if (curSpawnInterval > minSpawnInterval)
        {
            curSpawnInterval -= decrementSpawnInterval;
        }
        else
        {
            curSpawnInterval = minSpawnInterval; 
        }

        StartCoroutine(ChangeSpawnInterval(changeSpawnIntervalT));
    }

    #endregion
}
