using TurnBasedGame;
using TurnBasedGame.Type;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public GameObject Attacker { get; set; }
    public GameObject Defender { get; set; }
    public GameState GameState { get; set; }
    public bool PlayerInitiated { get; set; }

    private void OnEnable()
    {
        Attacker = null;
        Defender = null;
        GameState = GameState.Exploring;
    }
}
