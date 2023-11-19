using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostMultiplier = 2f; // Adjust this value to set the boost multiplier
    public float boostDuration = 5f;   // Boost duration in seconds
    public float boostCooldown = 10f;  // Cooldown duration in seconds
    public float pushForce = 10f;      // Force applied to push other players
    public float pushDuration = 0.5f;  // Duration of the push force
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode boostKey;
    public KeyCode pushKey; // New key for pushing

    private Rigidbody2D rb;
    private float currentSpeed;
    private float boostTimer;
    private float cooldownTimer;
    private float pushTimer;
    private bool isBoostOnCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
        boostTimer = 0f;
        cooldownTimer = 0f;
        pushTimer = 0f;
        isBoostOnCooldown = false;
    }

    void Update()
    {
        HandleBoost();
        MovePlayer();

        if (Input.GetKeyDown(pushKey))
        {
            StartPush();
        }

        if (pushTimer > 0f)
        {
            ApplyPushForce();
        }
    }

    void HandleBoost()
    {
        if (isBoostOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isBoostOnCooldown = false;
                cooldownTimer = 0f;
            }
        }
        else if (Input.GetKeyDown(boostKey))
        {
            // Start the boost
            currentSpeed = moveSpeed * boostMultiplier;
            boostTimer = boostDuration;
            isBoostOnCooldown = true;
            cooldownTimer = boostCooldown; // Set cooldown timer when boost is used
        }
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetKey(leftKey) ? -1f : Input.GetKey(rightKey) ? 1f : 0f;
        float verticalInput = Input.GetKey(upKey) ? 1f : Input.GetKey(downKey) ? -1f : 0f;

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Check for boost duration
        if (boostTimer > 0f)
        {
            rb.velocity = movement.normalized * currentSpeed;
            boostTimer -= Time.deltaTime;
        }
        else
        {
            // If not boosting or on cooldown, move at normal speed
            rb.velocity = movement.normalized * moveSpeed;
        }
    }

    void StartPush()
    {
        pushTimer = pushDuration;
    }

    void ApplyPushForce()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f); // Adjust the radius as needed

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") && collider.gameObject != gameObject)
            {
                // Apply force to push the other player away
                Vector2 pushDirection = (collider.transform.position - transform.position).normalized;
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce * Time.deltaTime, ForceMode2D.Impulse);
            }
        }

        pushTimer -= Time.deltaTime;

        if (pushTimer <= 0f)
        {
            pushTimer = 0f;
        }
    }
}