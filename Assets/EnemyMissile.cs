using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float bulletSpeed;
    public float stopFollowingDist;

    bool follow = true;

    GameObject player;

    public float lifetime;
    public GameObject explosion;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > stopFollowingDist && follow == true)
        {
            transform.LookAt(player.transform.position);
        }
        else
        {
            StartCoroutine(FollowDelay());
        }

        //gameObject.GetComponent<Rigidbody>().AddForce((transform.forward * bulletSpeed) * Time.deltaTime);
        transform.position += (transform.forward * bulletSpeed) * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerWeapon")
        {
            Destruct();
        }
    }


    IEnumerator FollowDelay()
    {
        follow = false;
        yield return new WaitForSeconds(2f);
        follow = true;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(lifetime);
        Destruct();
    }

    void Destruct()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
