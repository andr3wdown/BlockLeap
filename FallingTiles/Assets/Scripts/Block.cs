using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isStopped;
    public float speed;
    public GameObject particle;
    float c = 1f;
	void FixedUpdate ()
    {
        if (!isStopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.up, speed * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            c -= Time.deltaTime;
            if (c <= 0)
            {
                Destroy(particle);
            }
        }
	}

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.GetComponent<Block>() != null)
        {
            if (col.transform.GetComponent<Block>().isStopped)
            {
                isStopped = true;
            }
        }

        if(col.transform.GetComponent<PlayerController>() != null)
        {
            col.transform.parent = this.transform;
        }
      
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.transform.GetComponent<PlayerController>() != null)
        {
            col.transform.parent = null;
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
