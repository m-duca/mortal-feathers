using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [Header("Movement Settings:")]

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float decceleration;

    [SerializeField]
    private float velPower;

    [SerializeField, Range(0f, 1f)]
    private float frictionAmount;


    // Components
    private Rigidbody2D rb;
    private SpriteRenderer spr;

    // Movement
    private Vector2 moveDirection;

    #region Engine Functions
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

        SetMoveDirection();

    }

    private void FixedUpdate()
    {

        ApplyMovement();
        FlipX();
    }

    #endregion

    #region My Functions

    private void SetMoveDirection()
    {

        moveDirection = ((Vector2)Player.transf.position - (Vector2)gameObject.transform.position).normalized;
    
    }

    private void ApplyMovement()
    {

        // Calculate X speed
        float targeSpeedX = moveDirection.x * moveSpeed;
        float speedDifX = targeSpeedX - rb.velocity.x;
        float accelRateX = (Mathf.Abs(targeSpeedX) > 0.01f) ? acceleration : decceleration;
        float moveX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

        // Calculate Y speed
        float targeSpeedY = moveDirection.y * moveSpeed;
        float speedDifY = targeSpeedY - rb.velocity.y;
        float accelRateY = (Mathf.Abs(targeSpeedY) > 0.01f) ? acceleration : decceleration;
        float moveY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);

        // Apply both speeds
        rb.AddForce(new Vector2(moveX, moveY) * Vector2.one);

    }

    private void FlipX()
    {

        if (moveDirection.x < 0)
        {
            spr.flipX = true;
        }
        else if (moveDirection.x > 0)
        {
            spr.flipX = false;
        }

    }

    #endregion
}
