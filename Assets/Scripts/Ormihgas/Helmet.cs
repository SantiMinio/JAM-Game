using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : MonoBehaviour, IPauseable
{
    bool paused;
    Rigidbody rb;

    public void HelmetOff(Vector3 dir)
    {
        transform.SetParent(null);
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = false;
            rb.AddForce(dir, ForceMode.Impulse);
        }
        GameManager.Instance.pauseManager.AddToPause(this);

        StartCoroutine(HelmetDissappear());
    }

    Vector3 velocity;

    public void Pause()
    {
        velocity = rb.velocity;
        rb.isKinematic = true;
    }

    public void Resume()
    {
        rb.isKinematic = false;
        rb.velocity = velocity;
    }

    IEnumerator HelmetDissappear()
    {

        float timer = 0;
        while (timer<3) 
        {
            if(paused) yield return new WaitForSeconds(0.1f);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }

        GameManager.Instance.pauseManager.RemoveToPause(this);
        Destroy(this.gameObject);
    }
}
