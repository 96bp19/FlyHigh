using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    public float characterRotateSpeed;
    InputData inputs;

    //public float moveForce =2;

    public float moveSpeed=2;

    bool controllingPlayer = false;

    public float maxRotateAngle = 30;
    public float controlledRotationSpeed = 2f;

    public Transform playermesh;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInputs();
        RotateAccordingToVeolocity();


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void RotateAccordingToVeolocity()
    {
        Quaternion lookRotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.up);
        Quaternion playerRot = transform.rotation;

        playerRot = Quaternion.Slerp(playerRot, lookRotation, characterRotateSpeed * Time.deltaTime);
        transform.rotation = playerRot;


        controllingPlayer = inputs.Horizontal_raw!= 0;
        if (controllingPlayer)
        {
            Debug.Log("rotating player according to inputs");
           
            playermesh.transform.Rotate(0, controlledRotationSpeed * inputs.Horizontal_raw* Time.deltaTime, 0, Space.Self);
            Vector3 clampedAngle = new Vector3(
                playermesh.transform.localEulerAngles.z,
                MyMath.ClampAngle(playermesh.localEulerAngles.y, -15, 15),
                playermesh.transform.localEulerAngles.z
                );

            playermesh.localEulerAngles = clampedAngle;
                
                


        }
        else
        {
            Vector3 newRotaation = MyMath.AngleLerp(playermesh.transform.localEulerAngles, Vector3.zero, characterRotateSpeed * Time.deltaTime);
            playermesh.localEulerAngles = newRotaation;
        }
    }

    private void GetInputs()
    {
        InputHandler.GetInput(out inputs);

    }

    public void MovePlayer()
    {
        //  Vector3 applideForce = transform.right * moveForce * inputs.HorizontalInp* Time.fixedDeltaTime;
        // rb.AddForce(applideForce, ForceMode.VelocityChange);
        //  Debug.Log("Player moved");

      
       
        Vector3 newMovePos = Vector3.right * moveSpeed*inputs.HorizontalInp * Time.deltaTime;
        Debug.DrawRay(transform.position, transform.right * 10* inputs.HorizontalInp, Color.blue);
        transform.Translate(newMovePos);
       
       

   
       

          
        
    }
}
