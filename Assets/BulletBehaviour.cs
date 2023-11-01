using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float bulletSpeed;

    public float Damage;

    //PlayerMovment movementTracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce((transform.forward * bulletSpeed) * Time.deltaTime);
        transform.position += (transform.forward * bulletSpeed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            other.GetComponent<enemyHealth>().health -= Damage;
            Destroy(gameObject);
        }
    }
}
