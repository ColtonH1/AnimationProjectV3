using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FindGem : MonoBehaviour
{
    public float lookRadius = 10f;

    public GameObject[] targetGameObject; //will be used to find GameObjects with a specified tag
    public List<GameObject> targets = new List<GameObject>(); //will be used find, and then delete once found, the game objects from what Sparx is looking for
    public bool isNear = false;
    public float distance;
    public int nearGemNum;
    // Start is called before the first frame update
    void Start()
    {
        targetGameObject = GameObject.FindGameObjectsWithTag("Gem"); //find all gems labled with "Gem"
        //add them to an array
        for(int i = 0; i < targetGameObject.Length; i++)
        {
            targets.Add(targetGameObject[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            distance = Vector3.Distance(targets[i].transform.position, transform.position); //distance from Sparx to closest gem

            //if gem is within range, set isNear to true, set which gem is near, remove the gem the array
            if (distance <= lookRadius)
            {
                isNear = true;
                nearGemNum = i;
                targets.Remove(targets[i]);
            }
            else
            {
                isNear = false;
            }
        }
        if(targets.Count == 0)
        {
            isNear = false;
        }

    }

    public bool nearGem()
    {
        return isNear;
    }

    //draw the range for unity editor 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
