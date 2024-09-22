using UnityEngine;
using UnityEditor;
using TMPro;
using System.Collections.Generic;

public class TextMeshProFontChanger : EditorWindow
{
    private List<GameObject> selectedPrefabs = new List<GameObject>();
    private TMP_FontAsset newFont;

    [MenuItem("Tools/TextMeshPro Font Changer")]
    public static void ShowWindow()
    {
        GetWindow<TextMeshProFontChanger>("TextMeshPro Font Changer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Prefabs to Modify", EditorStyles.boldLabel);

        // Drag and drop area for prefabs
        Event evt = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag and Drop Prefabs Here");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        if (draggedObject is GameObject prefab && PrefabUtility.IsPartOfPrefabAsset(prefab))
                        {
                            selectedPrefabs.Add(prefab);
                        }
                    }
                }
                break;
        }

        // Display selected prefabs
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Selected Prefabs:");
        for (int i = 0; i < selectedPrefabs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(selectedPrefabs[i], typeof(GameObject), false);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                selectedPrefabs.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        // New font selection
        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font", newFont, typeof(TMP_FontAsset), false);

        // Change font button
        if (GUILayout.Button("Change Font"))
        {
            ChangeFonts();
        }
    }

    private void ChangeFonts()
    {
        if (newFont == null)
        {
            EditorUtility.DisplayDialog("Error", "Please select a new font.", "OK");
            return;
        }

        foreach (GameObject prefab in selectedPrefabs)
        {
            // Get the asset path of the prefab
            string assetPath = AssetDatabase.GetAssetPath(prefab);

            // Load the prefab asset
            GameObject prefabAsset = PrefabUtility.LoadPrefabContents(assetPath);

            // Find all TextMeshPro components in the prefab
            TextMeshProUGUI[] textComponents = prefabAsset.GetComponentsInChildren<TextMeshProUGUI>(true);

            foreach (TextMeshProUGUI textComponent in textComponents)
            {
                Undo.RecordObject(textComponent, "Change Font");
                textComponent.font = newFont;
            }

            // Save the modified prefab
            PrefabUtility.SaveAsPrefabAsset(prefabAsset, assetPath);

            // Unload the prefab contents
            PrefabUtility.UnloadPrefabContents(prefabAsset);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Success", "Fonts have been changed in all selected prefabs.", "OK");
    }
}