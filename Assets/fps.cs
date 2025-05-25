using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fps : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private List<float> frameTimes = new List<float>();
    private const int sampleSize = 100;

    void Update()
    {
        float currentFPS = 1f / Time.unscaledDeltaTime;
        frameTimes.Add(currentFPS);
        if (frameTimes.Count > sampleSize)
            frameTimes.RemoveAt(0);

        frameTimes.Sort();
        float fps1Percent = frameTimes[Mathf.Max(0, frameTimes.Count * 99 / 100)];
        float fps0_1Percent = frameTimes[Mathf.Max(0, frameTimes.Count * 999 / 1000)];

        fpsText.text = $"FPS: {currentFPS:F1}\n1% Low: {fps1Percent:F1}\n0.1% Low: {fps0_1Percent:F1}";
    }
}