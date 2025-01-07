using System;
using System.Collections;
using Cinemachine;
using TurnBasedGame.Type;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurnBasedGame
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        [SerializeField] private GameData gameData;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CinemachineVirtualCamera battlaCamera;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            StaticGlobalEvent.OnAttackButtonClick += AttackButtonClickHandler;
            StaticGlobalEvent.OnDefenseButtonClick += DefenseButtonClickHandler;
        }

        public IEnumerator PrepareBattle(Character attacker, Character defender, bool playerInitiated)
        {
            FreezeExploration(attacker, defender);

            yield return PlayTransitionEffects();

            SetupBattle(attacker, defender, playerInitiated);

            TransitionToBattle();
        }

        private void FreezeExploration(Character attacker, Character defender)
        {
            Debug.Log("Exploration frozen!");
            if (attacker is null && defender is null) return;
            gameManager.CharacterBattleHandler(attacker, defender);

        }

        private IEnumerator PlayTransitionEffects()
        {
            Debug.Log("Playing transition effects...");
            yield return new WaitForSeconds(1f);
        }

        private void TransitionToBattle()
        {
            Debug.Log("Transitioning to battle...");
            StartBattle();
        }

        public void SetupBattle(Character attacker, Character defender, bool playerInitiated)
        {
            Debug.Log($"Setting up battle: {attacker.characterType} vs {defender.characterType}");
            gameData.GameState = GameState.Battle;
            gameData.PlayerInitiated = playerInitiated;
            StaticGlobalEvent.OnGameData?.Invoke(gameData);
        }

        public void StartBattle()
        {
            // Set Camera Priority
            battlaCamera.Priority = 11;
        }


        private void DefenseButtonClickHandler(bool obj)
        {
            Debug.Log($"Player Defense GameManager");

        }

        private void AttackButtonClickHandler(bool obj)
        {
            Debug.Log($"Player Attack GameManager");
        }
    }

}
