using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour, ITakeDamage,IPauseable
{
    [SerializeField] LayerMask playerMask = 1 << 9;
    [SerializeField] Animator anim = null;
    [SerializeField] AnimEvent animEvent = null;
    [SerializeField] AudioSource aS=null;
    [SerializeField] AudioClip auAttack = null;
    [SerializeField] AudioClip auDead = null;
    GenericAnt target;

    bool isAction;

    private void Start()
    {
        GameManager.Instance.StopMove += Attack;
        animEvent.Add_Callback("AttackEvent", AttackEvent);
        animEvent.Add_Callback("DeadEvent", DeadEvent);
        GameManager.Instance.AddToQuestion(IsAction, false);
        GameManager.Instance.pauseManager.AddToPause(this);
    }

    protected bool IsAction() => isAction;
    void Attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, transform.forward, out hit, GameManager.Instance.gridSpacing, playerMask))
            Attacking(hit.transform.GetComponent<GenericAnt>(), transform.forward);
        else if (Physics.Raycast(transform.position + Vector3.up * 0.2f, -transform.forward, out hit, GameManager.Instance.gridSpacing, playerMask))
            Attacking(hit.transform.GetComponent<GenericAnt>(), -transform.forward);
        else if (Physics.Raycast(transform.position + Vector3.up * 0.2f, transform.right, out hit, GameManager.Instance.gridSpacing, playerMask))
            Attacking(hit.transform.GetComponent<GenericAnt>(), transform.right);
        else if (Physics.Raycast(transform.position + Vector3.up * 0.2f, -transform.right, out hit, GameManager.Instance.gridSpacing, playerMask))
            Attacking(hit.transform.GetComponent<GenericAnt>(), -transform.right);

    }

    public void TakeDamage()
    {
        GameManager.Instance.StopMove -= Attack;
        anim.SetBool("Dead", true);
        isAction = true;
    }

    void Attacking(GenericAnt _ant, Vector3 dir)
    {
        transform.forward = dir;
        target = _ant;
        anim.SetTrigger("Attack");
        isAction = true;
    }

    void AttackEvent()
    {
        aS.clip = auAttack;
        aS.Play();
        target.Dead(DamageType.Normal);
        isAction = false;
    }

    void DeadEvent()
    {
        aS.clip = auDead;
        aS.Play();
        isAction = false;
        GameManager.Instance.AddToQuestion(IsAction, true);
        GameManager.Instance.pauseManager.RemoveToPause(this);
        gameObject.SetActive(false);
    }
    float animVel;

    public void Pause()
    {
        animVel = anim.speed;
        anim.speed = 0;
        if (aS.isPlaying) aS.Pause();
    }

    public void Resume()
    {
        anim.speed = animVel;
        aS.UnPause();
    }
}
