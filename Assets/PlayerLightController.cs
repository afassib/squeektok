using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for 2D Light

public class PlayerLightController : MonoBehaviour
{
    public Light2D playerLight; // Reference to the player's Light2D component
    public float lightThreshold = 0.1f; // Minimum required light intensity from external sources
    public float checkRadius = 2f; // Radius to check for external light sources
    public LayerMask lightLayer; // Layer for detecting light sources

    private void Update()
    {
        float totalLight = CalculateSurroundingLight();

        if (totalLight < lightThreshold)
        {
            playerLight.intensity = 0.3f; // Activate player light
        }
        else
        {
            playerLight.intensity = 0f; // Deactivate player light
        }
    }

    private float CalculateSurroundingLight()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius, lightLayer);
        float totalLight = 0f;

        foreach (Collider2D col in colliders)
        {
            Light2D externalLight = col.GetComponent<Light2D>();
            if (externalLight != null)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                float attenuation = 1f - (distance / externalLight.pointLightOuterRadius); // Simulate light falloff
                totalLight += externalLight.intensity * Mathf.Clamp01(attenuation);
            }
        }

        return totalLight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
