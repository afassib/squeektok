using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PhysicsShapeGenerator : EditorWindow
{
    private Texture2D texture;
    private string assetPath;
    private Sprite[] sprites;

    [MenuItem("Tools/Physics Shape Generator")]
    public static void ShowWindow()
    {
        GetWindow<PhysicsShapeGenerator>("Physics Shape Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select a Texture2D with Sprites", EditorStyles.boldLabel);
        texture = (Texture2D)EditorGUILayout.ObjectField("Texture", texture, typeof(Texture2D), false);

        if (GUILayout.Button("Generate Physics Shapes"))
        {
            if (texture != null)
            {
                GeneratePhysicsShapes();
            }
            else
            {
                Debug.LogWarning("Please select a valid Texture2D.");
            }
        }
    }

    private void GeneratePhysicsShapes()
    {
        assetPath = AssetDatabase.GetAssetPath(texture);
        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogError("Invalid Texture Path.");
            return;
        }

        // Load only sprites from the texture asset
        sprites = AssetDatabase.LoadAllAssetsAtPath(assetPath)
            .OfType<Sprite>()
            .ToArray();

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("No sprites found in texture. Ensure Sprite Mode is set to Multiple and slices exist.");
            return;
        }

        foreach (var sprite in sprites)
        {
            GenerateShapeForSprite(sprite);
        }

        Debug.Log("Physics shapes generated and assigned to GameObjects.");
    }

    private void GenerateShapeForSprite(Sprite sprite)
    {
        if (sprite == null) return;

        // Create a new GameObject for the sprite
        GameObject spriteObject = new GameObject(sprite.name);
        spriteObject.transform.position = Vector3.zero;

        // Add SpriteRenderer and assign the sprite
        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        // Add PolygonCollider2D and generate physics shape
        PolygonCollider2D collider = spriteObject.AddComponent<PolygonCollider2D>();

        List<Vector2> shapePoints = GeneratePhysicsShape(sprite);

        if (shapePoints.Count > 0)
        {
            collider.SetPath(0, shapePoints.ToArray());
            Debug.Log($"Generated physics shape for {sprite.name}");
        }
        else
        {
            Debug.LogWarning($"No shape generated for {sprite.name}, possibly transparent.");
        }
    }

    private List<Vector2> GeneratePhysicsShape(Sprite sprite)
    {
        List<Vector2> shapePoints = new List<Vector2>();

        Texture2D spriteTexture = sprite.texture;
        int width = spriteTexture.width;
        int height = spriteTexture.height;

        Rect rect = sprite.rect;
        int xMin = Mathf.FloorToInt(rect.x);
        int yMin = Mathf.FloorToInt(rect.y);
        int xMax = Mathf.CeilToInt(rect.x + rect.width);
        int yMax = Mathf.CeilToInt(rect.y + rect.height);

        for (int y = yMin; y < yMax; y++)
        {
            for (int x = xMin; x < xMax; x++)
            {
                Color pixelColor = spriteTexture.GetPixel(x, y);
                if (pixelColor.a > 0.1f) // If not fully transparent
                {
                    Vector2 point = new Vector2(
                        (x - xMin) / rect.width,
                        (y - yMin) / rect.height
                    );
                    shapePoints.Add(point);
                }
            }
        }

        return shapePoints;
    }
}
