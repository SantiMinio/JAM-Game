using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnt : GenericAnt
{
    [SerializeField] AudioClip[] attackSound = null;
    ITakeDamage currentTarget;
    public ParticleSystem hitPs;

    [SerializeField] Helmet myHelmet = null;
    [SerializeField] float forceToHelmet = 5;

    bool withHelmet = true;
    protected override void Start()
    {
        base.Start();
        animEvent.Add_Callback("GrabEvent", AttackAnim);
    }

    protected override void InFinalPos()
    {
        base.InFinalPos();

        TryAttack();
    }

    protected override void Move(Vector3 movement)
    {
        base.Move(movement);

        TryAttack();
    }

    void TryAttack()
    {
        if (!withHelmet) return;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, transform.forward, out hit, GameManager.Instance.gridSpacing, obsLayers))
        {
            if (hit.transform.GetComponent<ITakeDamage>() != null) Attack(hit.transform.GetComponent<ITakeDamage>());
        }
    }

    void Attack(ITakeDamage _item)
    {
        if (currentTarget != null) return;

        isAction = true;
        currentTarget = _item;
        anim.SetTrigger("Grab");
        PlaySound(attackSound[Random.Range(0,attackSound.Length)]);
        hitPs.Play();
    }

    public void AttackAnim()
    {
        currentTarget.TakeDamage();
        currentTarget = null;
        withHelmet = false;
        myHelmet.HelmetOff((-transform.forward + transform.up) * forceToHelmet);
        isAction = false;
    }

    public override void Dead(DamageType dmgType)
    {
        if (dmgType == DamageType.Normal && withHelmet) return;
        base.Dead(dmgType);
    }

    protected override void TurnOff()
    {
    }

    protected override void DeadAbstract()
    {
    }

    protected override void OnPause()
    {

    }

    protected override void OnResume()
    {
    }
}
