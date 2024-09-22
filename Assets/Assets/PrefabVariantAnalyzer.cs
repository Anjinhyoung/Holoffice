using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PrefabVariantCreator : EditorWindow
{
    private GameObject basePrefab;
    private List<GameObject> targetPrefabs = new List<GameObject>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/Prefab Variant Creator")]
    public static void ShowWindow()
    {
        GetWindow<PrefabVariantCreator>("Prefab Variant Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab Variant Creator", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        basePrefab = (GameObject)EditorGUILayout.ObjectField("Base Prefab", basePrefab, typeof(GameObject), false);

        EditorGUILayout.Space();

        Event evt = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag and Drop Target Prefabs Here");

        HandleDragAndDrop(dropArea, evt);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Target Prefabs:");
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));
        DisplaySelectedPrefabs();
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Variants"))
        {
            CreateVariants();
        }
    }

    private void HandleDragAndDrop(Rect dropArea, Event evt)
    {
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
                            if (!targetPrefabs.Contains(prefab))
                            {
                                targetPrefabs.Add(prefab);
                            }
                        }
                    }
                }
                break;
        }
    }

    private void DisplaySelectedPrefabs()
    {
        for (int i = 0; i < targetPrefabs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(targetPrefabs[i], typeof(GameObject), false);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                targetPrefabs.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void CreateVariants()
    {
        if (basePrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a base prefab.", "OK");
            return;
        }

        if (targetPrefabs.Count == 0)
        {
            EditorUtility.DisplayDialog("Error", "Please add target prefabs.", "OK");
            return;
        }

        foreach (var targetPrefab in targetPrefabs)
        {
            string basePath = AssetDatabase.GetAssetPath(basePrefab);
            string targetPath = AssetDatabase.GetAssetPath(targetPrefab);
            string variantPath = System.IO.Path.GetDirectoryName(targetPath) + "/" +
                                 System.IO.Path.GetFileNameWithoutExtension(targetPath) + "_Variant.prefab";

            // Create a new prefab variant based on the base prefab
            GameObject variantInstance = (GameObject)PrefabUtility.InstantiatePrefab(basePrefab);
            PrefabUtility.SaveAsPrefabAsset(variantInstance, variantPath);

            // Apply modifications from the target prefab to the new variant
            GameObject targetInstance = (GameObject)PrefabUtility.InstantiatePrefab(targetPrefab);

            ApplyModifications(targetInstance, variantInstance);

            PrefabUtility.SaveAsPrefabAsset(variantInstance, variantPath);

            // Final validation and application of missing changes
            ValidateAndApplyMissing(targetInstance, variantInstance);

            PrefabUtility.SaveAsPrefabAsset(variantInstance, variantPath);

            DestroyImmediate(variantInstance);
            DestroyImmediate(targetInstance);

            Debug.Log($"Created and validated variant: {variantPath}");
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", "Prefab variants have been created and validated.", "OK");
    }

    private void ApplyModifications(GameObject source, GameObject target)
    {
        // Apply transform modifications
        if (source.transform.position != target.transform.position)
            target.transform.position = source.transform.position;
        if (source.transform.rotation != target.transform.rotation)
            target.transform.rotation = source.transform.rotation;
        if (source.transform.localScale != target.transform.localScale)
            target.transform.localScale = source.transform.localScale;

        // Apply component modifications
        Component[] sourceComponents = source.GetComponents<Component>();
        foreach (Component sourceComp in sourceComponents)
        {
            Component targetComp = target.GetComponent(sourceComp.GetType());
            if (targetComp == null)
            {
                targetComp = target.AddComponent(sourceComp.GetType());
            }
            EditorUtility.CopySerialized(sourceComp, targetComp);
        }

        // Remove components that exist in target but not in source
        Component[] targetComponents = target.GetComponents<Component>();
        foreach (Component targetComp in targetComponents)
        {
            if (System.Array.Find(sourceComponents, comp => comp.GetType() == targetComp.GetType()) == null)
            {
                DestroyImmediate(targetComp);
            }
        }

        // Recursively apply modifications for child objects
        for (int i = 0; i < source.transform.childCount; i++)
        {
            Transform sourceChild = source.transform.GetChild(i);
            Transform targetChild = target.transform.Find(sourceChild.name);

            if (targetChild == null)
            {
                GameObject newChild = new GameObject(sourceChild.name);
                newChild.transform.SetParent(target.transform);
                targetChild = newChild.transform;
            }

            ApplyModifications(sourceChild.gameObject, targetChild.gameObject);
        }

        // Remove child objects that exist in target but not in source
        for (int i = target.transform.childCount - 1; i >= 0; i--)
        {
            Transform targetChild = target.transform.GetChild(i);
            if (source.transform.Find(targetChild.name) == null)
            {
                DestroyImmediate(targetChild.gameObject);
            }
        }
    }

    private void ValidateAndApplyMissing(GameObject source, GameObject target)
    {
        // Validate and apply missing components
        Component[] sourceComponents = source.GetComponents<Component>();
        Component[] targetComponents = target.GetComponents<Component>();

        foreach (Component sourceComp in sourceComponents)
        {
            Component targetComp = System.Array.Find(targetComponents, comp => comp.GetType() == sourceComp.GetType());
            if (targetComp == null)
            {
                targetComp = target.AddComponent(sourceComp.GetType());
                EditorUtility.CopySerialized(sourceComp, targetComp);
            }
            else
            {
                // Re-apply serialized properties to ensure all changes are captured
                EditorUtility.CopySerialized(sourceComp, targetComp);
            }
        }

        // Validate and apply missing child objects
        for (int i = 0; i < source.transform.childCount; i++)
        {
            Transform sourceChild = source.transform.GetChild(i);
            Transform targetChild = target.transform.Find(sourceChild.name);

            if (targetChild == null)
            {
                GameObject newChild = new GameObject(sourceChild.name);
                newChild.transform.SetParent(target.transform);
                targetChild = newChild.transform;
            }

            ValidateAndApplyMissing(sourceChild.gameObject, targetChild.gameObject);
        }

        // Ensure transform properties match
        target.transform.position = source.transform.position;
        target.transform.rotation = source.transform.rotation;
        target.transform.localScale = source.transform.localScale;
    }
}