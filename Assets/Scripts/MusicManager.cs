using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource music;
    // Start is called before the first frame update
    void Awake()
    {
        MusicManager[] array = FindObjectsOfType<MusicManager>().Where(x => x!=this).ToArray();

        if (array == null || array.Length<1)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(gameObject);
    }

    public void PauseMusic()
    {
        if (music.isPlaying) music.Pause();
    }

    public void ResumeMusic()
    {
        music.UnPause();
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
  
}
