using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileEditorWindow : EditorWindow
{
    
    CustomTile currentTile;
    private bool tileLoaded;
    public GUIStyle St;
    
    public enum TileTipe {Wall,Ground,Water,Deco,Other}

    public Color color1;
    public Color color2;
    public Color color3;
    public static void OpenWindow()
    {
        TileEditorWindow instance = (TileEditorWindow)GetWindow(typeof(TileEditorWindow));
        instance.St = new GUIStyle();
        instance.Show();
        instance.minSize = new Vector2(300, 300);
    }
    private void OnGUI()
    {
        if(GUILayout.Button("New Custom Tile"))
        {

        }
        currentTile = (CustomTile)EditorGUILayout.ObjectField("Customize Tile", currentTile, typeof(CustomTile), true);
        if (currentTile)
        {
            
            ShowTileOptions();
        }
    }
    void ShowTileOptions()
    {

        EditorGUILayout.LabelField("Mesh Size: " + currentTile.meshSize);
       if( GUILayout.Button("Re-Size tile to 1/1"))
        {
            currentTile.ReSize();
        }
       
        Repaint();
    }
}
