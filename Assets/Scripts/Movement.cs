using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip engineThrust;

    Rigidbody playerRocket;
    AudioSource rocketThrust;
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
            playerRocket.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!rocketThrust.isPlaying)
            {
                rocketThrust.PlayOneShot(engineThrust);
            }
        }
        else
        {
            rocketThrust.Stop();
        }

    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationThrust);
        }
    }
    void ApplyRotation(float rotationThisFrame)
    {
        playerRocket.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        playerRocket.freezeRotation = false;
    }
}
