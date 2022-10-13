using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bloodPrefab;

    


    // References
    private CameraShake cameraShake;

    #region Engine Functions

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            
            Player.killCount++;
            cameraShake.ApplyScreenShake();
            KillCat(collision.gameObject);

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    #endregion

    #region My Functions

    private void KillCat(GameObject cat)
    {
        Instantiate(bloodPrefab[Random.Range(0, bloodPrefab.Length)], gameObject.transform.position - Vector3.up * 0.25f, Quaternion.identity);
        Destroy(cat);
        Destroy(gameObject);
    }

    #endregion
}
