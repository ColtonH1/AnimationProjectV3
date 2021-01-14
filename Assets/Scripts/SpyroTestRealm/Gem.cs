using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject trailParticle, touchParticles; //particle systems on gem
    bool hasTriggered; //keep track if all events have been triggered for the gem //firstTime;
    public Transform playerT; //Spyro
    Vector3 firstPos; //original position
    float timer; //keep track of time to detroy gameobject after giving everything else time to play
    public static int gemCount = 0; //keep track of gem count
    //public bool start = false; 
    public SparxAnimatePickUpGem sparxAnim; //gameObject reference to SparxAnimatePickUpGem script
    private ThirdPersonMovement spyrosScript; //gameObject reference to ThirdPersonMovement script
    public GameObject spyro; //Spyro's gameObject which holds the ThirdPersonMovement script that is needed

    //audio
    public AudioSource audioSource; //the necessary audio source
    public AudioClip pickUpGem; //the clip to play

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); //to play the sound
        spyrosScript = spyro.GetComponent<ThirdPersonMovement>(); //to trigger the floating text
        touchParticles.SetActive(false);
        trailParticle.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Spyro or Sparx could trigger this
        if(((other.CompareTag("Player") || other.CompareTag("Sparx"))) && !hasTriggered)
        {
            sparxAnim.SetStartAnim(true); //Have Sparx begin to pick up the gem
            Invoke("playAudio", 0.2f); //play them gem audio shortly after being picked up
            spyrosScript.ShowFloatingText(); //trigger the floating text
            gemCount++; //increase gem count
            firstPos = transform.position; //original position

            trailParticle.SetActive(true); //trigger particles when flying to Spyro
            touchParticles.SetActive(false); //turn off particles for idle position
            hasTriggered = true; //all events for this gem has triggered
        }
    }


    void playAudio()
    {
        audioSource.PlayOneShot(pickUpGem); //invoked method to play audio
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Sparx"))
        {
            hasTriggered = false; //reset process
            Destroy(gameObject); //destory gameObject
            trailParticle.SetActive(false); //turn off particles
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasTriggered)
        {
            if (timer > 1)
            {
                sparxAnim.SetStartAnim(false); //turn off animation after enough time for it to have been played
                gameObject.SetActive(false); //turn off gameObject
            }

            timer += Time.deltaTime; //keep track of time
            //variables for the sprialling to Spyro
            var mid = (playerT.position + firstPos) / 2;
            mid = new Vector3(mid.x, mid.y + 1.5f, mid.z);

            var upper = firstPos + new Vector3(0, 2, 0);

            transform.position = CalculateCubicBazierPosition(firstPos, upper, mid, playerT.position, timer); //postion is the flying to Spyro
        }
    }

    //calculate the way it sprials to Spyro
    Vector3 CalculateCubicBazierPosition(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (((1 - t) * (1 - t) * (1 - t)) * p0 + 3 * ((1 - t) * (1 - t)) * t * p1 + 3 * (1 - t) * t * t * p2 + t * t * t * p3);
    }

    //gem count
    public static int GetGems()
    {
        return gemCount;
    }

}
