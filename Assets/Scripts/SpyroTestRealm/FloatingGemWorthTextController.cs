using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FloatingGemWorthTextController : MonoBehaviour
{
    private static FloatingGemWorthText popupText;
    private static GameObject canvas;

    public void OnEnable()
    {
        popupText = GetComponent<FloatingGemWorthText>();
    }
    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if(!popupText)
            popupText = Resources.Load<FloatingGemWorthText>("Prefab/GemWorthText_Parent");
        Debug.Log("We loaded: " + popupText);
    }

    public static void CreateWorthText(string text, Transform location)
    {
        Debug.Log("text is called");
        FloatingGemWorthText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
