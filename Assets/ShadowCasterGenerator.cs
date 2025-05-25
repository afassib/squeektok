using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;

[RequireComponent(typeof(CompositeCollider2D))]
[ExecuteInEditMode] // Runs in editor mode
public class ShadowCasterGenerator : MonoBehaviour
{
    private CompositeCollider2D compositeCollider;
    private ShadowCaster2D shadowCaster;

    void Awake()
    {
        GenerateShadowCaster();
    }

    [ContextMenu("Generate Shadow Caster")] // Right-click in Inspector to run
    public void GenerateShadowCaster()
    {
        // Get required components
        compositeCollider = GetComponent<CompositeCollider2D>();

        if (compositeCollider == null)
        {
            Debug.LogError("No CompositeCollider2D found!");
            return;
        }

        // Check if a Shadow Caster exists, otherwise create one
        shadowCaster = GetComponent<ShadowCaster2D>();
        if (shadowCaster == null)
            shadowCaster = gameObject.AddComponent<ShadowCaster2D>();

        // ❌ Clear previous shape path before generating new one
        SetShadowCasterShapePath(shadowCaster, new Vector3[0]);

        // Get the Composite Collider's world position offset
        Vector3 colliderOffset = compositeCollider.offset;

        // Extract points from Composite Collider
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < compositeCollider.pathCount; i++)
        {
            Vector2[] pathPoints = new Vector2[compositeCollider.GetPathPointCount(i)];
            compositeCollider.GetPath(i, pathPoints);

            foreach (Vector2 point in pathPoints)
            {
                // Convert from world space to local space
                Vector3 localPoint = transform.InverseTransformPoint(point);

                // 🔥 Fix position offset by subtracting the collider's offset
                localPoint -= colliderOffset;

                points.Add(localPoint);
            }
        }

        // Convert to Vector3 array for Shadow Caster
        Vector3[] shadowPoints = points.ToArray();

        // Use reflection to modify the private shapePath field
        SetShadowCasterShapePath(shadowCaster, shadowPoints);

        // Enable self-shadows
        shadowCaster.selfShadows = true;

        Debug.Log("✅ Shadow Caster 2D generated successfully with corrected position!");
    }

    private void SetShadowCasterShapePath(ShadowCaster2D caster, Vector3[] points)
    {
        // Use reflection to access the private field "m_ShapePath"
        FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
        if (shapePathField != null)
        {
            shapePathField.SetValue(caster, points);
        }
        else
        {
            Debug.LogError("❌ Failed to set Shadow Caster shape path: Field not found.");
        }
    }
}
