using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class NewLevelAlert : EditorWindow
{
    private LvlEditorWindow Lvlwindow;
    private GameObject currentLvl=default;
    private void OnGUI()
    {
        EditorGUILayout.LabelField("  A LevelSet already exists", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Do you want to overwrite it?", EditorStyles.boldLabel);

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Yes", GUILayout.Height(30)))
        {
            var b = GameObject.Find("LevelSet");
            DestroyImmediate(b);
            if (currentLvl == null)
            {
                GameObject prefab = PrefabUtility.LoadPrefabContents("Assets/Editor/LevelSet.prefab");
                var g = Instantiate(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                g.name = "LevelSet";
                currentLvl = prefab;
            }
            else
            {
                Instantiate(currentLvl, Vector3.zero, Quaternion.Euler(Vector3.zero));
            }
            Lvlwindow.currentLevelSt = currentLvl;
            Lvlwindow.toogleButtons = true;
            Close();
        }
        if (GUILayout.Button("No", GUILayout.Height(30)))
        {
            Lvlwindow.toogleButtons = true;
            Close();
        }
    }
    public void BlockWindow(LvlEditorWindow window, Object LoadedLvl)
    {
        window.toogleButtons = false;
        Lvlwindow = window;
    }


}
