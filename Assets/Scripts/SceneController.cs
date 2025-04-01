using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Loads a scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Loads a scene by index
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Loads a scene asynchronously (for smoother transitions)
    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    // Reloads the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quits the game (works in standalone builds)
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}