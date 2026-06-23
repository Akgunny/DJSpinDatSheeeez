using UnityEngine;
using UnityEditor;
using System.IO;

public class TurntableSpriteGenerator
{
    [MenuItem("Tools/Generate Turntable Sprites")]
    public static void Generate()
    {
        GenerateCircle("LeftTurntable", new Color(0.2f, 0.6f, 1f), 256);
        GenerateCircle("RightTurntable", new Color(1f, 0.3f, 0.3f), 256);
        GenerateWave("WavePlayer", new Color(0.2f, 1f, 0.6f), 128, 48);
        GenerateObstacle("Obstacle", new Color(1f, 0.5f, 0f), 48, 48);
        AssetDatabase.Refresh();
        Debug.Log("Sprites generated.");
    }

    static void GenerateCircle(string name, Color color, int size)
    {
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        float radius = size / 2f;
        Vector2 center = new Vector2(radius, radius);

        for (int y = 0; y < size; y++)
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center);
                tex.SetPixel(x, y, dist <= radius ? color : Color.clear);
            }

        for (int i = 0; i < (int)radius; i++)
            tex.SetPixel((int)center.x + i, (int)center.y, Color.white);

        tex.Apply();
        SaveSprite(tex, "Assets/Sprites/DJBooth/" + name + ".png", Application.dataPath + "/Sprites/DJBooth/" + name + ".png");
        Object.DestroyImmediate(tex);
    }

    static void GenerateWave(string name, Color color, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int x = 0; x < width; x++)
        {
            float t = (float)x / width;
            float waveY = Mathf.Sin(t * Mathf.PI * 2f) * (height * 0.3f) + height / 2f;

            for (int y = 0; y < height; y++)
            {
                float dist = Mathf.Abs(y - waveY);
                tex.SetPixel(x, y, dist < 3f ? color : Color.clear);
            }
        }

        tex.Apply();
        SaveSprite(tex, "Assets/Sprites/Gameplay/" + name + ".png", Application.dataPath + "/Sprites/Gameplay/" + name + ".png");
        Object.DestroyImmediate(tex);
    }

    static void GenerateObstacle(string name, Color color, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                tex.SetPixel(x, y, color);

        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        Object.DestroyImmediate(tex);

        string fullPath = Application.dataPath + "/Sprites/Gameplay/" + name + ".png";
        string assetPath = "Assets/Sprites/Gameplay/" + name + ".png";
        File.WriteAllBytes(fullPath, bytes);
        AssetDatabase.ImportAsset(assetPath);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(assetPath);
        importer.textureType = TextureImporterType.Sprite;
        importer.spritePixelsPerUnit = 100;
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();
    }

    static void SaveSprite(Texture2D tex, string assetPath, string fullPath)
    {
        File.WriteAllBytes(fullPath, tex.EncodeToPNG());
        AssetDatabase.ImportAsset(assetPath);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(assetPath);
        importer.textureType = TextureImporterType.Sprite;
        importer.spritePixelsPerUnit = 100;
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();
    }
}
