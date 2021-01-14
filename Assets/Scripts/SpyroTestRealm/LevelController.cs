using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] Text currentGemCountText; //text field to display gem count

    int currentGemNum = 0;


    // Update is called once per frame
    void Update()
    {
        currentGemNum = Gem.GetGems(); //Gem.cs holds the information of gem count
        IncreaseGemCount(); //increase gem count
    }

    public void IncreaseGemCount()
    {
        currentGemCountText.text = "" + currentGemNum.ToString(); //change text field to increased gem count
    }
}
