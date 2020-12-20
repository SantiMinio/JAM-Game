using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileEditorWindow : EditorWindow
{
    
    CustomTile currentTile;
    private bool tileLoaded;
    private Mesh mesh;
    
    public enum TileTipe {Wall,Ground,Water,Deco,Other}

    public Color color1;
    public Color color2;
    public Color color3;

    private void OnGUI()
    {
        currentTile = (CustomTile)EditorGUILayout.ObjectField("Customize Tile", currentTile, typeof(CustomTile), true);
        currentTile = ;
    }
    void ShowTileOptions()
    {
        EditorGUILayout.LabelField("Mesh Size: " + mesh.bounds.size);
    }
}
