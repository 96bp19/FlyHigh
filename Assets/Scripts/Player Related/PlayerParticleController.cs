using UnityEngine;


public class PlayerParticleController : MonoBehaviour
{

    public GameObject fallingParticleGameobject;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnPlayerLaunched()
    {
        fallingParticleGameobject.SetActive(false);
        InvokeRepeating("EnableFallingParticle", 0.2f,0.2f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        CancelInvoke("EnableFallingParticle");
        fallingParticleGameobject.SetActive(false);
    }

    void EnableFallingParticle()
    {
        
        if (rb.velocity.y <1f)
        {
            CancelInvoke("EnableFallingParticle");
            fallingParticleGameobject.SetActive(true);

        }
    }

}
