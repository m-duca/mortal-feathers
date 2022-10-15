using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shoot Settings:")]

    [SerializeField]
    private GameObject featherPrefab;

    [SerializeField]
    private float shootInterval;

    [SerializeField]
    private AudioSource[] shootAudioSources;

    private bool canShoot = true;

    // Audio
    private int curAudioIndex = 0;

    #region Engine Functions

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetMouseButton(0) && canShoot && Player.canPlay)
        {
            PlayAudio();
            SpawnFeather();
            StartCoroutine(SetShootInterval(shootInterval));
        }

    }
    #endregion

    #region My Functions
    private IEnumerator SetShootInterval(float interval)
    {

        canShoot = false;
        yield return new WaitForSeconds(interval);
        canShoot = true;

    }

    private void SpawnFeather()
    {

        // Calculate Move Direction and Spawn Position
        Vector2 direction = (MouseCursor.position - (Vector2)Player.transf.position).normalized;
        Vector2 spawnPos = (Vector2) Player.transf.position + direction;

        // Calculate Feather´s Rotation
        Vector2 dirRotation = (Vector2)Player.transf.position - MouseCursor.position;
        float rotationZ = Mathf.Atan2(dirRotation.y, dirRotation.x) * Mathf.Rad2Deg;

        // Spawn Feather
        GameObject feather = Instantiate(featherPrefab, spawnPos, Quaternion.Euler(0, 0, rotationZ));

        // Set Feather´s move direction
        feather.GetComponent<FeatherMovement>().moveDirection = direction;

    }

    private void PlayAudio()
    {
        shootAudioSources[curAudioIndex].Play();

        if (curAudioIndex < shootAudioSources.Length-1)
        {
            curAudioIndex++;
        }
        else
        {
            curAudioIndex = 0;
        }
    }

    #endregion
}
