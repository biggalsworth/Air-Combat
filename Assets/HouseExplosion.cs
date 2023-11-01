using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseExplosion : MonoBehaviour
{
    public GameObject[] HouseParts;

    bool explode;

    float stopPhysicsDelay = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode(Vector3 ExplosionPoint, float force)
    {
        //Debug.Log("Explode");
        foreach (GameObject part in HouseParts)
        {
            part.GetComponent<Rigidbody>().isKinematic = false;

            part.GetComponent<Rigidbody>().AddExplosionForce(force, ExplosionPoint, 10f);
        }
        StartCoroutine(StopPhysics());
    }



    public IEnumerator StopPhysics()
    {
        yield return new WaitForSeconds(stopPhysicsDelay);
        explode = false;
    }
}
