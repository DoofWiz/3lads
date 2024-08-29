using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDevTVRocketMove : MonoBehaviour
{
    [SerializeField] float rotatePower = 0f;
    [SerializeField] float thrustPower = 0f;
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        UseThrust();
        UseTurn();
    }


    void UseThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    void UseTurn()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotatePower);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotatePower);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
