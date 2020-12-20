using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SASTools :EditorWindow
{
    [MenuItem("CustomTools/SAS custom tools")]
    public static void OpenWindow() 
    {                  
        SASTools myWindow = (SASTools)GetWindow(typeof(SASTools));       
        myWindow.Show();
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Open Level Editor Window"))
        {
            LvlEditorWindow.OpenWindow();
        }
        if(GUILayout.Button("Open Tile Editor Window"))
        {

        }
    }
}
