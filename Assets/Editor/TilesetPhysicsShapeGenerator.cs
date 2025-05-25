using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class TilesetPhysicsShapeGenerator : EditorWindow
{
    [MenuItem("Tools/Generate Tileset Physics Shapes")]
    private static void GeneratePhysicsShapes()
    {
        Object[] selectedObjects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

        if (selectedObjects.Length == 0)
        {
            EditorUtility.DisplayDialog("Error", "Please select one or more tileset textures first.", "OK");
            return;
        }

        foreach (Object obj in selectedObjects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter == null)
                continue;

            // Configure texture importer settings
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Multiple;
            textureImporter.mipmapEnabled = false;
            textureImporter.filterMode = FilterMode.Point;
            textureImporter.spritePixelsPerUnit = 16;

            // Reimport to apply settings
            AssetDatabase.ImportAsset(path);

            // Load all sprites
            Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(path)
                .OfType<Sprite>()
                .ToArray();

            if (sprites.Length == 0)
            {
                Debug.LogWarning($"No sprites found in texture at {path}");
                continue;
            }

            // Create prefab folder
            string folderPath = System.IO.Path.GetDirectoryName(path) + "/PhysicsPrefabs";
            if (!AssetDatabase.IsValidFolder(folderPath))
                AssetDatabase.CreateFolder(System.IO.Path.GetDirectoryName(path), "PhysicsPrefabs");

            int processedCount = 0;

            foreach (Sprite sprite in sprites)
            {
                // Check existing physics shape
                List<Vector2> physicsShape = new List<Vector2>();
                int shapeCount = sprite.GetPhysicsShapeCount();

                if (shapeCount == 0 || sprite.GetPhysicsShape(0, physicsShape) == 0)
                {
                    // Generate custom physics shape
                    List<Vector2[]> newShapes = GenerateCustomPhysicsShape(sprite);
                    sprite.OverridePhysicsShape(newShapes);
                }
                else
                {
                    sprite.GetPhysicsShape(0, physicsShape);
                }

                // Create GameObject
                GameObject go = new GameObject(sprite.name);
                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;

                // Add and configure PolygonCollider2D
                PolygonCollider2D collider = go.AddComponent<PolygonCollider2D>();
                collider.points = physicsShape.ToArray();

                // Save as prefab
                string prefabPath = $"{folderPath}/{sprite.name}_physics.prefab";
                PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                DestroyImmediate(go);
                processedCount++;
            }

            // Save changes to the asset
            AssetDatabase.SaveAssets();
            Debug.Log($"Processed {processedCount} sprites from {path}");
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success",
            $"Finished generating physics shapes for {selectedObjects.Length} tileset(s)",
            "OK");
    }

    private static List<Vector2[]> GenerateCustomPhysicsShape(Sprite sprite)
    {
        List<Vector2[]> shapes = new List<Vector2[]>();

        // Option 1: Simple rectangular shape (default)
        Rect rect = sprite.rect;
        float ppu = sprite.pixelsPerUnit;
        Vector2 size = new Vector2(rect.width / ppu, rect.height / ppu);
        Vector2[] rectangle = new Vector2[]
        {
            new Vector2(-size.x/2, -size.y/2),
            new Vector2(size.x/2, -size.y/2),
            new Vector2(size.x/2, size.y/2),
            new Vector2(-size.x/2, size.y/2)
        };
        shapes.Add(rectangle);

        // Option 2: Uncomment below for alpha-based outline (more accurate but slower)
        /*
        Texture2D texture = sprite.texture;
        Color[] pixels = texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        List<Vector2> outline = GenerateOutlineFromSprite(pixels, rect, ppu);
        if (outline.Count >= 3) // Minimum points for a valid polygon
        {
            shapes.Clear();
            shapes.Add(outline.ToArray());
        }
        */

        return shapes;
    }

    private static List<Vector2> GenerateOutlineFromSprite(Color[] pixels, Rect rect, float ppu)
    {
        List<Vector2> outline = new List<Vector2>();

        for (int y = 0; y < rect.height; y++)
        {
            for (int x = 0; x < rect.width; x++)
            {
                int index = y * (int)rect.width + x;
                if (pixels[index].a > 0.1f) // Alpha threshold
                {
                    bool isEdge = (x == 0 || y == 0 || x == rect.width - 1 || y == rect.height - 1);
                    if (!isEdge)
                    {
                        int left = index - 1;
                        int right = index + 1;
                        int up = index - (int)rect.width;
                        int down = index + (int)rect.width;
                        isEdge = (pixels[left].a <= 0.1f || pixels[right].a <= 0.1f ||
                                 pixels[up].a <= 0.1f || pixels[down].a <= 0.1f);
                    }

                    if (isEdge)
                    {
                        Vector2 point = new Vector2(
                            (x - rect.width / 2) / ppu,
                            (y - rect.height / 2) / ppu
                        );
                        outline.Add(point);
                    }
                }
            }
        }

        // Simplify outline (optional)
        if (outline.Count > 3)
        {
            outline = SimplifyOutline(outline, 0.1f);
        }

        return outline;
    }

    private static List<Vector2> SimplifyOutline(List<Vector2> points, float tolerance)
    {
        if (points.Count <= 4) return points;

        List<Vector2> simplified = new List<Vector2> { points[0] };
        for (int i = 1; i < points.Count - 1; i++)
        {
            Vector2 prev = simplified[simplified.Count - 1];
            Vector2 curr = points[i];
            if (Vector2.Distance(prev, curr) > tolerance)
                simplified.Add(curr);
        }
        simplified.Add(points[points.Count - 1]);
        return simplified;
    }

    [MenuItem("Tools/Generate Tileset Physics Shapes", true)]
    private static bool ValidateGeneratePhysicsShapes()
    {
        return Selection.activeObject != null;
    }
}