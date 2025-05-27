using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LimitLightEffect : MonoBehaviour
{
    public float maxLightIntensity = 1f;

    void Update()
    {
        Light2D[] lights = FindObjectsByType<Light2D>(FindObjectsSortMode.None);

        foreach (Light2D light in lights)
        {
            float distance = Vector3.Distance(light.transform.position, transform.position);
            float intensity = Mathf.Clamp(light.intensity / (distance * distance), 0, maxLightIntensity);

            // Modify material emission to limit effect
            Renderer rend = GetComponent<Renderer>();
            if (rend)
            {
                rend.material.SetColor("_EmissionColor", Color.white * intensity);
            }
        }
    }
}