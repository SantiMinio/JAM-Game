using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SavePopup : EditorWindow
{
    private LvlEditorWindow lvlWindow;
    private string Name;
    private void OnGUI()
    {
        EditorGUILayout.LabelField(Name, EditorStyles.boldLabel);
       Name= EditorGUILayout.TextField("Level Set 1");
        if (GUILayout.Button("Save"))
        {
            lvlWindow.SaveLvl(Name);
            Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }
    }
    public void OnSave(LvlEditorWindow window,string lvlName)
    {
        Name = lvlName;
        lvlWindow = window;
    }
}
