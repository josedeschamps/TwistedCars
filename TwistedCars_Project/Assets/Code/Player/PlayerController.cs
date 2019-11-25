using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //player stats
    public int m_PlayerNumber = 1;
    public float movementSpeed = 12f;
    public float shootRate = 5.0f;
    public float boostRate = 0.5f;
    public float speedBoostPower = 100;
    public float turnSpeed = 180f;
    //public float pitchRange = 0.2f;


    //movement
    private Rigidbody RB;
    private string movementAxisName;
    private string turnAxisName;

    //player inputs names
    private float movementInputValue;
    private float turnInputValue;
    private string boostInputName;
    private string shootInputName;
    //private float originalPitch;


    ////Charge Boost
    //public GameObject charging;
    //public GameObject boostUp;
    //private int chargeCounter = 0;
    //private float nextBoost = 0.0f;

    ////Shooting
    //private Transform spawnPoint;
    //public GameObject BulletPrefab;
    //private float nextFire = 0.0f;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        RB.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;

    }


    private void OnDisable()
    {
        RB.isKinematic = true;

    }


    void Start()
    {

        //spawnPoint = GetComponentInChildren<Transform>();

        shootInputName = "Shoot" + m_PlayerNumber;
        boostInputName = "Boost" + m_PlayerNumber;
        movementAxisName = "Vertical"; 
        turnAxisName = "Horizontal"; 


       

    }


    void Update()
    {



        //if (Input.GetButton(shootInputName) && Time.time > nextFire)
        //{

        //    nextFire = Time.time / shootRate;
        //    Shoot();

        //}






        //---Start here----///

        ////The R1 controller charge boost speed button
        //if (Input.GetButton(boostInputName))
        //{

        //    chargeCounter++;
        //    charging.SetActive(true);

        //    if (chargeCounter >= 100)
        //    {

        //        boostUp.SetActive(true);
        //        charging.SetActive(false);

        //    }

        //}

        ////The R1 controller buttonup do the boost
        //if (Input.GetButtonUp(boostInputName))
        //{

        //    if (chargeCounter >= 100)
        //    {

        //        boostUp.SetActive(false);
        //        SpeedBoost();


        //    }
        //    else if (Time.time > nextBoost)
        //    {

        //        nextBoost = Time.time + boostRate;
        //        charging.SetActive(false);
        //    }

        //    chargeCounter = 0;
        //}

        //--ends here---//



        //movement inputs
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
    }



    void FixedUpdate()
    {

        Move();
        Turn();
    }

    private void Move()
    {


        Vector3 movement = transform.forward * movementInputValue * movementSpeed * Time.deltaTime;
        RB.MovePosition(RB.position + movement);

    }


    private void Turn()
    {

        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        RB.MoveRotation(RB.rotation * turnRotation);
    }


    //private void SpeedBoost()
    //{

    //    Vector3 boost = transform.forward * speedBoostPower;
    //    RB.velocity = boost;
    //}


    //private void Shoot()
    //{

    //   // Instantiate(BulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

    //}
}
