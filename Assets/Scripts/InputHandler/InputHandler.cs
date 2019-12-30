using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static string HorizontalInput = "Horizontal";
    private static  string VerticalInput = "Vertical";
    private static bool InputEnabled = true;

    private static InputData inputData;
    static InputData input = new InputData(0); 

    public static void EnableInput( bool value)
    {
        inputData = input;
        InputEnabled = value;
    }

    private void Update()
    {
        if (!InputEnabled)
        {
           
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

    public InputData( float resetval=0)
    {
        HorizontalInp = 0;
        VerticalInp = 0;
        Horizontal_raw = 0;
        Vertical_raw = 0;
    }
        
}
