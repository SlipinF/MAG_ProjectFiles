using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public Unit currentUnit;
    private Vector2 _cordinates;
    public bool isEmpty { get; private set; }

    [SerializeField] private Color _colorOdd;
    [SerializeField] private Color _colorEven;

    private void OnMouseDown() {
        if (GameManager.Instance.State != GameState.PlayerSelecting) return;
        OnTileClickedHandle();
    }
    private void OnTileClickedHandle() {
        if (!isEmpty) {
            GridManager.Instance.OnTileSelected(this);
        }
    }
    public void Init(Vector2 coridnates, Unit newUnit) {
        _cordinates = coridnates;
        currentUnit = newUnit;
    }
    public void Populate(Unit newUnit) {
        isEmpty = false;
        currentUnit = newUnit;
    }
    public void Empty() {
        isEmpty = true;
        currentUnit = null;
    }
    private void Start() {
        GetComponent<SpriteRenderer>().color = GridColor;
    }
    public Color GridColor => (_cordinates.x % 2 == 0 && _cordinates.y % 2 == 0) || (_cordinates.x % 2 != 0 && _cordinates.y % 2 != 0) ? _colorOdd : _colorEven;

    public Tile Left => _cordinates.x > 0 ? GridManager.Instance.GetTileAtPosition(new Vector2(_cordinates.x - 1, _cordinates.y)) : null;

    public Tile Top => _cordinates.y >= 0 ? GridManager.Instance.GetTileAtPosition(new Vector2(_cordinates.x, _cordinates.y + 1)) : null;

    public Tile Right => _cordinates.x < GridManager.Instance.GetWight() - 1 ? GridManager.Instance.GetTileAtPosition(new Vector2(_cordinates.x + 1, _cordinates.y)) : null;

    public Tile Bottom => _cordinates.y <= GridManager.Instance.GetHeight() - 1 ? GridManager.Instance.GetTileAtPosition(new Vector2(_cordinates.x, _cordinates.y - 1)) : null;

    public Tile[] Neighbours => new Tile[] { Left, Top, Right, Bottom };

    public List<Tile> GetConnectedTiles(List<Tile> exclude = null) {
        var result = new List<Tile> { this, };

        if (exclude == null) {
            exclude = new List<Tile> { this, };
        }
        else {
            exclude.Add(this);
        }
        foreach (var neighbour in Neighbours) {
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.isEmpty || neighbour.currentUnit._data.ColorID != currentUnit._data.ColorID) continue;
            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }
        return result;
    }
    public Tile GetNextPopulatedTileInColumn() {
        var result = this;
        var current = this;
        for (int i = (int)_cordinates.y; i < GridManager.Instance.GetHeight() - 1; i++) {
            if (!current.Top.isEmpty) {
                result = current.Top;
                return result;
            }
            current = current.Top;
        }
        return null;
    }
}
