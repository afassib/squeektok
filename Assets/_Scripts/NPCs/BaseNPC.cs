using UnityEngine;
using System.Collections;
using Bardent.Interaction;
using Bardent;

public class BaseNPC : MonoBehaviour
{
    [SerializeField] protected bool isAwake = true;
    [SerializeField] protected bool hasAnimations = false;
    [SerializeField] protected bool hasTweening = false;
    [SerializeField] protected GameObject coreObject;
    [SerializeField] protected GameObject interactionIndicator;
    [SerializeField] protected Bobber interactionBobber;
    [SerializeField] protected Object ink;
    private Player player;

    // Constructor (empty for now)
    public BaseNPC()
    {
        // TODO: Initialize with parameters when needed
    }

    // Unity lifecycle methods
    protected virtual void Awake()
    {
        // Initialization logic here
    }

    protected virtual void Start()
    {
        player = Player.instance;
        // Logic to run at start
    }

    protected virtual void Update()
    {
        // Per-frame logic here
    }

    // Conversation logic
    public virtual void StartConversation()
    {
        if (!isAwake)
        {
            Debug.Log($"{name} is sleeping and cannot talk.");
            return;
        }

        // TODO: Start conversation logic
        MakeEyeContactWithPlayer();
        Debug.Log($"{name} started a conversation.");
    }

    public virtual void OnConversationEnded()
    {
        // Called when the conversation ends
        Debug.Log($"{name} ended the conversation.");
    }

    // Wake and Sleep state management
    public virtual void Wake()
    {
        if (isAwake) return;
        StartCoroutine(WakeCoroutine());
    }

    public virtual void Sleep()
    {
        if (!isAwake) return;
        isAwake = false;
        StartCoroutine(SleepCoroutine());
    }

    // Coroutines for animation/tween logic
    protected virtual IEnumerator WakeCoroutine()
    {
        // TODO: Play wake-up animation or tween here
        Debug.Log($"{name} is waking up...");
        yield return null;
        isAwake = true;
    }

    protected virtual IEnumerator SleepCoroutine()
    {
        // TODO: Play sleep animation or tween here
        Debug.Log($"{name} is going to sleep...");
        yield return null;
    }

    // Utility
    public bool IsAwake()
    {
        return isAwake;
    }

    public void MakeEyeContactWithPlayer()
    {
        // TODO call method on player by passing this object
        player.LookToObject(gameObject);

    }

    public void EnableInteraction()
    {
        interactionIndicator?.SetActive( true );
        interactionBobber?.StartBobbing();
    }

    public void DisableInteraction()
    {
        interactionBobber?.StopBobbing();
        interactionIndicator?.SetActive( false );
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual void Interact()
    {
        //
    }
}
