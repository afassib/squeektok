using Bardent.Interaction;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

namespace Bardent.Interaction.Interactables
{
    public class DialogueStarter : MonoBehaviour, IInteractable
    {
        [Header("Ink JSON")]
        public TextAsset inkJSON;
        [Header("Emote Animator")]
        [SerializeField] private Animator emoteAnimator;
        public Bobber Bobber;
        public UnityEvent onInteraction;
        public void DisableInteraction()
        {
            Bobber.StopBobbing();
            Bobber.gameObject.SetActive(false);
        }

        public void EnableInteraction()
        {
            Bobber.gameObject.SetActive(true);
            Bobber.StartBobbing();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Interact()
        {
            Debug.Log("Interact");
            onInteraction.Invoke();
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            
        }
    }
}
