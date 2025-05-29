using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FloatingCharactersTMP : MonoBehaviour
{
    public float amplitude = 5f;
    public float speed = 2f;

    private TextMeshProUGUI textComponent;
    private bool isAnimating = false;
    private Coroutine animationCoroutine;
    private float[] randomOffsets;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void OnDestroy()
    {
    }

    public void OnTextChanged()
    {
        PrepareOffsets();
    }

    public void StartAnimation()
    {
        if (isAnimating) return;
        isAnimating = true;
        PrepareOffsets();
        animationCoroutine = StartCoroutine(AnimateCharacters());
    }

    public void StopAnimation()
    {
        if (!isAnimating) return;
        isAnimating = false;
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
        ResetVertices();
    }

    private void PrepareOffsets()
    {
        textComponent.ForceMeshUpdate();

        int charCount = textComponent.textInfo.characterCount;
        int oldCount = (randomOffsets!=null) ? randomOffsets.Length : 0;
        float[] oldFloats = new float[oldCount];
        for (int i = 0; i < oldCount; i++)
        {
            oldFloats[i] = randomOffsets[i];
        }
        randomOffsets = new float[charCount];

        for (int i = 0; i < charCount; i++)
        {
            if(oldFloats.Length>i)
                randomOffsets[i] = oldFloats[i];
            else
                randomOffsets[i] = Random.Range(0f, Mathf.PI * 2);
        }
    }

    private void ResetVertices()
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            if (randomOffsets.Length <= i) continue;
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    private IEnumerator AnimateCharacters()
    {
        while (isAnimating)
        {
            TMP_TextInfo textInfo = textComponent.textInfo;
            if (textInfo.characterCount == 0)
            {
                yield return null;
                continue;
            }

            if (randomOffsets == null || randomOffsets.Length > textInfo.characterCount)
                PrepareOffsets();

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                float offsetY = Mathf.Sin(Time.time * speed + randomOffsets[Mathf.Min(i, randomOffsets.Length - 1)]) * amplitude;
                Vector3 offset = new Vector3(0, offsetY, 0);

                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] += offset;
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }

            yield return null;
        }
    }
}
