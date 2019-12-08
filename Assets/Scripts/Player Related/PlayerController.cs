using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    public float characterRotateSpeed;
    InputData inputs;

    public float moveForce =2;


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
    }

    private void GetInputs()
    {
        InputHandler.GetInput(out inputs);

    }

    public void MovePlayer()
    {
        Vector3 applideForce = transform.right * moveForce * inputs.HorizontalInp* Time.fixedDeltaTime;
        rb.AddForce(applideForce, ForceMode.VelocityChange);
        Debug.Log("Player moved");
    }
}
