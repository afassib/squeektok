using UnityEngine;

public class JumpGear : MonoBehaviour
{
    public float jumpForce = 10f; // Force applied to the player
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0); // Reset vertical velocity
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (animator != null)
            {
                animator.Play("Jump", -1, 0); // Trigger the animation
            }
        }
    }
}
