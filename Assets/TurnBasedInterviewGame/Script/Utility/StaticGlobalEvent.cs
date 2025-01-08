using System;
using TurnBasedGame;
using TurnBasedGame.Type;

/// <summary>
/// I made this to be able act as Third Party anything.
/// Think of it as Radio. 
/// </summary>
public static class StaticGlobalEvent
{
    public static Action<GameState> OnGameStateChanged;
    public static Action<GameData> OnGameData;
    public static Action<Character,Character> OnCharacterBattle;
    public static Action<CharacterData> CharacterData;
    public static Action<bool> OnSwitchTurn;
    public static Action<float,bool> OnCharacterDamaged;
    public static Action<bool> OnCanEnemyAttack;


    #region UI Event
    public static Action<bool> OnAttackButtonClick;
    public static Action<bool> OnDefenseButtonClick;
    public static Action<bool> OnBuffButtonClick;
    public static Action<bool> OnDeBuffButtonClick;
    #endregion
}
