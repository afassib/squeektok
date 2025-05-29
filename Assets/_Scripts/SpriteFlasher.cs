using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlasher : MonoBehaviour
{
    public Color colorA = Color.white;      // First flash color
    public Color colorB = Color.red;        // Second flash color
    public float flashInterval = 0.5f;      // Time between flashes in seconds

    private SpriteRenderer spriteRenderer;
    private Color initialColor;
    private bool isFlashing = false;
    private Coroutine flashCoroutine;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
            enabled = false;
            return;
        }

        initialColor = spriteRenderer.color;
    }

    public void StartFlashing()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            flashCoroutine = StartCoroutine(Flash());
        }
    }

    public void StopFlashing()
    {
        if (isFlashing)
        {
            isFlashing = false;
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);
            spriteRenderer.color = initialColor;
        }
    }

    private IEnumerator Flash()
    {
        bool toggle = false;

        while (isFlashing)
        {
            spriteRenderer.color = toggle ? colorA : colorB;
            toggle = !toggle;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
