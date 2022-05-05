
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip WinSound;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    AudioSource audioSource;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    void DebugKeys(){
        if(Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();

        else if(Input.GetKeyDown(KeyCode.C))
            collisionDisabled = !collisionDisabled;
    }
    void OnCollisionEnter(Collision other)
    {
        if(collisionDisabled)
            return;
        if (isTransitioning == false)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                case "Fuel":
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
        else
            return;

    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(DeathSound);
        deathParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(WinSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
