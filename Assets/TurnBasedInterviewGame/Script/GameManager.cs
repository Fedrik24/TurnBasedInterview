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
            StaticGlobalEvent.OnAttackButtonClick += AttackButtonClickHandler;
            StaticGlobalEvent.OnDefenseButtonClick += DefenseButtonClickHandler;
        }

        private void DefenseButtonClickHandler(bool obj)
        {
            Debug.Log($"Player Defense GameManager");
            // Handler how player defnse, and got attacked by enemy
            // meaning this would skip turn and player hit/take damage to defense
            // idk but i think it would be great if this on Player Component rather than GameManager
            // we'll see. 
        }

        private void AttackButtonClickHandler(bool obj)
        {
            Debug.Log($"Player Attack GameManager");
            //attack
        }

        private void Update()
        {
            if (gameData.GameState == GameState.Battle)
            {
                if (HasSetupBattle) return;
                gameState = GameState.Battle;
                StaticGlobalEvent.OnCharacterBattle += CharacterBattleHandler;
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

        private void CharacterBattleHandler(Character attacker, Character defender)
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
