using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour {
    public static ScoreCounter Instance { get; private set; }
    private int _score;
    private int _requiredScore;
    private void Awake() => Instance = this;

    public int Score {
        get => _score;
        set {
            if (_score == value) return;
            _score = value;

            scoreText.SetText($"{_score} / {_requiredScore}");
        }
    }
    public int RequiredScore {
        get => _requiredScore;
        set {
            if (_requiredScore == value) return;
            _requiredScore = value;
            scoreText.SetText($"{_score} / {_requiredScore}");
        }
    }
    [SerializeField] private TextMeshProUGUI scoreText;
}
