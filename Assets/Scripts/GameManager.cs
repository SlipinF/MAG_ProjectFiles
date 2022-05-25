using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] private int _score;
    [SerializeField] private int _requiredScore;
    [SerializeField] private int _movesRemaining;
    public GameState State;
    private void Awake() {
        Instance = this;
    }  
    private void Start() {
        UpdateGameState(GameState.GenerateGrid);
    }
    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.GenerateGrid:
                PopulateUiElements();
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.TileInteraction:
                break;
            case GameState.PlayerSelecting:
                break;
            case GameState.TilePop:
                _movesRemaining--;
                PopulateUiElements();
                GridManager.Instance.ColapseTiles();
                CheckForEndOfGame();
                break;
            case GameState.ColapseAnimation:
                GridManager.Instance.RepopulateBoard();
                break;
            case GameState.Pause:
                UIMenager.Instance.PauseGame();
                break;
            case GameState.GameOver:
                DisplayGameOverScreen();
                break;
            case GameState.Victory:
                DisplayVictoryScreen();
                break;
            default:
                break;
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (State == GameState.Pause) {
                UIMenager.Instance.ResumeGame();
            }
            else {
                UpdateGameState(GameState.Pause);
            }
        }
    }
    public void DisplayVictoryScreen() {
        UIMenager.Instance.ShowVictoryScreen();
    }
    public void DisplayGameOverScreen() {
        UIMenager.Instance.showDefeatScreen();
    }
    public void SetScore(int Value) {
        _score += Value;
    }
    public void CheckForEndOfGame() {
        if (_movesRemaining <= 0) {
            if (_score >= _requiredScore) {
                UpdateGameState(GameState.Victory);
            }
            else {
                UpdateGameState(GameState.GameOver);
            }
        }
    }

    public void PopulateUiElements() {
        ScoreCounter.Instance.RequiredScore = _requiredScore;
        ScoreCounter.Instance.Score = _score;
        MovesCounter.Instance.Moves = _movesRemaining;
    }
}
