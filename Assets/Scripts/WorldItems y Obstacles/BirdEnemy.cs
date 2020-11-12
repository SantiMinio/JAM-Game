using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    [SerializeField] float movementsToAppear = 3;
    [SerializeField] float movementsToDissapppear = 2;
    [SerializeField] float flySpeed = 3;
    [SerializeField] LayerMask playerLayer = 1 << 9;

    [SerializeField] Transform skyPos = null;
    [SerializeField] Transform terrainPos = null;

    bool inTerrain;
    bool isAction;
    int movementCount;

    GenericAnt target;

    protected virtual void Start()
    {
        GameManager.Instance.StopMove += CountMovement;
        GameManager.Instance.AddToQuestion(IsAction, false);
        transform.position = skyPos.position;
    }

    bool IsAction() => isAction;

    void CountMovement()
    {
        movementCount += 1;

        if (inTerrain) TryToCapture();

        if (!target)
        {
            if (inTerrain && movementCount >= movementsToDissapppear) ToSky();
            else if (!inTerrain && movementCount >= movementsToAppear) ToTerrain();
        }
    }

    void TryToCapture()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 5, playerLayer))
            target = hit.transform.GetComponent<GenericAnt>();
        else
            return;

        target.Dead(DamageType.Heavy);
        target.transform.SetParent(transform);
        target = null;
        ToSky();
    }

    void ToTerrain()
    {
        inTerrain = true;
        movementCount = 0;
        StartCoroutine(Travel(terrainPos.position, true));
    }

    void ToSky()
    {
        inTerrain = false;
        movementCount = 0;
        StartCoroutine(Travel(skyPos.position));
    }

    IEnumerator Travel(Vector3 finalPos, bool tryCapture = false)
    {
        Vector3 initPos = transform.position;
        isAction = true;
        float lerp = 0;

        while(lerp < 1)
        {
            lerp += Time.deltaTime * flySpeed;

            transform.position = Vector3.Lerp(initPos, finalPos, lerp);

            yield return new WaitForSeconds(0.01f);
        }

        isAction = false;
        if (tryCapture) TryToCapture();
    }
}
