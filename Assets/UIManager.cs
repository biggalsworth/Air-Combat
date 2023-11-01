using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public Camera cam;
    public GameObject crosshair;
    public GameObject enemyIndicator;


    GameObject aimPoint;
    GameObject Enemy;
    ShootPointAimer aimScript;

    Transform gotransform;

    // Start is called before the first frame update
    void Start()
    {
        aimPoint = GameObject.Find("ShootPoint");
        aimScript = aimPoint.GetComponent<ShootPointAimer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //gotransform.position = aimScript.endOfAim;

        //crosshair.transform.position = cam.WorldToScreenPoint(gotransform.position);

        var screenPos = cam.WorldToScreenPoint(aimScript.endOfAim.position);
        crosshair.transform.position = screenPos;

        try
        {
            Enemy = GameObject.FindWithTag("EnemyWeapon");
            if(Vector3.Distance(GameObject.Find("Player").transform.position, Enemy.transform.position) < 50f)
            {
                enemyIndicator.SetActive(true);
                var enemyPos = cam.WorldToScreenPoint(Enemy.transform.position);
                enemyIndicator.transform.position = enemyPos;
            }
            else
            {
                enemyIndicator.SetActive(false);
            }

        }
        catch
        {
            enemyIndicator.SetActive(false);
        }
    }
}
