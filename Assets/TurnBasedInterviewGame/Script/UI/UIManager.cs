using System;
using TMPro;
using TurnBasedGame.Type;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    /// <summary>
    /// I Pursposely put all in one like this, since it safe time ehe xD
    /// All related to UI
    /// </summary>
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
        [SerializeField] private Image Buff;
        [SerializeField] private Image Debuff;
        [SerializeField] private GameObject tutorial;
        [SerializeField] private GameObject gameFinishPanel;
        [SerializeField] private TextMeshProUGUI winnerText;

        private float tutorialCloseStart = 0f;
        private float tutorialCloseMax = 3f;
        private bool hasTutorialClose = false;


        private void Awake()
        {
            StaticGlobalEvent.OnGameStateChanged += GameStateHandler;
            StaticGlobalEvent.OnGameData += GameDataHandler;
            StaticGlobalEvent.OnSwitchTurn += SwitchTurnHandler;
            StaticGlobalEvent.OnCharacterDamaged += CharacterDamagedHandler;
            StaticGlobalEvent.HasWonGame += HasWonGameHandler;
        }

        private void HasWonGameHandler(bool value, CharacterType type)
        {
            if (value)
            {
                gameFinishPanel.SetActive(true);
                winnerText.text = $"Winner  : {type}";
            }
        }

        private void Update()
        {
            if (hasTutorialClose) return;
            tutorialCloseStart += Time.deltaTime;
            if (tutorialCloseStart >= tutorialCloseMax)
            {
                tutorial.SetActive(false);
                hasTutorialClose = true;
            }
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
            Buff.color = new Color(20 / 255f, 154 / 255f, 149 / 255f, 255 / 255f);
            StaticGlobalEvent.OnBuffButtonClick?.Invoke(true);
        }

        public void OnDebuffClick()
        {
            Debug.Log($"Player OnDebuffClick !");
            Debuff.color = new Color(75 / 255f, 18 / 255f, 33 / 255f, 255 / 255f);
            StaticGlobalEvent.OnDeBuffButtonClick?.Invoke(true);
        }

        public void OnCloseGameButton()
        {
            Application.Quit();
        }
    }
}

