using Bardent.Assets._Scripts.Bosses;
using Bardent.CoreSystem;
using Bardent.Interaction;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Bardent
{
    public class BossWaker : CoreComponent, IInteractable
    {

        public UnityEvent StartInteracting;
        public UnityEvent StopInteracting;
        private Vector3 initialScale;
        private Vector3 initialPosition;
        [SerializeField] private BossBase boss;

        public void DisableInteraction()
        {
            StopInteracting.Invoke();
        }

        public void EnableInteraction()
        {
            StartInteracting.Invoke();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Interact()
        {
            // Wake enemy
            transform.DOKill();
            ResetScaleAndPositoin();
            transform.DOShakeScale(0.5f, 0.1f, 10, 90, true).OnComplete(
            () =>
            {
                boss?.ReceiveMessage(BossMessage.WakeBoss);
            }
            );
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            initialPosition = transform.position;
            initialScale = transform.localScale;
        }

        private void ResetScaleAndPositoin()
        {
            transform.position = initialPosition;
            transform.localScale = initialScale;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
