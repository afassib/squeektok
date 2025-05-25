using UnityEngine;
using UnityEngine.UI;

public class InfiniteUITexture : MonoBehaviour
{
    public RawImage rawImage;
    public Vector2 scrollSpeed = new Vector2(0.1f, 0.1f); // Speed of the texture movement

    void Update()
    {
        // Move the UV Rect to create the illusion of an infinite background
        rawImage.uvRect = new Rect(rawImage.uvRect.position + scrollSpeed * Time.deltaTime, rawImage.uvRect.size);
    }
}
