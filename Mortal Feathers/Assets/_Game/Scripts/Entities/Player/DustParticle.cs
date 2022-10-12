using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticle : MonoBehaviour
{
    // Components
    private ParticleSystem particles;

    #region Engine Functions
    private void Awake()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        VerifyMove();
    }
    #endregion

    #region My Functions
    private void VerifyMove()
    {
        if (Player.spr.flipX)
        {
            gameObject.transform.position = Player.transf.position + new Vector3(0.138f, -0.492f, 0f);
        }
        else
        {
            gameObject.transform.position = Player.transf.position + new Vector3(-0.138f, -0.492f, 0f);
        }

        if (PlayerMovement.moveInput == Vector2.zero)
        {
            Invoke("StopParticles", 0.5f);
        }
        else
        {
            particles.enableEmission = true;
        }
    }

    private void StopParticles()
    {
        particles.enableEmission = false;
    }
    #endregion
}
