using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLightFlicker : MonoBehaviour
{
    public Light2D fireLight;
    public float intensityOffset = 0.2f;
    public float flickerSpeed = 0.1f;
    public float positionVariation = 0.05f;
    public float positionVariationSpeed = 1.0f;

    private Vector3 initialPosition;
    private float initialIntensity;
    private float timeOffset;

    void Start()
    {
        if (fireLight == null)
        {
            fireLight = GetComponent<Light2D>();
        }
        initialPosition = transform.position;
        initialIntensity = fireLight.intensity;
        timeOffset = Random.value * 10f; // Ensures different flicker timing for multiple lights
    }

    void Update()
    {
        // Flicker intensity
        fireLight.intensity = Mathf.Lerp(initialIntensity - intensityOffset, initialIntensity + intensityOffset, Mathf.PerlinNoise(Time.time * flickerSpeed, timeOffset));

        // Slight movement
        transform.position = initialPosition + new Vector3(
            Mathf.PerlinNoise(Time.time * positionVariationSpeed, timeOffset) * positionVariation - (positionVariation / 2),
            Mathf.PerlinNoise(Time.time * positionVariationSpeed, timeOffset + 1) * positionVariation - (positionVariation / 2),
            0 // Keep Z constant for 2D
        );
    }
}