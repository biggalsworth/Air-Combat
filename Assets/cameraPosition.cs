using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPosition : MonoBehaviour
{

    public GameObject CameraPos;
    public GameObject PlayerObj;


    public Transform followTarget;
    public float adjustSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position = CameraPos.transform.position;

        transform.transform.LookAt(PlayerObj.transform.position);*/


        float vertical = Mathf.Lerp(transform.position.y, followTarget.position.y, adjustSpeed * Time.deltaTime);

        transform.position = new Vector3(followTarget.position.x, vertical, followTarget.position.z);

    }
}
