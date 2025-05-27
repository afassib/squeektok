using System;
using System.Collections;
using Bardent.ProjectileSystem.DataPackages;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Bardent.ProjectileSystem
{
    /// <summary>
    /// This class is the interface between projectile components and any entity that spawns a projectile.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        // This event is used to notify all projectile components that Init has been called
        public event Action OnInit;
        public event Action OnReset;
        public Light2D light2d;
        public float lightIntensity;
        private Coroutine fadeCoroutine; // Store the running coroutine

        public event Action<ProjectileDataPackage> OnReceiveDataPackage;

        public Rigidbody2D Rigidbody2D { get; private set; }

        public void Init()
        {
            OnInit?.Invoke();
        }

        public void Reset()
        {
            OnReset?.Invoke();
        }

        

        /* This function is called before Init from the weapon. Any weapon component can use this to function to pass along information that the projectile might need that is
        weapon specific, such as: damage amount, draw length modifiers, etc. */        
        public void SendDataPackage(ProjectileDataPackage dataPackage)
        {
            OnReceiveDataPackage?.Invoke(dataPackage);
        }

        #region Plumbing

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            lightIntensity = light2d.intensity;
        }

        #endregion

        #region lighting
        public void faidIn()
        {
            FadeLight(lightIntensity);
        }
        public void faidOut()
        {
            FadeLight(0);
        }

        public void FadeLight(float newvalue)
        {

            // If there's an active coroutine, stop it before starting a new one
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            // Start a new coroutine and store its reference
            fadeCoroutine = StartCoroutine(FadeLightCoroutine(light2d.intensity, newvalue, 0.5f));
        }

        private IEnumerator FadeLightCoroutine(float startIntensity, float endIntensity, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                light2d.intensity = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            light2d.intensity = endIntensity; // Ensure exact value at the end
            fadeCoroutine = null; // Clear reference when finished
        }

        #endregion
    }
}