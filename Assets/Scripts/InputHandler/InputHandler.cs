using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static string HorizontalInput = "Horizontal";
    private static  string VerticalInput = "Vertical";
    private static bool InputEnabled = true;

    private static InputData inputData;

    public static void EnableInput( bool value)
    {
        InputEnabled = value;
    }

    private void Update()
    {
        if (!InputEnabled)
        {
            Debug.Log("Input disabled");
            return;
        }

        SetInputValues();

    }

    void SetInputValues()
    {
        inputData.HorizontalInp = Input.GetAxis(HorizontalInput);
        inputData.VerticalInp = Input.GetAxis(VerticalInput);
        inputData.Horizontal_raw = Input.GetAxisRaw(HorizontalInput);
        inputData.Vertical_raw = Input.GetAxisRaw(VerticalInput);
    }

    public static void  GetInput(out InputData inputs)
    {
       inputs = inputData;
    }
    

    
}

public struct InputData
{
    public float HorizontalInp;
    public float VerticalInp;
    public float Horizontal_raw;
    public float Vertical_raw;
}
