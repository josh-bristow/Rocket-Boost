using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelReloadDelay = 2f;
    [SerializeField] float newLevelDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip victorySound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) 
    {
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
        GetComponent<RocketMovement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", levelReloadDelay);
    }

    private void FinishLevelSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victorySound);
        Invoke("NextLevel", newLevelDelay);
    }
}
