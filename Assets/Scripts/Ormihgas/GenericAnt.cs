using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericAnt : MonoBehaviour, IPauseable
{
    [SerializeField] protected LayerMask obsLayers = 1 << 0;
    [SerializeField] float movementLerp = 3f;
    [SerializeField] protected Animator anim = null;
    [SerializeField] protected AnimEvent animEvent = null;
    [SerializeField] protected AudioClip splashSound = null;
    Rigidbody rb;
    bool lerp;
    float lerping;
    Vector3 initPos;
    Vector3 finalPos;

    public bool isAction;

    AudioSource audioSource;
    [SerializeField] AudioClip deadSound = null;

    protected virtual void Start()
    {
        GameManager.Instance.InitializeAnt(Move, this);
        animEvent.Add_Callback("DeadEvent", DeadEvent);
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.AddToQuestion(IsAction, false);
        GameManager.Instance.pauseManager.AddToPause(this);
    }

    protected bool IsAction() => isAction;

    public void Off(ControllerBase controller)
    {
        TurnOff();
        isAction = false;
        GameManager.Instance.AddToQuestion(IsAction, true);
        controller.MovementInput -= Move;
        GameManager.Instance.pauseManager.RemoveToPause(this);
        gameObject.SetActive(false);
    }

    protected abstract void TurnOff();

    protected virtual void Move(Vector3 movement)
    {
        transform.forward = movement.normalized;
        if (!ObstacleSensor())
        {
            if (lerp) return;
            initPos = transform.position;
            finalPos = transform.position + movement;
            lerp = true;
            anim.SetBool("Walk", true);
        }
        else
            GameManager.Instance.StopMovementAnt();
    }

    private void Update()
    {
        if (isPause) return;

        if (lerp)
        {
            lerping += Time.deltaTime * movementLerp;
            transform.position = Vector3.Lerp(initPos, finalPos, lerping);
            if (lerping >= 1)
                InFinalPos();
        }
    }

    protected virtual void InFinalPos()
    {
        transform.position = finalPos;
        lerping = 0;
        lerp = false;
        anim.SetBool("Walk", false);
        GameManager.Instance.StopMovementAnt();
        CheckFloor();
    }

    bool ObstacleSensor()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, transform.forward, GameManager.Instance.gridSpacing, obsLayers))
            return true;

        return false;
    }

    void CheckFloor()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, -transform.up, GameManager.Instance.gridSpacing, obsLayers)) return;

        Falling();
    }

    protected virtual void Falling()
    {
        Dead(DamageType.Heavy);
        rb.constraints = RigidbodyConstraints.None;
    }

    public virtual void Dead(DamageType dmgType)
    {
        RemoveFromCamera();
        anim.SetBool("Dead", true);
        GameManager.Instance.DeadAnt(Move, this);
        PlaySound(deadSound);
        isAction = true;
    }

    void DeadEvent()
    {
        isAction = false;
        GameManager.Instance.AddToQuestion(IsAction, true);
        DeadAbstract();
        GameManager.Instance.pauseManager.RemoveToPause(this);
        gameObject.SetActive(false);
    }

    void RemoveFromCamera()
    {
        var cam = Camera.main.GetComponent<CameraFollow>();
        cam.ants.Remove(this);
    }
    protected abstract void DeadAbstract();

    protected void PlaySound(AudioClip soundToPlay, bool looping = false)
    {
        audioSource.Stop();
        audioSource.loop = looping;
        audioSource.clip = soundToPlay;
        audioSource.Play();
    }

    protected bool isPause;
    float animVel;
    Vector3 velocity;

    public void Pause()
    {
        isPause = true;
        velocity = rb.velocity;
        rb.isKinematic = true;
        animVel = anim.speed;
        anim.speed = 0;
        if (audioSource.isPlaying) audioSource.Pause();
        OnPause();
    }

    public void Resume()
    {
        isPause = false;
        rb.isKinematic = false;
        rb.velocity = velocity;
        anim.speed = animVel;
        audioSource.UnPause();
        OnResume();
    }

    protected abstract void OnPause();

    protected abstract void OnResume();
}
