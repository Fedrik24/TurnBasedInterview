using System.Collections;
using System.Collections.Generic;
using TurnBasedGame.Type;
using UnityEngine;

namespace TurnBasedGame
{
    /// <summary>
    /// This is act as Core for character model.
    /// </summary>
    public class Character : MonoBehaviour
    {
        
        // Character Data
        [SerializeField] private float healthPoint;
        [SerializeField] private float defensePoint;
        [SerializeField] private int attack;

        public Animator animator;
        public GameState gameState;
        private CharacterAbility[] characterAbilities;
        private bool hasCachedAbility = false;
        public CharacterType characterType;
        public CharacterData characterData;

        private void Awake()
        {
            if (!hasCachedAbility) CacheAbilities();
            //gameState = GameState.Exploring; // Temp
            SetUpCharacterData();
        }

        private void SetUpCharacterData()
        {
            characterData.HealthPoint = healthPoint;
            characterData.DefensePoint = defensePoint;
            characterData.Attack = attack;
        }


        private void CacheAbilities()
        {
            // Cache All Character Ability
            characterAbilities = gameObject.GetComponentsInChildren<CharacterAbility>();
            List<CharacterAbility> tempCharacterAbilities = new List<CharacterAbility>();

            for (int i = 0; i < characterAbilities.Length; i++)
            {
                tempCharacterAbilities.Add(characterAbilities[i]);
            }

            characterAbilities = tempCharacterAbilities.ToArray();
            hasCachedAbility = true;
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            foreach (CharacterAbility characterAbility in characterAbilities)
            {
                if (characterAbility.enabled)
                {
                    characterAbility.UpdateAnimator();
                }
            }
        }

        public void SetGameState(GameState state)
        {
            gameState = state;
        }

        public void TakeDamage(Character attacker)
        {
            Debug.Log($"Attacker Type : {attacker.characterType}");
            StartCoroutine(BattleManager.Instance.PrepareBattle(attacker, this, attacker.characterType == CharacterType.Enemy ? false : true));
        }
    }
}


