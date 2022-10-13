using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
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

    [SerializeField]
    private float dashForce;

    [SerializeField]
    private float dashInterval;

    [SerializeField] 
    private float dashEffectInterval;

    [SerializeField]
    private SpriteRenderer dashIcon;

    [SerializeField]
    private GameObject dashEffectPrefab;


    // Movement
    [HideInInspector]
    public static Vector2 moveInput;

    // Dash
    private bool canDash = true;

    [HideInInspector]
    public static bool isDashing = false;

    private bool dashEffect = false;

    #region Engine Functions

    // Update is called once per frame
    private void Update()
    {

        if (Player.canPlay)
        {
            // Get Player´s Input
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // Dash Input
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                isDashing = true;
                StartCoroutine(SetDashInterval(dashInterval));
            }

            if (isDashing)
            {
                dashEffect = true;
                StartCoroutine(StopDashEffect(dashEffectInterval));
            }

            if (dashEffect)
            {
                GameObject dashEffect = Instantiate(dashEffectPrefab, Player.transf.position, Quaternion.identity);
                dashEffect.GetComponent<SpriteRenderer>().flipX = Player.spr.flipX;
            }

            Animate();
            FlipX();

        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    private void FixedUpdate()
    {
        if (Player.canPlay)
        {

            if (isDashing)
            {
                ApplyDash();
                isDashing = false;
            }
            else
            {
                ApplyMovement();
                ApplyFriction();
            }

        }
    }

    #endregion

    #region My Functions

    private void ApplyMovement()
    {

        // Calculate X speed
        float targeSpeedX = moveInput.x * moveSpeed;
        float speedDifX = targeSpeedX - Player.rb.velocity.x;
        float accelRateX = (Mathf.Abs(targeSpeedX) > 0.01f) ? acceleration : decceleration;
        float moveX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

        // Calculate Y speed
        float targeSpeedY = moveInput.y * moveSpeed;
        float speedDifY = targeSpeedY - Player.rb.velocity.y;
        float accelRateY = (Mathf.Abs(targeSpeedY) > 0.01f) ? acceleration : decceleration;
        float moveY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);

        // Apply both speeds
        Player.rb.AddForce(new Vector2(moveX, moveY) * Vector2.one);
    }

    private void ApplyFriction()
    {
        
        if (moveInput == Vector2.zero)
        {
            float amountX = Mathf.Min(Mathf.Abs(Player.rb.velocity.y), Mathf.Abs(frictionAmount));
            float amountY = Mathf.Min(Mathf.Abs(Player.rb.velocity.y), Mathf.Abs(frictionAmount));

            Vector2 amount = new Vector2(amountX * Mathf.Sign(Player.rb.velocity.x), amountY * Mathf.Sign(Player.rb.velocity.y));

            Player.rb.AddForce(Vector2.one * -amount, ForceMode2D.Impulse);
        }
        
    }

    private void FlipX()
    {
        if (MouseCursor.position.x < Player.transf.position.x)
        {
            Player.spr.flipX = true;
        } else if (MouseCursor.position.x > Player.transf.position.x)
        {
            Player.spr.flipX = false;
        }
    }

    private void Animate()
    {
        if (Mathf.Abs(moveInput.x) > 0 ||  Mathf.Abs(moveInput.y) > 0)
        {
            Player.anim.speed = Player.animSpeed;
        }
        else if (moveInput == Vector2.zero)
        {
            Player.anim.speed = 0f;
        }
    }

    private IEnumerator SetDashInterval(float interval)
    {
        canDash = false;
        yield return new WaitForSeconds(interval);
        dashIcon.flipX = Player.spr.flipX;
        dashIcon.enabled = true;
        Invoke("HideDashIcon", 0.8f);
        canDash = true;
    }

    private IEnumerator StopDashEffect(float interval)
    {
        yield return new WaitForSeconds(interval);
        dashEffect = false;
    }

    private void HideDashIcon()
    {
        dashIcon.enabled = false;
    }

    private void ApplyDash()
    {
        if (moveInput != Vector2.zero)
        {
            Player.rb.AddForce(moveInput * dashForce, ForceMode2D.Impulse);
        }
        else
        {
            Player.rb.AddForce(Vector2.down * dashForce, ForceMode2D.Impulse);
        }
        
    }

    #endregion
}
