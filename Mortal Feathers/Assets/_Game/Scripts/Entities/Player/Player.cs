using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    [HideInInspector]
    public static Rigidbody2D rb;

    [HideInInspector]
    public static SpriteRenderer spr;

    [HideInInspector]
    public static Animator animator;

    [HideInInspector]
    public static Transform transf;

    [HideInInspector]
    public static Animation anim;


    [HideInInspector]
    public static bool canPlay = true;

    #region Engine Functions
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        transf = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animation>();
    }
    #endregion
}
