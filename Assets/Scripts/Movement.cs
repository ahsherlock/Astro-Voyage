using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody playerRocket;
    AudioSource rocketThrust;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        playerRocket = GetComponent<Rigidbody>();
        rocketThrust = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();


    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            StartThrust();

        }
        else
        {
            StopThrust();
        }

    }

    void StopThrust()
    {
        rocketThrust.Stop();
        mainThrusterParticles.Stop();
    }

    void StartThrust()
    {
        playerRocket.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!rocketThrust.isPlaying)
        {
            rocketThrust.PlayOneShot(mainEngine);
        }
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopLeftAndRightRotation();
        }
    }

    private void StopLeftAndRightRotation()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        playerRocket.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        playerRocket.freezeRotation = false;
    }
}
