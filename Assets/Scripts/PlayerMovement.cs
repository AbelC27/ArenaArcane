using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    [Header("Audio")]
    public AudioSource footstepSource; 
    public AudioClip footstepSound;    

    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCost = 30f;

    private bool isDashing = false;
    private float dashTimeLeft;

    public PlayerStamina playerStamina;

    void Update()
    {
        if (isDashing) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 0) 
        {
            if (footstepSource != null && !footstepSource.isPlaying)
            {
                footstepSource.clip = footstepSound;
                footstepSource.loop = true; 
                footstepSource.Play();
            }
        }
        else 
        {
            if (footstepSource != null && footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && movement != Vector2.zero)
        {
            AttemptDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.MovePosition(rb.position + movement.normalized * dashSpeed * Time.fixedDeltaTime);
            dashTimeLeft -= Time.fixedDeltaTime;
            if (dashTimeLeft <= 0) isDashing = false;
        }
        else
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void AttemptDash()
    {
        if (playerStamina != null && playerStamina.TryUseStamina(dashCost))
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
        }
    }
}