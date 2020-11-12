using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager
{
    List<IPauseable> pausingPlayObjects = new List<IPauseable>();

    public void AddToPause(IPauseable po) => pausingPlayObjects.Add(po);
    public void RemoveToPause(IPauseable po) => pausingPlayObjects.Remove(po);

    public void Pause()
    {
        for (int i = 0; i < pausingPlayObjects.Count; i++)
            pausingPlayObjects[i].Pause();
    }

    public void Resume()
    {
        for (int i = 0; i < pausingPlayObjects.Count; i++)
            pausingPlayObjects[i].Resume();
    }
}
