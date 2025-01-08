using System;
using TurnBasedGame.Type;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurnBasedGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameData gameData;

        private GameState gameState;
        private Character attacker;
        private Character defender;
        private bool HasSetupBattle = false;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            gameState = GameState.Exploring;

        }

        private void Update()
        {
            if (gameData.GameState == GameState.Battle)
            {
                if (HasSetupBattle) return;
                gameState = GameState.Battle;
                SetupBattle();
            }
        }

        private void SetupBattle()
        {
            attacker.SetGameState(gameState);
            defender.SetGameState(gameState);
            StaticGlobalEvent.OnGameStateChanged?.Invoke(GameState.Battle);
            HasSetupBattle = true;
        }

        public void CharacterBattleHandler(Character attacker, Character defender)
        {
            this.attacker = attacker;
            this.defender = defender;
        }
        public void UpdateGameStateForAllCharacters()
        {
            Instantiate(gameData.Attacker);
            Instantiate(gameData.Defender);
        }
    }
}
