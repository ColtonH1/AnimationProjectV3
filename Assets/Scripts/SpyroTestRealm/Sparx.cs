using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparx : MonoBehaviour
{
    public GameObject thePlayer; //Spyro
    public float targetDistance;
    public float allowedDistance = 5;
    public GameObject theNPC; //Sparx
    public float followSpeed;
    public RaycastHit shot; //what the raycast shoots at

    public static int gemCount = 0;

    public FindGem findGem;
    public bool foundGem;
    GameObject thisGem;
    bool atGem = false;



    private void Awake()
    {
        findGem = gameObject.GetComponent<FindGem>();
    }

    // Update is called once per frame
    void Update()
    {
        atGem = findGem.isNear;
        thisGem = findGem.targetGameObject[findGem.nearGemNum];
        //test running
        allowedDistance = 5;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunWithSpyro();
        }
        else
        {
            FollowSpyro();
        }

    }

    private void MoveToGem()
    {
        Vector3 defaultPosition = transform.position;

        transform.position = Vector3.MoveTowards(transform.position, thisGem.transform.position, followSpeed * Time.deltaTime); // move to the gem
        transform.position = Vector3.MoveTowards(transform.position, defaultPosition, followSpeed * Time.deltaTime); // move back to the player
        atGem = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gem")
        {
            gemCount++;
        }
    }

    //If Spyro is running, run faster to keep up
    private void RunWithSpyro()
    {
        transform.LookAt(thePlayer.transform); //know Spyro's position and face it

        //if there is a raycast from Sparx, in the forward direction (which is constantly looking at Spyro) that hits
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot)) 
        {
            targetDistance = shot.distance; //targetDistance is how far away Spyro is

            //will not allow Sparx to get too close to Spyro
            if (targetDistance >= allowedDistance)
            {
                followSpeed = 0.3f; //set a faster follow speed to keep up
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, followSpeed); //move to Spyro with given speed
            }
            else
            {
                followSpeed = 0; //too close, stop for this update round
            }
        }
    }

    //If Spyro is walking, fly normal speed behind
    private void FollowSpyro()
    {
        transform.LookAt(thePlayer.transform); //know Spyro's position and face it

        //if there is a raycast from Sparx, in the forward direction (which is constantly looking at Spyro) that hits
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
        {
            targetDistance = shot.distance;//targetDistance is how far away Spyro is

            //will not allow Sparx to get too close to Spyro
            if (targetDistance >= allowedDistance && !atGem)
            {
                followSpeed = 0.1f; //the normal fly speed behind Spyro
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, followSpeed); //move to Spyro with given speed
            }
            else
            {
                followSpeed = 0; //too close, stop for this update round
            }
        }
    }
}
