using Bardent;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Sprite activatedSprite;
    private SpriteRenderer spriteRenderer;
    private bool isPlayerOnMine = false;
    private Bomb bomb;
    [SerializeField] private Animator animator;
    [SerializeField] private MineData mineData;

    private void Awake()
    {
        bomb = GetComponentInChildren<Bomb>();
        if (animator==null)
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }
        if(mineData)
        {
            animator.runtimeAnimatorController = mineData.mineAnimatorController;
            bomb.animator.runtimeAnimatorController = mineData.bombAnimatorController;
            bomb.SetData(mineData);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        bomb.SetSpriteRenderer(spriteRenderer, animator);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnMine = true;
            if (activatedSprite != null)
                spriteRenderer.sprite = activatedSprite;

            // Optional: play animation instead of sprite swap
            animator.Play("BombMineTransition");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isPlayerOnMine)
        {
            isPlayerOnMine = false;
            bomb.ActivateBomb(other.gameObject); // Pass the player GameObject
        }
    }
}
