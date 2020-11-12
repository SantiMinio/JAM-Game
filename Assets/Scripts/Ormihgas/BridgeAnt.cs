using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAnt : GenericAnt
{
    Vector3 objetivePos;
    Vector3 initialPos;
    [SerializeField] float fallSpeed = 1.2f;
    [SerializeField] float fallAmmount = -1.09f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Falling()
    {

        if (!Physics.Raycast(transform.position + transform.forward * GameManager.Instance.gridSpacing + Vector3.up * 0.2f, -transform.up, GameManager.Instance.gridSpacing, obsLayers))
        {
            base.Falling();
            return;
        }

        var cam = Camera.main.GetComponent<CameraFollow>();
        cam.ants.Remove(this);
        GameManager.Instance.DeadAnt(Move, this);
        StartCoroutine(FallCoroutine());
    }

    IEnumerator FallCoroutine()
    {
        isAction = true;
        initialPos = transform.position;
        objetivePos = new Vector3(transform.position.x, transform.position.y + fallAmmount, transform.position.z);
        float lerpTimer = 0;

        while(lerpTimer < 1)
        {
            if(isPause) yield return new WaitForSeconds(0.01f);
            lerpTimer += Time.deltaTime * fallSpeed;
            transform.position = Vector3.Lerp(initialPos, objetivePos, lerpTimer);

            yield return new WaitForSeconds(0.01f);
        }

        isAction = false;
        GameManager.Instance.AddToQuestion(IsAction, true);
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
