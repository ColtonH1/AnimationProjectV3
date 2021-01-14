using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparxAnimatePickUpGem : MonoBehaviour
{
    //animation
    Animator animator;
    public bool startAnim = false;

    //audio
    public AudioSource audioSource;

    //get animation and sound components
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    //check if startAnim has been set to true, if so, start the animation
    //once startAnim goes back to false, stop the animation
    void Update()
    {
        if (startAnim)
        {
            animator.SetBool("NearGem", true);
        }
        else
        {
            animator.SetBool("NearGem", false);
        }
    }

    //reference called by another script to start animation
    public void SetStartAnim(bool start)
    {
        startAnim = start;
        if(start)
        {
            audioSource.Play(); //if animation plays, play audio
        }
    }
}
