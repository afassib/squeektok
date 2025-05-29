using System.Collections;
using Bardent;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bomb : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private MineData mineData;
    [SerializeField] private Light2D lightComponent;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator srAnimator;

    public void SetData(MineData mineData)
    {
        this.mineData = mineData;
    }

    private bool hasExploded = false;
    [SerializeField] private GameObject parentMine;

    private void Awake()
    {
        if (lightComponent == null) lightComponent = transform.GetChild(0).GetComponent<Light2D>();
        if(parentMine==null) parentMine = transform.parent.gameObject;
    }
    public void ActivateBomb(GameObject player)
    {
        if (hasExploded) return;
        hasExploded = true;
        if (mineData.activationTime > 0)
        {
            srAnimator.Play("BombMinePreExplosion");
            DOVirtual.Float(0f, 0.8f, mineData.activationTime, value =>
            {
                lightComponent.pointLightOuterRadius = value;
            }).OnComplete(() =>
            {
                lightComponent.pointLightOuterRadius = 0;
                Explode();
            });
        }

    }

    private void Explode()
    {
        StartCoroutine(PlayFeedbacks());

        // Apply damage to the player


        // Optional: Destroy bomb after animation delay
    }

    IEnumerator PlayFeedbacks()
    {
        Destroy(sr.gameObject);
        sr.sprite = null;
        yield return new WaitForSeconds(mineData.subFeedBackDelay);
        FeedbackLibrary.Instance.Play(mineData.feedBackName, 
            new FeedbackContext
            {
                audioClip = mineData.feedBackSound,
                intensity = mineData.feedBackEntensity,

            });

        yield return new WaitForSeconds(mineData.postFeedBackDelay);
        FeedbackLibrary.Instance.Play("FlashFeedback");
        if (animator != null)
        {
            animator.Play("Explosion");
        }
    }

    public void DestroyAfterAnimation()
    {
        Destroy(parentMine);
    }

    internal void SetSpriteRenderer(SpriteRenderer sr, Animator srAnimator)
    {
        this.sr = sr;
        this.srAnimator = srAnimator;
    }
}
