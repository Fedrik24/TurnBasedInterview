using System.Collections;
using System.Collections.Generic;
using TurnBasedGame.UI;
using UnityEngine;

namespace TurnBasedGame
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public IEnumerator PrepareBattle(Character attacker, Character defender, bool playerInitiated)
        {
            // Freeze the exploration state
            FreezeExploration();

            // Play transition animation or effects
            yield return PlayTransitionEffects();

            // Prepare battle data
            SetupBattle(attacker, defender, playerInitiated);

            // Transition to battle
            TransitionToBattle();
        }

        private void FreezeExploration()
        {
            // Disable player and enemy movement
            Debug.Log("Exploration frozen!");
        }

        private IEnumerator PlayTransitionEffects()
        {
            Debug.Log("Playing transition effects...");
            UIManager.Instance.FadeIn();
            yield return new WaitForSeconds(1f); // Simulate transition delay
            UIManager.Instance.FadeOut();
        }

        private void TransitionToBattle()
        {
            // Switch to battle scene or activate battle mode
            Debug.Log("Transitioning to battle...");
            StartBattle();
        }

        public void SetupBattle(Character attacker, Character defender, bool playerInitiated)
        {
            Debug.Log($"Setting up battle: {attacker.characterType} vs {defender.characterType}");
            // Prepare battle data here
        }

        public void StartBattle()
        {
            // Load battle scene or activate battle overlay
            Debug.Log("Battle started!");
        }
    }

}
