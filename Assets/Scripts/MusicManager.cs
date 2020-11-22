using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] AudioSource music = null;

    [SerializeField] List<int> levelToChange = new List<int>();
    [SerializeField] List<AudioClip> musics = new List<AudioClip>();

    void Awake()
    {
        MusicManager[] array = FindObjectsOfType<MusicManager>().Where(x => x!=this).ToArray();

        if (array == null || array.Length<1)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            music.Play();
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

    public void EnterToLevel(int level)
    {
        AudioClip current = null;

        for (int i = 0; i < levelToChange.Count; i++)
        {
            if (levelToChange[i] <= level)
                current = musics[i];
        }

        if (music.clip == current) music.UnPause();
        else
        {
            music.Stop();
            music.clip = current;
            music.Play();
        }
    }
  
}
