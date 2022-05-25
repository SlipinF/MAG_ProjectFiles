using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController Instance;
    private void Awake() {
        Instance = this;
    }
    public void SetNewScene(int sceneID) {
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single);
        UIMenager.Instance.ResumeGame();
    }
    public void RestartCurrentScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
