using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PrefabIntegratorAndVariantCreator : EditorWindow
{
    private List<GameObject> sourcePrefabs = new List<GameObject>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/Prefab Integrator and Variant Creator")]
    public static void ShowWindow()
    {
        GetWindow<PrefabIntegratorAndVariantCreator>("Prefab Integrator and Variant Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab Integrator and Variant Creator", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        Event evt = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag and Drop Source Prefabs Here");

        HandleDragAndDrop(dropArea, evt);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Source Prefabs:");
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));
        DisplaySelectedPrefabs();
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Integrated Prefab and Variants"))
        {
            CreateIntegratedPrefabAndVariants();
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
                            if (!sourcePrefabs.Contains(prefab))
                            {
                                sourcePrefabs.Add(prefab);
                            }
                        }
                    }
                }
                break;
        }
    }

    private void DisplaySelectedPrefabs()
    {
        for (int i = 0; i < sourcePrefabs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(sourcePrefabs[i], typeof(GameObject), false);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                sourcePrefabs.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void CreateIntegratedPrefabAndVariants()
    {
        if (sourcePrefabs.Count == 0)
        {
            EditorUtility.DisplayDialog("Error", "Please add source prefabs.", "OK");
            return;
        }

        // Create integrated prefab
        GameObject integratedInstance = new GameObject("IntegratedPrefab");

        foreach (var sourcePrefab in sourcePrefabs)
        {
            GameObject sourceInstance = (GameObject)PrefabUtility.InstantiatePrefab(sourcePrefab);
            IntegrateGameObjects(sourceInstance, integratedInstance);
            DestroyImmediate(sourceInstance);
        }

        string integratedPath = "Assets/IntegratedPrefab.prefab";
        GameObject integratedPrefab = PrefabUtility.SaveAsPrefabAsset(integratedInstance, integratedPath);
        DestroyImmediate(integratedInstance);

        Debug.Log($"Created integrated prefab: {integratedPath}");

        // Create variants
        for (int i = 0; i < sourcePrefabs.Count; i++)
        {
            var sourcePrefab = sourcePrefabs[i];
            string sourcePath = AssetDatabase.GetAssetPath(sourcePrefab);
            string variantPath = System.IO.Path.GetDirectoryName(sourcePath) + "/" +
                                 System.IO.Path.GetFileNameWithoutExtension(sourcePath) + "_Variant.prefab";

            GameObject variantInstance = (GameObject)PrefabUtility.InstantiatePrefab(integratedPrefab);
            GameObject sourceInstance = (GameObject)PrefabUtility.InstantiatePrefab(sourcePrefab);

            ApplyVariations(sourceInstance, variantInstance, i + 1);

            PrefabUtility.SaveAsPrefabAsset(variantInstance, variantPath);

            DestroyImmediate(variantInstance);
            DestroyImmediate(sourceInstance);

            Debug.Log($"Created variant {i + 1}: {variantPath}");
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", "Integrated prefab and variants have been created.", "OK");
    }

    private void IntegrateGameObjects(GameObject source, GameObject target)
    {
        // Integrate components
        foreach (Component sourceComp in source.GetComponents<Component>())
        {
            if (!(sourceComp is Transform)) // Skip Transform component
            {
                Component targetComp = target.GetComponent(sourceComp.GetType());
                if (targetComp == null)
                {
                    targetComp = target.AddComponent(sourceComp.GetType());
                }
                EditorUtility.CopySerialized(sourceComp, targetComp);
            }
        }

        // Integrate child objects
        foreach (Transform sourceChild in source.transform)
        {
            Transform targetChild = target.transform.Find(sourceChild.name);
            if (targetChild == null)
            {
                targetChild = new GameObject(sourceChild.name).transform;
                targetChild.SetParent(target.transform);
            }
            IntegrateGameObjects(sourceChild.gameObject, targetChild.gameObject);
        }
    }

    private void ApplyVariations(GameObject source, GameObject target, int variantNumber)
    {
        Debug.Log($"Applying variations for Variant {variantNumber}");

        // Copy all components from source to target
        CopyComponents(source, target, variantNumber);

        // Copy all child objects from source to target
        CopyChildObjects(source, target, variantNumber);

        // Apply transform properties
        target.transform.position = source.transform.position;
        target.transform.rotation = source.transform.rotation;
        target.transform.localScale = source.transform.localScale;

        Debug.Log($"Finished applying variations for Variant {variantNumber}");
    }

    private void CopyComponents(GameObject source, GameObject target, int variantNumber)
    {
        Component[] sourceComponents = source.GetComponents<Component>();
        foreach (Component sourceComp in sourceComponents)
        {
            Component targetComp = target.GetComponent(sourceComp.GetType());
            if (targetComp == null)
            {
                targetComp = target.AddComponent(sourceComp.GetType());
                Debug.Log($"Variant {variantNumber}: Added missing component {sourceComp.GetType()}");
            }
            EditorUtility.CopySerialized(sourceComp, targetComp);
            Debug.Log($"Variant {variantNumber}: Copied component {sourceComp.GetType()}");
        }

        // Remove components in target that don't exist in source
        Component[] targetComponents = target.GetComponents<Component>();
        foreach (Component targetComp in targetComponents)
        {
            if (!(targetComp is Transform) && !sourceComponents.Any(c => c.GetType() == targetComp.GetType()))
            {
                DestroyImmediate(targetComp);
                Debug.Log($"Variant {variantNumber}: Removed extra component {targetComp.GetType()}");
            }
        }
    }

    private void CopyChildObjects(GameObject source, GameObject target, int variantNumber)
    {
        // Copy child objects from source to target
        foreach (Transform sourceChild in source.transform)
        {
            Transform targetChild = target.transform.Find(sourceChild.name);
            if (targetChild == null)
            {
                targetChild = new GameObject(sourceChild.name).transform;
                targetChild.SetParent(target.transform);
                Debug.Log($"Variant {variantNumber}: Added missing child object {sourceChild.name}");
            }
            CopyComponents(sourceChild.gameObject, targetChild.gameObject, variantNumber);
            CopyChildObjects(sourceChild.gameObject, targetChild.gameObject, variantNumber);
        }

        // Remove child objects in target that don't exist in source
        List<Transform> childrenToRemove = new List<Transform>();
        foreach (Transform targetChild in target.transform)
        {
            if (source.transform.Find(targetChild.name) == null)
            {
                childrenToRemove.Add(targetChild);
            }
        }
        foreach (Transform child in childrenToRemove)
        {
            Debug.Log($"Variant {variantNumber}: Removed extra child object {child.name}");
            DestroyImmediate(child.gameObject);
        }
    }
}