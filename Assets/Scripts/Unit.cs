using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour {
    [SerializeField] public TileData _data;
    [SerializeField] private Vector2 _position;
    public void Move(Vector2 newPosition) {
        _position = newPosition;
        transform.DOMoveY(newPosition.y, 0.5f).OnComplete(() => {
            if (GameManager.Instance.State == GameState.ColapseAnimation) {
                GameManager.Instance.UpdateGameState(GameState.PlayerSelecting);
            }
            else if (GameManager.Instance.State == GameState.TilePop) {
                GameManager.Instance.UpdateGameState(GameState.ColapseAnimation);
            }
        });
    }
    public void Shake() {
        transform.DOShakeRotation(0.5f, 4f, 10, 90, false).OnComplete(() => { GameManager.Instance.UpdateGameState(GameState.PlayerSelecting);});
    }
}
