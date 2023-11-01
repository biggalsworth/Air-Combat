using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInteraction : MonoBehaviour
{

    public int radius;
    public int dist;
    public float force;

    public GameObject Explosion;

    bool explode = false;

    public float damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (explode)
        {
            RaycastHit hit;

            Vector3 startPos = transform.position;
            float distanceToObstacle = 0;

            // Cast a sphere wrapping character controller 10 meters forward
            // to see if it is about to hit anything.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                GameObject hitObj = hitCollider.gameObject;
                if (hitObj.tag == "House")
                {
                    //Debug.Log("HIT");
                    hitObj.GetComponent<HouseExplosion>().Explode(transform.position, force);
                }
                if(hitObj.tag == "Enemy")
                {
                    hitObj.GetComponent<enemyHealth>().health -= damage;
                }
            }

            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<enemyHealth>().health -= damage;
            }
            gameObject.GetComponent<MeshCollider>().enabled = false;
            explode = true;
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
