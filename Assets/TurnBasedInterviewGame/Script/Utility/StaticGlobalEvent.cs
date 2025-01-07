using System;
using TurnBasedGame;
using TurnBasedGame.Type;

public static class StaticGlobalEvent
{
    public static Action<GameState> OnGameStateChanged;
    public static Action<GameData> OnGameData;
    public static Action<Character,Character> OnCharacterBattle;
    public static Action<CharacterData> CharacterData;

    #region UI Event
    public static Action<bool> OnAttackButtonClick;
    public static Action<bool> OnDefenseButtonClick;
    #endregion
}
