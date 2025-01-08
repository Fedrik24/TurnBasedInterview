using System;
using System.Collections;
using Cinemachine;
using TurnBasedGame.Type;
using UnityEngine;

namespace TurnBasedGame
{
    /// <summary>
    /// This is core on how battle script.
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        [SerializeField] private GameData gameData;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CinemachineVirtualCamera battleCamera;

        private Character player;
        private Character enemy;
        private bool isPlayerTurn;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            StaticGlobalEvent.OnAttackButtonClick += AttackButtonClickHandler;
            StaticGlobalEvent.OnDefenseButtonClick += DefenseButtonClickHandler;
            StaticGlobalEvent.OnBuffButtonClick += BuffButtonClickHandler;
            StaticGlobalEvent.OnDeBuffButtonClick += DeBuffButtonClickHandler;

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
            if (attacker == null || defender == null) return;
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
            player = playerInitiated ? attacker : defender;
            enemy = playerInitiated ? defender : attacker;
            isPlayerTurn = playerInitiated;
            Debug.Log($"isplayerTurn : {isPlayerTurn}");

            gameData.GameState = GameState.Battle;
            StaticGlobalEvent.OnGameData?.Invoke(gameData);
        }

        public void StartBattle()
        {
            
            if(battleCamera is null){
                battleCamera =FindAnyObjectByType<CinemachineVirtualCamera>();
            }
            battleCamera.Priority = 11;
            Debug.Log("Battle started!");
            if (!isPlayerTurn)
            {
                Debug.Log($"Should not showing");
                PerformAttack(enemy, player);
            }
        }

        private void AttackButtonClickHandler(bool isPlayerAction)
        {
            if (isPlayerTurn)
            {
                Debug.Log("Player attacks!");
                PerformAttack(player, enemy);
            }
        }

        private void DefenseButtonClickHandler(bool isPlayerAction)
        {
            if (isPlayerTurn)
            {
                Debug.Log("Player defends!");
                PerformDefense(player);
            }
        }

        private void BuffButtonClickHandler(bool obj)
        {
            if (isPlayerTurn)
            {
                PerformBuffDefense(player);
            }
        }

        private void DeBuffButtonClickHandler(bool obj)
        {
            if (isPlayerTurn)
            {
                PerformDebuff(enemy);
            }
        }


        private void PerformAttack(Character attacker, Character defender)
        {
            float damage = attacker.characterData.Attack - defender.characterData.DefensePoint;
            damage = Mathf.Max(damage, 5);
            defender.characterData.HealthPoint -= damage;

            Debug.Log($"{attacker.characterType} attacks {defender.characterType} for {damage} damage. Remaining HP: {defender.characterData.HealthPoint}");
            StaticGlobalEvent.OnCharacterDamaged?.Invoke(damage, isPlayerTurn);
            if (defender.characterData.HealthPoint <= 0)
            {
                EndBattle(attacker);
            }
            else
            {
                StartCoroutine(SwitchTurn());
            }
        }

        private void PerformDebuff(Character enemy)
        {
            float debuffDefense = enemy.characterData.DefensePoint / 2;
            debuffDefense = Mathf.Max(debuffDefense, 0);
            enemy.characterData.DefensePoint = debuffDefense;
            StartCoroutine(SwitchTurn());
        }

        private void PerformDefense(Character defender)
        {
            defender.characterData.HealthPoint += defender.characterData.DefensePoint - 5;
            StartCoroutine(SwitchTurn());
        }

        private void PerformBuffDefense(Character defender)
        {
            defender.characterData.DefensePoint *= 1.5f;
            Debug.Log($"{defender.characterType} defends, increasing defense to {defender.characterData.DefensePoint}");

            StartCoroutine(SwitchTurn());
        }

        private IEnumerator SwitchTurn()
        {
            yield return new WaitForSeconds(1f); // Pause for turn transition

            isPlayerTurn = !isPlayerTurn;
            StaticGlobalEvent.OnSwitchTurn?.Invoke(isPlayerTurn);
            Debug.Log(isPlayerTurn ? "Player's turn!" : "Enemy's turn!");

            if (!isPlayerTurn)
            {
                EnemyTurn();
            }
        }

        private void EnemyTurn()
        {
            // Simple AI: Randomly choose between attack or defense
            bool willAttack = UnityEngine.Random.value > 0.5f;
            if (willAttack)
            {
                PerformAttack(enemy, player);
            }
            else
            {
                PerformDefense(enemy);
            }
        }

        private void EndBattle(Character winner)
        {
            Debug.Log($"{winner.characterType} wins the battle!");
            gameData.GameState = GameState.Exploring;
            StaticGlobalEvent.OnGameStateChanged?.Invoke(GameState.Exploring);
            battleCamera.Priority = 1;
            StaticGlobalEvent.HasWonGame?.Invoke(true, winner.characterType);
        }
    }
}