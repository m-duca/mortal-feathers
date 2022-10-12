using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTrigger : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
