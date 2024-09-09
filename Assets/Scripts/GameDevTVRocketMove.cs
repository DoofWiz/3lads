using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDevTVRocketMove : MonoBehaviour
{
    [SerializeField] float rotatePower = 0f;
    [SerializeField] float thrustPower = 0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip rightEngine;
    [SerializeField] AudioClip leftEngine;
    [SerializeField] ParticleSystem mainEngineVFX;
    [SerializeField] ParticleSystem leftThrusterVFX;
    [SerializeField] ParticleSystem rightThrusterVFX;
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
        void UseTurn()
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
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineVFX.isPlaying)
        {
            mainEngineVFX.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineVFX.Stop();
    }

    void RotateRight()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(leftEngine);
        }
        if (!leftThrusterVFX.isPlaying)
        {
            leftThrusterVFX.Play();
        }
        ApplyRotation(rotatePower);
    }

    void RotateLeft()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rightEngine);
        }
        if (!rightThrusterVFX.isPlaying)
        {
            rightThrusterVFX.Play();
        }
        ApplyRotation(-rotatePower);
    }

    void StopRotating()
    {
        audioSource.Stop();
        rightThrusterVFX.Stop();
        leftThrusterVFX.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
