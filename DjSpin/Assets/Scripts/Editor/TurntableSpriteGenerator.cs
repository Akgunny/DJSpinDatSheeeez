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
        AssetDatabase.Refresh();
        Debug.Log("Turntable sprites generated in Assets/Sprites/DJBooth/");
    }

    static void GenerateCircle(string name, Color color, int size)
    {
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        float radius = size / 2f;
        Vector2 center = new Vector2(radius, radius);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center);
                float alpha = Mathf.Clamp01(radius - dist);
                tex.SetPixel(x, y, dist <= radius ? color : Color.clear);
            }
        }

        // Draw a simple line marker so you can see it spinning
        for (int i = 0; i < (int)radius; i++)
            tex.SetPixel((int)center.x + i, (int)center.y, Color.white);

        tex.Apply();

        string path = "Assets/Sprites/DJBooth/" + name + ".png";
        File.WriteAllBytes(Application.dataPath + "/Sprites/DJBooth/" + name + ".png", tex.EncodeToPNG());
        Object.DestroyImmediate(tex);

        AssetDatabase.ImportAsset(path);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
        importer.textureType = TextureImporterType.Sprite;
        importer.spritePixelsPerUnit = 100;
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();
    }
}
