using UnityEngine;

public class RocketMovement : MonoBehaviour
{        
    [SerializeField] float mainThrust = 1500f;
    [SerializeField] float rotationThrust = 300f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    bool gravityEnabled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.useGravity = false;
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
    }


    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }

        if (!gravityEnabled)
        {
            rb.useGravity = true;
            gravityEnabled = true;
        }
    }


    private void StopThrusting()
    {
        mainBoosterParticles.Stop();
        audioSource.Stop();
    }


    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }


    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }


    private void StopRotation()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
