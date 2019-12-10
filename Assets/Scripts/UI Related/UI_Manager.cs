using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TMP_Text landingText;

    private static UI_Manager _Instance;
    public static UI_Manager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<UI_Manager>();

            }
            return _Instance;
        }
    }


    public void UpdateLandingText(float distance)
    {
        landingText.gameObject.SetActive(true);
        if (distance <=4)
        {
            landingText.text = "Perfect";
        }
        else if (distance <=5)
        {
            landingText.text = "Great";
        }
        else if (distance <=7)
        {
            landingText.text = "Nice";
        }
        else
        {
            landingText.text = "Not bad";
        }
    }
}
