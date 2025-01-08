using System;
using TurnBasedGame.Type;
using UnityEngine;


namespace TurnBasedGame.Utility
{
    /// <summary>
    /// Spawner when battle.
    /// </summary>
    public class SpawnCharacter : MonoBehaviour
    {
        [SerializeField] private GameObject playerSpawnPoint;
        [SerializeField] private GameObject enemySpawnPoint;
        [SerializeField] private GameObject playerCharacter;
        [SerializeField] private GameObject enemyCharacter;

        private void Awake()
        {
            StaticGlobalEvent.OnGameStateChanged += GameStateHandler;
        }

        private void GameStateHandler(GameState state)
        {
            if(state == GameState.Battle)
            {
                // Set GameState on Character
                playerCharacter.GetComponent<Character>().gameState = GameState.Battle;
                enemyCharacter.GetComponent<Character>().gameState = GameState.Battle;
                // Instatiate Player and Enem
                Instantiate(playerCharacter, playerSpawnPoint.transform.position, Quaternion.identity);
                Instantiate(enemyCharacter, enemySpawnPoint.transform.position, Quaternion.identity);
            }
        }

    }

}
