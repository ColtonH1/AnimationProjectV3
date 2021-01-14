using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimationController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //get component
    }

    // Update is called once per frame
    void Update()
    {
        //check if Spyro's script is walking or not every frame
        if(ThirdPersonMovement.isWalking)
        {
            animator.SetBool("StartWalking", true);
        }
        else
        {
            animator.SetBool("StartWalking", false);
        }
    }
}
