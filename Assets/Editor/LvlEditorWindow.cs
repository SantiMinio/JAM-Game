using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using System;
using Object = UnityEngine.Object;

public class LvlEditorWindow : EditorWindow
{
    public bool toogleButtons = true;
    public Object LoadedLvlSet;

    public GUIStyle St;
    private bool _hasLvlSet;
    private List<GameObject> TilesList;
    public static void OpenWindow()
    {
        LvlEditorWindow instance = (LvlEditorWindow)GetWindow(typeof(LvlEditorWindow));
        instance.St = new GUIStyle();
        instance.Show();
    }
    private void OnGUI()
    {
        if (toogleButtons)
        {
            if (GUILayout.Button("New Level Set", GUILayout.Width(150), GUILayout.ExpandWidth(false), GUILayout.Height(75)))
            {
                if (GameObject.Find("LevelSet"))
                {
                    var w = CreateInstance<NewLevelAlert>();
                    w.position = new Rect(Screen.width / 2, Screen.height / 2, 200, 100);
                    w.ShowPopup();
                    w.BlockWindow(this, null);
                }
                else
                {
                    GameObject prefab = PrefabUtility.LoadPrefabContents("Assets/Editor/LevelSet.prefab");
                    var g = Instantiate(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                    g.name = "LevelSet";
                    Debug.Log("se creo un Level set");
                }
                _hasLvlSet = true;
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Level Set", GUILayout.Width(150), GUILayout.ExpandWidth(false), GUILayout.Height(75)))
            {
                if (LoadedLvlSet)
                {
                    var w = CreateInstance<NewLevelAlert>();
                    w.position = new Rect(Screen.width / 2, Screen.height / 2, 200, 100);
                    w.ShowPopup();
                    w.BlockWindow(this, LoadedLvlSet);
                }
                else
                {
                    Debug.Log("Load Level field is empty");
                }
            }
            LoadedLvlSet = EditorGUILayout.ObjectField("Level Set", LoadedLvlSet, typeof(object), true);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Save Level Set", GUILayout.Width(150), GUILayout.ExpandWidth(false), GUILayout.Height(75)))
            {

            }
        }
    }

    void TilesOptions()
    {

    }
}
