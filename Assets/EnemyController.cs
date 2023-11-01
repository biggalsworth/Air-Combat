using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;

    Transform target;

    public GameObject PivotPoint;
    public GameObject barrel;
    public GameObject shootPoint;

    public GameObject enemyBullet;

    bool shoot;
    float shootDelay = 10f;

    public float Range;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        shoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < Range)
        {
            target = player.transform;

            Vector3 targetPostition = new Vector3(target.position.x, this.transform.position.y, target.position.z);

            PivotPoint.transform.LookAt(targetPostition);

            Vector3 barrelPos = new Vector3(target.position.x, target.transform.position.y + 5, target.position.z);

            //barrel.transform.LookAt(barrelPos);

            Vector3 PlayPosPred = target.position + (transform.forward * 5);
            barrel.transform.LookAt(barrelPos);

            if (shoot)
            {
                GameObject ActiveBullet = Instantiate(enemyBullet, shootPoint.transform.position, shootPoint.transform.rotation);
                ActiveBullet.transform.LookAt(PlayPosPred);

                StartCoroutine(delayShot());
            }
        }
    }


    IEnumerator delayShot()
    {
        shoot = false;
        yield return new WaitForSeconds(shootDelay);
        shoot = true;
    }
}
