using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OffsetSortingOrderWindow : EditorWindow
{
    [SerializeField]
    Transform selectedObject;

    Transform[] children;
    List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    [SerializeField]
    static int offsetValue;

    [MenuItem("Window/Custom Tools/Offset Sorting Order")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(OffsetSortingOrderWindow));
    }

    private void OnInspectorUpdate()
    {
        GetSelectedObject();
        Repaint();
    }

    private void OnGUI()
    {
        UpdateWindowInfo();
    }

    void GetSelectedObject()
    {
        if (renderers != null)
            renderers.Clear();

        selectedObject = Selection.activeTransform;

        if (selectedObject != null)
        {
            if (selectedObject.childCount > 0)
            {
                children = new Transform[selectedObject.childCount];
                int i = 0;
                foreach (Transform child in selectedObject)
                {
                    children[i] = child;
                    i++;
                }
            }
        }
    }

    void UpdateWindowInfo()
    {
        if (selectedObject != null)
        {
            GUILayout.Space(5);
            GUI.enabled = false;
            GameObject obj = (GameObject)EditorGUILayout.ObjectField(selectedObject.gameObject, typeof(GameObject), true);
            GUI.enabled = true;
            GUILayout.Space(5);
            if (selectedObject.childCount == 0)
            {
                EditorGUILayout.HelpBox("This object doesn't have children.", MessageType.Info);
            }
            else
            {
                if (!GetSpriteRenderers())
                {
                    EditorGUILayout.HelpBox("There are no Sprite Renderers under this Parent", MessageType.Info);
                }
                else
                {
                    DrawFunctionality();
                }

            }
        }
        else
        {
            EditorGUILayout.HelpBox("Select an object in the scene.", MessageType.Info);
        }
    }

    bool GetSpriteRenderers()
    {
        foreach (Transform child in children)
        {
            SpriteRenderer spr = child.GetComponent<SpriteRenderer>();
            if (spr != null)
            {
                if(renderers.Count == 0)
                    renderers.Add(spr);
                else if (!renderers.Contains(spr))
                    renderers.Add(spr);
            }
        }

       // if (renderers == null)
       //     return false;
       // else
            return renderers.Count > 0 ? true : false;
    }

    Vector2 scrollPos;
    void DrawFunctionality()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        GUIStyle styleLeft = new GUIStyle();
        styleLeft.normal.textColor = Color.gray;
        styleLeft.fontSize = 10;

        GUIStyle styleRight = new GUIStyle(styleLeft);
        styleRight.alignment = TextAnchor.MiddleRight;

        EditorGUILayout.LabelField("Renderers", styleLeft, GUILayout.MaxWidth(130));
        GUILayout.Space(5);
        foreach (SpriteRenderer r in renderers)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(r.name, styleLeft, GUILayout.MaxWidth(130));
            EditorGUILayout.LabelField(r.sortingOrder.ToString(), styleRight, GUILayout.MaxWidth(20));
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        GUILayout.Space(5);

        offsetValue = EditorGUILayout.IntField("Order Offset", offsetValue);

        GUILayout.Space(5);

        if (GUILayout.Button("Apply"))
        {
            ApplyOffset(offsetValue);
        }
    }

    void ApplyOffset(int value)
    {
        foreach (SpriteRenderer r in renderers)
        {
            r.sortingOrder += value;
            EditorUtility.SetDirty(r);
        }
    }

}
