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
    public GameObject currentLevelSt;
    public bool toogleButtons = true;
    public Object LoadedLvlSet;
    public string levelName;
    public GUIStyle St;

    private List<GameObject> TilesList;
    public static void OpenWindow()
    {
        LvlEditorWindow instance = (LvlEditorWindow)GetWindow(typeof(LvlEditorWindow));
        instance.St = new GUIStyle();
        instance.Show();
        instance.minSize = new Vector2(400, 300);
    }
    private void OnGUI()
    {
        if (toogleButtons)
        {
            if (GUILayout.Button("New Level Set", GUILayout.Width(100), GUILayout.ExpandWidth(false), GUILayout.Height(55)))
            {
                if (GameObject.Find("LevelSet") || GameObject.Find(levelName))
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
                    levelName = g.name;
                    Debug.Log("se creo un Level set");
                    currentLevelSt = prefab;
                }
               
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Level Set", GUILayout.Width(100), GUILayout.ExpandWidth(false), GUILayout.Height(55)))
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
            EditorGUIUtility.labelWidth = 100;
            LoadedLvlSet = EditorGUILayout.ObjectField("Load Level Set", LoadedLvlSet, typeof(object), true);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Save Level Set", GUILayout.Width(100), GUILayout.ExpandWidth(false), GUILayout.Height(55)))
            {               
                var w = CreateInstance<SavePopup>();
                w.position = new Rect(Screen.width / 2, Screen.height / 2, 200, 100);
                w.ShowPopup();
                w.OnSave(this,levelName);
                if (currentLevelSt != null)
                {

                    if (AssetDatabase.Contains(currentLevelSt))
                    {
                        AssetDatabase.GetAssetPath(currentLevelSt);
                        PrefabUtility.SavePrefabAsset(currentLevelSt);
                    }
                    else
                    {

                        PrefabUtility.SaveAsPrefabAssetAndConnect(currentLevelSt, "Assets/Prefabs/LS/" + levelName, InteractionMode.UserAction);
                    }
                }
            }
        }
    }

    public void SaveLvl(string name)
    {
        PrefabUtility.SaveAsPrefabAssetAndConnect(currentLevelSt, "Assets/Prefabs/LS/" + name, InteractionMode.UserAction);
        Debug.Log("guardo nivel");
    }
    void TilesOptions()
    {

    }
}
