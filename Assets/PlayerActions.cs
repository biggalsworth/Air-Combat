using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    InputMaster controls;

    GameObject spawn;

    [Header("Bullet")]

    public GameObject shootPoint;
    public GameObject BulletPrefab;
    PlayerMovment movementScript;

    public GameObject flareObj;


    float shoot = 0;
    float flare = 0;

    bool shootDelay = false;
    bool flareDelay = false;
    [SerializeField]
    bool flared = false;
    public float bulletCooldown;
    public float flareCooldown;


    [Header("Bomb")]

    public GameObject bombPrefab;
    public GameObject bombDropPoint;

    float dropBomb;
    public float bombCooldown;

    bool bombDelay;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new InputMaster();;

        movementScript = GetComponent<PlayerMovment>();

        spawn = GameObject.Find("SpawnPoint");

        flareObj = GameObject.Find("Flares");

    }

    // Update is called once per frame
    void Update()
    {

        #region shooting 

        shoot = controls.Player.Shoot.ReadValue<float>();

        flare = controls.Player.Flares.ReadValue<float>();

        //Debug.Log(shoot);

        if (!shootDelay)
        {
            if (shoot == 1)
            {
                GameObject bullet = Instantiate(BulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
                //bullet.GetComponent<BulletBehaviour>().bulletSpeed += movementScript.activeForwardSpeed;
                Destroy(bullet, 5f);
                StartCoroutine(Delay("bullet", bulletCooldown));
            }
        }

        #endregion

        #region Bomb Attack

        dropBomb = controls.Player.Bomb.ReadValue<float>();

        if (!bombDelay)
        {
            if (dropBomb == 1)
            {
                GameObject droppedBomb = Instantiate(bombPrefab, bombDropPoint.transform.position, Quaternion.Euler(180, 0, 0));
                //bullet.GetComponent<BulletBehaviour>().bulletSpeed += movementScript.activeForwardSpeed;
                Destroy(droppedBomb, 20f);
                StartCoroutine(Delay("bomb", bombCooldown));
            }
        }


        #endregion

        #region flares

        if (!flareDelay)
        {
            if (flare == 1)
            {
                flareObj.GetComponent<ParticleSystem>().Play();

                StartCoroutine(Flare());

            }
        }

        #endregion

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "PlayerWeapon" && other.gameObject.tag != "Player")
        {
            //Debug.Log(other.gameObject.tag);
            if(other.gameObject.tag == "EnemyWeapon")
            {
                if (flared == false)
                {
                    Time.timeScale = 0.1f;
                    StartCoroutine(Death());
                }
            }
            else
            {
                Time.timeScale = 0.1f;
                StartCoroutine(Death());
            }
        }
    }


    IEnumerator Delay(string delayType, float delayTime)
    {
        if (delayType == "bullet")
        {
            shootDelay = true;
            yield return new WaitForSeconds(delayTime);
            shootDelay = false;
        }

        if (delayType == "bomb")
        {
            bombDelay = true;
            yield return new WaitForSeconds(delayTime);
            bombDelay = false;
        }

        if (delayType == "flare")
        {
            flareDelay = true;
            yield return new WaitForSeconds(delayTime);
            flareDelay = false;
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.1f);

        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;

        Time.timeScale = 1f;
    }

    IEnumerator Flare()
    {
        flareDelay = true;

        flared = true;
        yield return new WaitForSeconds(2);
        flared = false;

        yield return new WaitForSeconds(flareCooldown);

        flareDelay = false;
    }



    public void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
