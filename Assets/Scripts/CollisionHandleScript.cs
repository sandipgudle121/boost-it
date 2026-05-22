using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandleScript : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2.0f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool isControllable = true;
    bool isCollidable = true;

    AudioSource audioSource;
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
            LoadNextLevel();
        else if(Keyboard.current.cKey.wasPressedThisFrame)
            isCollidable = !isCollidable;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!isControllable || !isCollidable)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX, 0.5f);
        successParticles.Play();
        GetComponent<MovementScript>().enabled = false;
        Invoke("LoadNextLevel", 1.0f);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX, 0.5f);
        crashParticles.Play();
        GetComponent<MovementScript>().enabled = false;
        Invoke("ReloadLevel", sceneLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
