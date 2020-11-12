using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer = 1 << 9;
    GenericAnt currentAnt;

    private void Start()
    {
        GameManager.Instance.StopMove += CheckIfAnt;
    }

    void CheckIfAnt()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.up, out hit, GameManager.Instance.gridSpacing, playerLayer))
        {
            if (!currentAnt)
            {
                currentAnt = hit.transform.GetComponent<GenericAnt>();
                InFloorFeedback();
            }
        }
        else
        {
            if (currentAnt)
                BreakFloor();
        }
    }

    void BreakFloor()
    {
        GameManager.Instance.StopMove -= CheckIfAnt;
        gameObject.SetActive(false);
    }
    
    void InFloorFeedback()
    {

    }
}
