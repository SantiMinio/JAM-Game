using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomTile : MonoBehaviour
{
    
    public Color c1;
    public Color c2;
    public Shader Shader;
    public Vector3 meshSize;
    public void ReSize()
    {        
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        meshSize=mesh.bounds.size;
        if (meshSize.x!=1)
        {
            transform.localScale = Vector3.one*(1/meshSize.x);
        }
        Debug.Log(meshSize);
        mesh.RecalculateBounds();
    }

    public void SetPivotParent(Vector3 pivotPosition)
    {
        if (transform.parent == null)
        {
            transform.parent = new GameObject(name + "parent").transform; 
        }
        transform.parent.position = transform.position+pivotPosition;
    }
}
