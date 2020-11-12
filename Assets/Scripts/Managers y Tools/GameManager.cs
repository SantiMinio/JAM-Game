using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] int minimunCollect = 1;
    [SerializeField] int maximumAnt = 2;
    [SerializeField] ControllerBase controller = null;
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip winSound = null;
    [SerializeField] AudioClip loseSound = null;
    int totalCollectables;
    public float gridSpacing = 1;
    int antCount;
    int collectablesAmmount;
    int antLeave;

    bool inPause;
    [SerializeField] UI_Manager uiManager;
    [SerializeField] private float timeToRestart; //lo que tarda en reiniciar el nivel cuando perdes(para poder ver como mueren)
    public Action StopMove = delegate { };
    bool stopCD;
    float stopTimer;
    private CameraFollow cam;

    int antMovement = 0;

    public PauseManager pauseManager = new PauseManager();

    int collectorAnts;

    private void Awake() { Instance = this; pauseManager = new PauseManager(); }

    SaveData data = new SaveData();

    private void Start()
    {
        MusicManager.instance.ResumeMusic();

        cam = Camera.main.GetComponent<CameraFollow>();
        uiManager.RefreshCollectabeAmmount(collectablesAmmount, minimunCollect);

        if (BinarySerialization.IsFileExist(MainMenu.FileName)) data = BinarySerialization.Deserialize<SaveData>(MainMenu.FileName);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        Debug.Log("lo pasé?: " + data.levelsClear[sceneIndex].ToString());
        Debug.Log("Cuántas estrellas obtuve? " + data.starsPerLevel[sceneIndex]);
    }
    public void InitializeAnt(Action<Vector3> MoveInit, GenericAnt ant)
    {
        controller.MovementInput += MoveInit;
        antCount += 1;
        if (ant.GetComponent<CollectorAnt>()) collectorAnts += 1;
    }

    public void AddCollectable()
    {
        totalCollectables += 1;
    }

    public void ResumeGame()
    {
        inPause = false;
        uiManager.PauseScreen(false);
        pauseManager.Resume();
        MusicManager.instance.ResumeMusic();
    }

    private void Update()
    {
        if (inPause) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inPause = true;
            uiManager.PauseScreen(true);
            MusicManager.instance.PauseMusic();
            pauseManager.Pause();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GenericAnt>()) AntLeaving(other.GetComponent<GenericAnt>());
    }

    public void AntLeaving(GenericAnt _ant)
    {
        cam.ants.Remove(_ant);
        _ant.Off(controller);
        antCount -= 1;
        antLeave += 1;
        if (_ant.GetComponent<CollectorAnt>()) collectorAnts -= 1;
        CheckAntAmmount();

        antMovement += 1;

        if (antMovement >= antCount)
        {
            antMovement = 0;
            StopMove();
        }
    }

    public void DropItem()
    {
        collectablesAmmount += 1;
        uiManager.RefreshCollectabeAmmount(collectablesAmmount, minimunCollect);
    }

    void EndGame()
    {
        if (collectablesAmmount >= minimunCollect) Win();
        else Lose();
    }

    void Win()
    {
        controller.playing = false;
        MusicManager.instance.PauseMusic();
        audioSource.clip = winSound;
        audioSource.Play();
        Debug.Log("Ganaste, sos el mascapo");
        PointsRecount();
        StartCoroutine(FinishCoroutine(true));
    }

    void PointsRecount()
    {
        int stars = 1;

        if (collectablesAmmount >= totalCollectables) stars += 1;

        if (antLeave >= maximumAnt) stars += 1;

        uiManager.RefreshScore(stars);
        Debug.Log("ganaste " + stars + " estrellas.");

        int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        data.levelsClear[sceneIndex] = true;
        if(stars > data.starsPerLevel[sceneIndex]) data.starsPerLevel[sceneIndex] = stars;

        BinarySerialization.Serialize(MainMenu.FileName, data);
    }

    void Lose()
    {
        controller.playing = false;
        MusicManager.instance.music.Stop();
        audioSource.clip = loseSound;
        if(!audioSource.isPlaying) audioSource.Play();

        Debug.Log("Perdiste, sos un banana");

        StartCoroutine(FinishCoroutine(false));
    }

    void CheckAntAmmount()
    {
        if (antCount <= 0) EndGame();
        else if (minimunCollect - collectablesAmmount > collectorAnts) //yo agarre 1 y no llego al minimo,asi que si no tengo hormigas para llegar al minimo pierdo
            Lose();
    }

    public void DeadAnt(Action<Vector3> _Move, GenericAnt _ant)
    {
        if (_ant.GetComponent<CollectorAnt>()) collectorAnts -= 1;
        controller.MovementInput -= _Move;
        antCount -= 1;
        CheckAntAmmount();
    }

    public void StopMovementAnt()
    {
        //if (stopCD) return;

        antMovement += 1;

        if (antMovement >= antCount)
        {
            antMovement = 0;
            StopMove();
        }
    }

    IEnumerator FinishCoroutine(bool win)
    {
        yield return new WaitForSeconds(timeToRestart);
        if (win) uiManager.WinScreen();
        else uiManager.LoseScreen();
    }

    public void AddToQuestion(Func<bool> _IsStop, bool rest)
    {
        if (rest)
        {
            if(controller.questionList.Contains(_IsStop)) controller.questionList.Remove(_IsStop);
        }
        else
        {
            if (!controller.questionList.Contains(_IsStop)) controller.questionList.Add(_IsStop);
        }
    }
}

public enum DamageType { Normal, Heavy }
