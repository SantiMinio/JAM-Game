using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SASTools : EditorWindow
{
    [MenuItem("CustomTools/SAS custom tools")]
    public static void OpenWindow()
    {
        SASTools myWindow = (SASTools)GetWindow(typeof(SASTools));
        myWindow.Show();
        myWindow.minSize = new Vector2(300, 200);
        myWindow.maxSize = new Vector2(300, 200);
    }
    private void OnGUI()
    {
        EditorGUILayout.LabelField("Super Ant Squad tools", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Herramientas personalizadas para creacion de assets y niveles", EditorStyles.centeredGreyMiniLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open Level Editor", GUILayout.Height(45)))
        {
            LvlEditorWindow.OpenWindow();
        }
        if (GUILayout.Button("Open Tile Editor ", GUILayout.Height(45)))
        {
            TileEditorWindow.OpenWindow();
        }
        GUILayout.EndHorizontal();
    }
}
