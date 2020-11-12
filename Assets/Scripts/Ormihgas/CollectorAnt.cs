using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorAnt : GenericAnt
{
    [SerializeField] Transform collectableParent = null;
    Collectable currentCollectable = null;
    [SerializeField] AudioClip grabSound = null;

    protected override void Start()
    {
        base.Start();
        animEvent.Add_Callback("GrabEvent", CollectAnim);
    }

    protected override void InFinalPos()
    {
        base.InFinalPos();

        TryCollect();
    }

    protected override void TurnOff()
    {
        if (currentCollectable) GameManager.Instance.DropItem();
    }

    protected override void Move(Vector3 movement)
    {
        base.Move(movement);

        TryCollect();
    }

    void TryCollect()
    {
        if (currentCollectable) return;

        RaycastHit hit;

        if(Physics.Raycast(transform.position + Vector3.up * 0.2f, transform.forward, out hit, GameManager.Instance.gridSpacing, obsLayers))
        {
            if (hit.transform.GetComponent<Collectable>()) Collect(hit.transform.GetComponent<Collectable>());
        }
    }

    void Collect(Collectable _item)
    {
        if (_item.Grabing) return;
        currentCollectable = _item;
        anim.SetTrigger("Grab");
        PlaySound(grabSound);
        isAction = true;
    }

    public void CollectAnim()
    {
        currentCollectable.CollectItem(collectableParent);
        isAction = false;
    }

    protected override void DeadAbstract()
    {
        currentCollectable?.DropItem(transform.position);
    }

    protected override void OnPause()
    {
    }

    protected override void OnResume()
    {
    }
}
