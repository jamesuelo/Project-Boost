using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool isAlive;
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem engine;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;

    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProccessThrust();
        ProcessRotation();
    }

    void ProccessThrust(){
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        engine.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);

        if (!engine.isPlaying)
            engine.Play();
    }

    void ProcessRotation(){

        if (Input.GetKey(KeyCode.A))
        {
            Rotateleft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            rightThrust.Stop();
            leftThrust.Stop();
        }
    }

    private void RotateRight()
    {
        Rotation(-rotationThrust);
        if (!leftThrust.isPlaying)
            leftThrust.Play();
    }

    private void Rotateleft()
    {
        Rotation(rotationThrust);
        if (!rightThrust.isPlaying)
            rightThrust.Play();
    }

    void Rotation(float rotationFrame){
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime* rotationFrame);
        rb.freezeRotation = false;
    }
}
