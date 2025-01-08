using System;
using TMPro;
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
        [SerializeField] private Sprite playerSpriteImage;
        [SerializeField] private Sprite enemySpriteImage;
        [SerializeField] private Slider enemyHealth;
        [SerializeField] private Slider playerHealth;
        [SerializeField] private TextMeshProUGUI damagedText;
        private void Awake()
        {
            StaticGlobalEvent.OnGameStateChanged += GameStateHandler;
            StaticGlobalEvent.OnGameData += GameDataHandler;
            StaticGlobalEvent.OnSwitchTurn += SwitchTurnHandler;
            StaticGlobalEvent.OnCharacterDamaged += CharacterDamagedHandler;

        }

        private void CharacterDamagedHandler(float value, bool isPlayerTurn)
        {
            damagedText.text = $"Damaged Dealt   {value}";
            if (isPlayerTurn)
            {
                enemyHealth.value -= value;
            }
            else
            {
                playerHealth.value -= value;
            }
        }

        private void SwitchTurnHandler(bool value)
        {
            if (value)
            {
                // Enemy Turn
                playerTurnImage.sprite = enemySpriteImage;
                enemyTurnImage.sprite = playerSpriteImage;
            }
            else
            {
                // Player Turn
                playerTurnImage.sprite = playerSpriteImage;
                enemyTurnImage.sprite = enemySpriteImage;

            }
        }

        private void GameDataHandler(GameData data)
        {
            // Initalize who attack first.
            Debug.Log($"is Player Init : {data.PlayerInitiated}");
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
            StaticGlobalEvent.OnAttackButtonClick?.Invoke(true);
        }

        public void OnDefenseClick()
        {
            Debug.Log($"Player Defense !");
            StaticGlobalEvent.OnDefenseButtonClick?.Invoke(true);
        }

        public void OnBuffClick()
        {
            Debug.Log($"Player OnBuffClick !");
            StaticGlobalEvent.OnBuffButtonClick?.Invoke(true);
        }

        public void OnDebuffClick()
        {
            Debug.Log($"Player OnDebuffClick !");
            StaticGlobalEvent.OnDeBuffButtonClick?.Invoke(true);
        }
    }
}

