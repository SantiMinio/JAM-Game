using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool Grabing { get; private set; }

    private void Start()
    {
        GameManager.Instance.AddCollectable();
    }

    public bool CollectItem(Transform parentToAttach)
    {
        if (Grabing) return false;

        Grabing = true;

        transform.position = parentToAttach.transform.position;
        transform.SetParent(parentToAttach);

        return true;
    }

    public void DropItem(Vector3 posToDrop)
    {
        Grabing = false;
        transform.position = posToDrop;
        transform.SetParent(null);
    }
}
