using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovesCounter : MonoBehaviour {
    public static MovesCounter Instance { get; private set; }
    private int _moves;
    private void Awake() => Instance = this;

    public int Moves {
        get => _moves;
        set {
            if (_moves == value) return;
            _moves = value;

            scoreText.SetText($"{_moves}");
        }
    }
    [SerializeField] private TextMeshProUGUI scoreText;
}
