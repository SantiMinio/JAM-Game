using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;


[CustomEditor(typeof(LevelSet))]
public class LevelSetEditor : Editor
{
    private LevelSet _lvlSet;
    private GUIStyle _labelStyle;
   
    private Editor _prev0;
    private Editor _prev1;
    private Editor _prev2; //tuve problemas creando un array asi que hice 3 previews separados
    
    private AnimBool _enableMore;
    
    private void OnEnable()
    {
        _lvlSet = (LevelSet)target;

        //creo un estilo propio y lo guardo
        _labelStyle = new GUIStyle();
        _labelStyle.fontStyle = FontStyle.Italic;
        _labelStyle.alignment = TextAnchor.MiddleCenter;

        _enableMore = new AnimBool(false); 
        _enableMore.valueChanged.AddListener(Repaint);
    }
    public override void OnInspectorGUI()
    {
        _lvlSet.name = EditorGUILayout.TextField("Level Name", _lvlSet.name);

        CreatePreview();
       

    }
    private void CreatePreview() //para elegir y ver los tiles a utilizar,estos se van cada uno al grupo correspondiente

    {
        EditorGUILayout.LabelField("Current Ground Tile");
        _lvlSet.groundTile = (GameObject)EditorGUILayout.ObjectField(_lvlSet.groundTile, typeof(GameObject), true);
        EditorGUILayout.LabelField("Current Wall Tile");
        _lvlSet.wallTile = (GameObject)EditorGUILayout.ObjectField(_lvlSet.wallTile, typeof(GameObject), true);
        EditorGUILayout.LabelField("Current Water Tile");
        _lvlSet.waterTile = (GameObject)EditorGUILayout.ObjectField(_lvlSet.waterTile, typeof(GameObject), true);
        GUIStyle myS = new GUIStyle();

        myS.normal.background = EditorGUIUtility.whiteTexture;

        _enableMore.target = EditorGUILayout.Toggle("Show Tiles Preview", _enableMore.target);
        if (EditorGUILayout.BeginFadeGroup(_enableMore.faded))
        {
             _lvlSet.prevSize = EditorGUILayout.Slider(_lvlSet.prevSize, 10, 250);

            if (_lvlSet.prevSize <= 80)
            {
                EditorGUILayout.BeginHorizontal();
            }
            if (_lvlSet.groundTile != null)
            {
                if (_prev0 == null)
                    _prev0 = CreateEditor(_lvlSet.groundTile);
                _prev0.OnPreviewGUI(GUILayoutUtility.GetRect(_lvlSet.prevSize,_lvlSet.prevSize), myS);
            }
            if (_lvlSet.wallTile != null)
            {
                if (_prev1 == null)
                    _prev1 = CreateEditor(_lvlSet.wallTile);
                _prev1.OnPreviewGUI(GUILayoutUtility.GetRect(_lvlSet.prevSize, _lvlSet.prevSize), myS);
            }
            if (_lvlSet.waterTile != null)
            {
                if (_prev2 == null)
                    _prev2 = CreateEditor(_lvlSet.waterTile);
                _prev2.OnPreviewGUI(GUILayoutUtility.GetRect(_lvlSet.prevSize, _lvlSet.prevSize), myS);
            }
            if (_lvlSet.prevSize <= 80)
            {
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndFadeGroup();
       
    }

    void propSelector()
    {
        
    }
}
