using System;
using TurnBasedGame;
using TurnBasedGame.Type;

public static class StaticGlobalEvent
{
    public static Action<GameState> OnGameStateChanged;
    public static Action<GameData> OnGameData;
    public static Action<Character,Character> OnCharacterBattle;
    public static Action<CharacterData> CharacterData;
    public static Action<bool> OnSwitchTurn;
    public static Action<float,bool> OnCharacterDamaged;


    #region UI Event
    public static Action<bool> OnAttackButtonClick;
    public static Action<bool> OnDefenseButtonClick;
    public static Action<bool> OnBuffButtonClick;
    public static Action<bool> OnDeBuffButtonClick;
    #endregion
}
