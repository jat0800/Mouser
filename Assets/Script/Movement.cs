using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainthrust = 100f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem jetParticle;
    [SerializeField] ParticleSystem rightSideParticle;
    [SerializeField] ParticleSystem leftSideParticle;

    Rigidbody myRb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       myRb = GetComponent<Rigidbody>(); 
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotations();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }
    void ProcessRotations()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }
    void StartThrust () 
    {
        myRb.AddRelativeForce(0,1,0 * mainthrust * Time.deltaTime);
            if (!jetParticle.isPlaying)
            {
            jetParticle.Play();
            }
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
    }
    void StopThrust ()
    {
        audioSource.Stop();
        jetParticle.Stop();
    }
    void RotateLeft () 
    {
        ApplyRotation(rotateSpeed);
            if (!rightSideParticle.isPlaying)
            { 
            rightSideParticle.Play();
            }
    }
    void RotateRight () 
    {
        ApplyRotation(-rotateSpeed);
            if (!leftSideParticle.isPlaying)
            { 
            leftSideParticle.Play();
            }
    }
    void StopRotate ()
    {
        leftSideParticle.Stop();
        rightSideParticle.Stop();
    }
    void ApplyRotation(float rotateSpeed) 
    {
        myRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);    
        myRb.freezeRotation = false;
    }

}
