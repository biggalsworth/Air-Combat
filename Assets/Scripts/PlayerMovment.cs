using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody body;

    public Vector3 Velocity;


    Vector3 m;

    InputMaster controls;

    Vector2 move;

    float xAxis;

    public float activeForwardSpeed;
    
    public float baseSpeed;
    public float speed = 1;
    public float speedMult = 30;
    public float speedLimit = 20;

    public float xRot;
    public float yRot;
    
    void Awake()
    {
        controls = new InputMaster();

        body = GetComponent<Rigidbody>();

        if (transform.rotation.z > -45 && transform.rotation.z < 45)
        {
            controls.Player.Move.performed += ctx => xAxis = ctx.ReadValue<float>() * 5;
        }
        else
        {
            xAxis = 0;
        }
        controls.Player.Move.canceled += ctx => xAxis = 0;

        controls.Player.Accelerate.performed += ctx => speed = ctx.ReadValue<float>();
        controls.Player.Accelerate.canceled += ctx => speed = 0.1f;


        controls.Player.Decelerate.performed += ctx => speed = ctx.ReadValue<float>() * -1;
        controls.Player.Decelerate.canceled += ctx => speed = 0.1f;

        controls.Player.LookLeftRight.performed += ctx => xRot = ctx.ReadValue<float>() * Time.deltaTime;
        controls.Player.LookLeftRight.canceled += ctx => xRot = 0;

        controls.Player.Lookupdown.performed += ctx => yRot = ctx.ReadValue<float>() * Time.deltaTime;
        controls.Player.Lookupdown.canceled += ctx => yRot = 0;



    }

    // Update is called once per frame
    void Update()
    {
        /*
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        */
        if (baseSpeed + speed * speedMult < speedLimit && baseSpeed + speed * speedMult > 5)
        {
            m = new Vector3(0, 0, baseSpeed + speed * speedMult);
        }

        transform.Rotate(0, xRot * 50, 0, Space.World);
        transform.Rotate(yRot * 50, 0, 0, Space.Self);

        //spinning

        //find the current z rotation
        float zSpin;
        zSpin = gameObject.transform.localEulerAngles.z; //localEulerAngles will get the z angle based on a global 360 rotation. transform.rotation.z returns decimals for some reason so dont use that
        string dir = "";

        if(zSpin < 358 && zSpin > 181)
        {
            //Debug.Log("right");
            dir = "right";
        }
        if(zSpin < 180.9f && zSpin > 2 )    //spinning right
        {
            //Debug.Log("Left");
            dir = "left";
        }

        if (xAxis == 0)
        {
            if (dir == "left")
            {
                transform.Rotate(0, 0, 0 - transform.eulerAngles.z * Time.deltaTime);
            }
            if (dir == "right")
            {
                transform.Rotate(0, 0, 0 + (transform.eulerAngles.z * Time.deltaTime)/10); //divide by ten to make the turn slower
            }
        }
        else
        {
            //spins the plane
            transform.Rotate(new Vector3(0, 0, -xAxis * 20) * Time.deltaTime);
        }


        //move foward

        //transform.Translate(m * Time.deltaTime);
        if (speed > 0)
        {
            //Debug.Log("Speed:" + speed);
            activeForwardSpeed = baseSpeed + speed * speedMult;
            transform.position += (transform.forward * activeForwardSpeed) * Time.deltaTime;
        }
        else if (speed < 0)
        {
            //Debug.Log("Slowing");
            //Debug.Log("Speed:" + speed);
            activeForwardSpeed = baseSpeed + speed * speedMult;

            //Debug.Log(activeForwardSpeed);
            if(activeForwardSpeed > 0)
            {
                transform.position -= (transform.forward * activeForwardSpeed) * Time.deltaTime;
            }
            else
            {
                transform.position += (transform.forward * baseSpeed) * Time.deltaTime;
            }
        }

        //activeForwardSpeed = baseSpeed + speed * speedMult;
       

        Velocity = body.velocity;
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
