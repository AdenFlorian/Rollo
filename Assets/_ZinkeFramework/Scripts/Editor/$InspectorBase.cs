using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Hook all inspector objects
/// </summary>
[CustomEditor(typeof(Object))]
public class InspectorBase : Editor
{
    /// <summary>
    /// Hide the content for these extensions
    /// </summary>
    static string[] m_hideExtensions =
    {
        string.Empty,
        "scene",
    };

    /// <summary>
    /// Override what the inspector shows
    /// </summary>
    public override void OnInspectorGUI()
    {
        if (null == target)
        {
            return;
        }

        // Print the path of the created asset
        string assetPath = AssetDatabase.GetAssetPath(target);
        if (!string.IsNullOrEmpty(assetPath))
        {
            // get the file extension
            string extension = Path.GetExtension(assetPath);
            if (string.IsNullOrEmpty(extension))
            {
                extension = string.Empty;
            }
            else
            {
                extension = extension.ToLower();
            }

            // check if we can show the content
            bool showContent = true;
            foreach (string ext in m_hideExtensions)
            {
                if (ext.Equals(extension))
                {
                    showContent = false;
                    break;
                }
            }

            if (!Directory.Exists(assetPath) &&
                File.Exists(assetPath) &&
                showContent)
            {
                using (StreamReader sr = new StreamReader(assetPath))
                {
                    do
                    {
                        string line = sr.ReadLine();
                        if (null == line)
                        {
                            break;
                        }
                        else
                        {
                            GUILayout.Label(line);
                        }
                    }
                    while (true);
                }
            }
        }

        // show the deault inspector
        base.OnInspectorGUI();
    }
}