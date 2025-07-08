using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Props : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<IPlayer>() != null)
        {
            rb.isKinematic = false;

            rb.AddForce(Random.Range(20f, 100f), Random.Range(20f, 40f), Random.Range(20f, 100f));

            StopCoroutine(ResetRB());
            StartCoroutine(ResetRB());
        }
    }

    private IEnumerator ResetRB()
    {
        yield return new WaitForSeconds(5f);
        rb.isKinematic = true;
    }
}
