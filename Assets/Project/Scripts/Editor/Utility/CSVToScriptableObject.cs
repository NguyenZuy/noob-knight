using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Zuy.NoobKnight.Animated;

namespace Zuy.NoobKnight.Editor
{
    public class CSVToScriptableObject : MonoBehaviour
    {
        protected static string m_MATERIAL_PATH = "Assets/Project/Materials/Character/";
        protected static string m_TEXTURE_SHEET_PATH = "Assets/Project/Art/Character/";

        [MenuItem("Tools/Parse CSV to ScriptableObject")]
        public static void ParseCSV()
        {
            string path = EditorUtility.OpenFilePanel("Select CSV file", "", "csv");
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("No CSV file selected.");
                return;
            }

            // Open the file with shared read access
            string[] lines;
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(fileStream))
            {
                var fileContent = reader.ReadToEnd();
                lines = fileContent.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            }

            // Skip the first line (header)
            for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex];
                string[] values = line.Split(','); // Assuming comma-separated values

                if (values.Length <= 0)
                {
                    Debug.LogError("Invalid CSV format.");
                    return;
                }

                byte typeCharacter;
                if (!byte.TryParse(values[0], out typeCharacter))
                {
                    Debug.LogError("Invalid typeCharacter value.");
                    return;
                }

                string materialPath = m_MATERIAL_PATH + values[1];
                Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                if (material == null)
                {
                    Debug.LogError($"Material not found at path: {materialPath}");
                    return;
                }

                List<AnimatedTextureSheet> animatedTextureSheets = new List<AnimatedTextureSheet>();

                for (int i = 0; i < 3; i++)
                {
                    int baseIndex = 2 + i * 3;

                    byte typeAnimation;
                    if (!byte.TryParse(values[baseIndex], out typeAnimation))
                    {
                        Debug.LogError($"Invalid typeAnimation_{i} value.");
                        return;
                    }

                    float speed;
                    if (!float.TryParse(values[baseIndex + 1], out speed))
                    {
                        Debug.LogError($"Invalid speed_{i} value.");
                        return;
                    }

                    string textureSheetPath = m_TEXTURE_SHEET_PATH + typeCharacter + "/" +  values[baseIndex + 2];
                    Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(textureSheetPath);
                    if (texture == null)
                    {
                        Debug.LogError($"Texture not found at path: {textureSheetPath}");
                        return;
                    }

                    animatedTextureSheets.Add(new AnimatedTextureSheet()
                    {
                        typeAnimation = typeAnimation,
                        speed = speed,
                        textureSheet = texture
                    });
                }

                var newSO = ScriptableObject.CreateInstance<AnimatedCollectionSO>();
                newSO.typeCharacter = typeCharacter;
                newSO.material = material;
                newSO.animatedTextureSheets = animatedTextureSheets;

                string assetPath = $"Assets/Project/ScriptableObjects/{typeCharacter}_AnimatedCollectionSO.asset";
                AssetDatabase.CreateAsset(newSO, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("CSV parsing and ScriptableObject creation completed.");
        }
    }
}
