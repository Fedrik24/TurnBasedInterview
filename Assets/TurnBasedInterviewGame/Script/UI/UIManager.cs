using System;
using TurnBasedGame.Type;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject EnemyInfo;
        [SerializeField] private GameObject TurnInfo;
        [SerializeField] private GameObject AttackPanel;
        [SerializeField] private Image playerTurnImage;
        [SerializeField] private Image enemyTurnImage;
        private void Awake()
        {
            StaticGlobalEvent.OnGameStateChanged += GameStateHandler;
            StaticGlobalEvent.OnGameData += GameDataHandler;
        }

        private void GameDataHandler(GameData data)
        {
            // Initalize who attack first.
            if (data.PlayerInitiated)
            {
                playerTurnImage.color = new Color(255, 255, 255, 255);
            }
            else
            {
                enemyTurnImage.color = new Color(255, 255, 255, 255);
            }
        }

        private void GameStateHandler(GameState state)
        {
            if (state == GameState.Battle)
            {
                EnemyInfo.SetActive(true);
                TurnInfo.SetActive(true);
                AttackPanel.SetActive(true);
            }
        }

        public void OnAttackClick()
        {
            Debug.Log($"Player Attack !");
            // send event to gamemanager. let the game manager handled.
            StaticGlobalEvent.OnAttackButtonClick?.Invoke(true);
        }

        public void OnDefenseClick()
        {
            Debug.Log($"Player Defense !");
            StaticGlobalEvent.OnDefenseButtonClick?.Invoke(true);
        }
    }
}

