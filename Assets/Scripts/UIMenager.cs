using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenager : MonoBehaviour {
    public static UIMenager Instance;
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;
    private void Awake() {
        Instance = this;
    }
    public void QuitGame() {
        Application.Quit();
    }
    public void ResumeGame() {
        GameManager.Instance.UpdateGameState(GameState.PlayerSelecting);
        _menuUI.SetActive(false);
        _hud.SetActive(true);
        Time.timeScale = 1;
    }
    public void PauseGame() {
        _menuUI.SetActive(true);
        _hud.SetActive(false);
        Time.timeScale = 0;
    }
    public void ShowVictoryScreen() {
        _victoryScreen.SetActive(true);
    }
    public void showDefeatScreen() {
        _defeatScreen.SetActive(true);
    }
    public void RestartLevel() {
        SceneController.Instance.RestartCurrentScene();
    }
}
