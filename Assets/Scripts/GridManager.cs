using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public static GridManager Instance;

    [SerializeField] private int _wight;
    [SerializeField] private int _height;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private List<Unit> _piecePrefab;

    [SerializeField] private Transform _cam;
    private Dictionary<Vector2, Tile> _tiles;

    private void Awake() {
        Instance = this;
    }
    public void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        GameObject tileHolder = new GameObject("TileHolder");
        for (int x = 0; x < _wight; x++) {
            for (int y = 0; y < _height; y++) {
                var RandomPiece = _piecePrefab[Random.Range(0, _piecePrefab.Count)];
                var GeneratedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                GeneratedTile.name = $"Tile_{x}_{y}";
                GeneratedTile.gameObject.transform.SetParent(tileHolder.gameObject.transform);

                var GeneretedPiece = Instantiate(RandomPiece, new Vector3(x, y), Quaternion.identity);
                GeneretedPiece.Move(new Vector2(x, y));
                GeneretedPiece.gameObject.transform.SetParent(GeneratedTile.gameObject.transform);

                GeneratedTile.Init(new Vector2(x, y), GeneretedPiece);
                _tiles[new Vector2(x, y)] = GeneratedTile;

            }
        }
        _cam.transform.position = new Vector3((float)_wight / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        GameManager.Instance.UpdateGameState(GameState.PlayerSelecting);
    }
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) {
            return tile;
        }
        return null;
    }
    public void OnTileSelected(Tile tile) {
        List<Tile> tilesToDestroy = tile.GetConnectedTiles();
        if (tilesToDestroy.Count > 1) {
            for (int i = 0; i < tilesToDestroy.Count; i++) {
                GameManager.Instance.SetScore(tilesToDestroy[i].currentUnit._data.Points * tilesToDestroy.Count);
                Destroy(tilesToDestroy[i].currentUnit.gameObject);
                tilesToDestroy[i].Empty();

            }
            GameManager.Instance.UpdateGameState(GameState.TilePop);
        }
        else {
            tile.currentUnit.Shake();
            GameManager.Instance.UpdateGameState(GameState.TileInteraction);
        }
    }
    public void ColapseTiles() {
        var colapseTiles = new List<Tile>();
        for (int y = 0; y < _height; y++) {
            for (int x = 0; x < _wight; x++) {
                if (_tiles[new Vector2(x, y)].isEmpty) {
                    var tile = _tiles[new Vector2(x, y)].GetNextPopulatedTileInColumn();
                    if (tile == null) {
                        continue;
                    }
                    colapseTiles.Add(tile);
                    tile.currentUnit.Move(new Vector2(x, y));
                    tile.currentUnit.gameObject.transform.SetParent(_tiles[new Vector2(x, y)].gameObject.transform);
                    _tiles[new Vector2(x, y)].Populate(tile.currentUnit);
                    tile.Empty();

                }
            }
        }
        if (colapseTiles.Count == 0) {
            GameManager.Instance.UpdateGameState(GameState.ColapseAnimation);
        }
    }
    public void RepopulateBoard() {
        for (int y = 0; y < _height; y++) {
            for (int x = 0; x < _wight; x++) {
                if (_tiles[new Vector2(x, y)].isEmpty) {
                    var RandomPiece = _piecePrefab[Random.Range(0, _piecePrefab.Count)];
                    var GeneretedPiece = Instantiate(RandomPiece, new Vector3(x, 11), Quaternion.identity); // Chenge the height to be adjustable;
                    GeneretedPiece.Move(new Vector2(x, y));
                    GeneretedPiece.gameObject.transform.SetParent(_tiles[new Vector2(x, y)].gameObject.transform);
                    _tiles[new Vector2(x, y)].Populate(GeneretedPiece);
                }
            }
        }
    }
    public int GetWight() {
        return _wight;
    }
    public int GetHeight() {
        return _height;
    }
}
