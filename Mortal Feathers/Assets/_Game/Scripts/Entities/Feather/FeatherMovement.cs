using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherMovement : MonoBehaviour
{
    [Header("Movement Settings:")]

    [SerializeField]
    private float moveSpeed;

    // Components
    private Rigidbody2D rb;

    [HideInInspector]
    public Vector2 moveDirection;

    #region Engine Functions

    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveSpeed * moveDirection;
    }

    #endregion
}
