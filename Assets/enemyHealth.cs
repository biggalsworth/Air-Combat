using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float health;
    public GameObject explosion;
    //public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        //health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
