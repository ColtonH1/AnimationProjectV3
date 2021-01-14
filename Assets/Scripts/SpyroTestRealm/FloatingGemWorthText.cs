using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingGemWorthText : MonoBehaviour
{
    public Animator animator;//control the animation
    public TextMesh worthText;//The text displaying the gem's worth

    //once enabled: 
    // - get the animation information
    // - Destroy once the animation is over
    // - get the text component
    private void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        worthText = GetComponent<TextMesh>();
    }

    //set the text once called
    public void SetText(string text)
    {
        worthText.text = text;
    }
}
