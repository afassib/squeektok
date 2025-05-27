using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PixelAccurateColliderGenerator : EditorWindow
{
    private Texture2D texture;

    [MenuItem("Tools/Pixel Accurate Collider Generator")]
    public static void ShowWindow()
    {
        GetWindow<PixelAccurateColliderGenerator>("Collider Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Generate PolygonColliders from Sprite Texture", EditorStyles.boldLabel);
        texture = (Texture2D)EditorGUILayout.ObjectField("Texture", texture, typeof(Texture2D), false);

        if (texture != null && GUILayout.Button("Generate Pixel-Accurate Colliders"))
        {
            GenerateColliders(texture);
        }
    }

    private void GenerateColliders(Texture2D texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);

        if (!importer.isReadable)
        {
            importer.isReadable = true;
            importer.SaveAndReimport();
        }

        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

        foreach (var obj in sprites)
        {
            if (obj is Sprite sprite)
            {
                CreateSpriteObjectWithCollider(sprite);
            }
        }

        Debug.Log("Collider generation completed.");
    }

    private void CreateSpriteObjectWithCollider(Sprite sprite)
    {
        GameObject go = new GameObject(sprite.name);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;

        PolygonCollider2D collider = go.AddComponent<PolygonCollider2D>();

        Texture2D tex = sprite.texture;
        Rect rect = sprite.rect;

        int startX = Mathf.FloorToInt(rect.x);
        int startY = Mathf.FloorToInt(rect.y);
        int width = Mathf.FloorToInt(rect.width);
        int height = Mathf.FloorToInt(rect.height);

        Color[] pixels = tex.GetPixels(startX, startY, width, height);
        bool[,] mask = new bool[width, height];
        float alphaThreshold = 0.1f;

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                mask[x, y] = pixels[y * width + x].a > alphaThreshold;

        List<Vector2> outline = MarchingSquares(mask, width, height);

        if (outline.Count < 3)
        {
            Debug.LogWarning($"Skipping {sprite.name}, not enough points for polygon.");
            return;
        }

        Vector2 pivot = sprite.pivot;
        float ppu = sprite.pixelsPerUnit;

        for (int i = 0; i < outline.Count; i++)
            outline[i] = (outline[i] - pivot) / ppu;

        collider.pathCount = 1;
        collider.SetPath(0, outline.ToArray());

        go.transform.position = Vector3.zero;
    }

    // === MARCHING SQUARES EDGE DETECTION ===
    private List<Vector2> MarchingSquares(bool[,] data, int width, int height)
    {
        List<Vector2> points = new List<Vector2>();
        bool[,] visited = new bool[width, height];

        Vector2Int start = FindFirstEdgePixel(data, width, height);
        if (start == -Vector2Int.one) return points;

        Vector2Int current = start;
        Vector2Int prev = Vector2Int.zero;

        do
        {
            Vector2Int next = NextEdgePixel(current, data, width, height, ref prev);
            points.Add(current);

            if (next == -Vector2Int.one || visited[next.x, next.y])
                break;

            visited[current.x, current.y] = true;
            current = next;

        } while (current != start);

        return points;
    }

    private Vector2Int FindFirstEdgePixel(bool[,] data, int width, int height)
    {
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                if (data[x, y] && HasTransparentNeighbor(data, x, y, width, height))
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return -Vector2Int.one;
    }

    private bool HasTransparentNeighbor(bool[,] data, int x, int y, int width, int height)
    {
        for (int j = -1; j <= 1; j++)
            for (int i = -1; i <= 1; i++)
                if (i != 0 || j != 0)
                    if (!data[x + i, y + j])
                        return true;
        return false;
    }

    private Vector2Int NextEdgePixel(Vector2Int current, bool[,] data, int width, int height, ref Vector2Int prevDir)
    {
        // 8 directions (clockwise)
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0),   // right
            new Vector2Int(1, 1),   // up-right
            new Vector2Int(0, 1),   // up
            new Vector2Int(-1, 1),  // up-left
            new Vector2Int(-1, 0),  // left
            new Vector2Int(-1, -1), // down-left
            new Vector2Int(0, -1),  // down
            new Vector2Int(1, -1),  // down-right
        };

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int dir = directions[i];
            int nx = current.x + dir.x;
            int ny = current.y + dir.y;

            if (nx >= 0 && ny >= 0 && nx < width && ny < height)
            {
                if (data[nx, ny] && HasTransparentNeighbor(data, nx, ny, width, height))
                {
                    prevDir = dir;
                    return new Vector2Int(nx, ny);
                }
            }
        }

        return -Vector2Int.one;
    }
}
