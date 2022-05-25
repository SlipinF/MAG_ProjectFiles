using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums { }

public enum TileColor {
    Red,
    Blue,
    Green,
    Yellow,
    Orange,
    Purple,
    Pink,
    Black,
    White
}

public enum GameState {    
    GenerateGrid,
    PlayerSelecting,
    TileInteraction,       
    TilePop,
    ColapseAnimation,
    CalculatePoints,
    Pause,
    GameOver,
    Victory    
}
