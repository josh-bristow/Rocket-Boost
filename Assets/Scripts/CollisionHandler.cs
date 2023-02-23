using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelReloadDelay = 2f;
    [SerializeField] float newLevelDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip victorySound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly": 
            Debug.Log("Why are you still on the start pad");
            break;
            case "Finish":
            FinishLevelSequence();
            break;
            default:
            StartCrashSequence();
            break;
        }
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    private void NextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        } 
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", levelReloadDelay);
    }

    private void FinishLevelSequence()
    {
        isTransitioning = true;
        victoryParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victorySound);
        Invoke("NextLevel", newLevelDelay);
    }


    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;

        }
    }
}
