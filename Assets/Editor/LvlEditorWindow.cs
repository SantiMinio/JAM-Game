using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LvlEditorWindow : EditorWindow
{
    public GUIStyle St;
    public static void OpenWindow()
    {
        LvlEditorWindow instance = (LvlEditorWindow)GetWindow(typeof(LvlEditorWindow));
        instance.St = new GUIStyle();
        instance.Show();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("New Level Set",GUILayout.Width(150), GUILayout.ExpandWidth(false), GUILayout.Height(75)))
        {

        } 

    }
}
