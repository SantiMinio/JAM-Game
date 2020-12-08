using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FirstWindow :EditorWindow
{
    [MenuItem("CustomTools/SAS custom tools")]
    public static void OpenWindow() 
    {                  
        FirstWindow myWindow = (FirstWindow)GetWindow(typeof(FirstWindow));       
        myWindow.Show();
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Open Level Editor Window"))
        {
            LvlEditorWindow.OpenWindow();
        }
    }
}
