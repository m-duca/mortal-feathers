using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFade : MonoBehaviour
{
    // Components
    private SpriteRenderer spr;

    [SerializeField]
    private AudioSource[] catDeathAudioSources;

    #region Engine Functions

    // Start is called before the first frame update
    private void Start()
    {
        int randIndex = Random.Range(0, catDeathAudioSources.Length);

        if (!catDeathAudioSources[randIndex].isPlaying)
        {
            catDeathAudioSources[randIndex].Play();
        }
        
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        ApplyFade();
    }

    #endregion

    #region My Functions

    private void ApplyFade()
    {
        float alpha = spr.color.a;

        if (alpha <= 0.0f)
        {
            Destroy(gameObject);
        }

        alpha -= 0.055f * Time.deltaTime;

        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, alpha);
    }

    #endregion

}
