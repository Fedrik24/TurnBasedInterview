using System.Collections;
using System.Collections.Generic;
using TurnBasedGame.Type;
using UnityEngine;

namespace TurnBasedGame
{
    public class Character : MonoBehaviour
    {
        public Animator animator;
        public GameState gameState;
        private CharacterAbility[] characterAbilities;
        private bool hasCachedAbility = false;
        public CharacterType characterType;
        public CharacterData characterData;

        // Character Data
        [SerializeField] private float healthPoint;
        [SerializeField] private float defensePoint;
        [SerializeField] private int attack;
        

        private void Awake()
        {
            if(!hasCachedAbility) CacheAbilities();
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

            for(int i = 0; i < characterAbilities.Length; i++)
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

        private void UpdateAnimator(){
            foreach(CharacterAbility characterAbility in characterAbilities){
                if(characterAbility.enabled){
                    characterAbility.UpdateAnimator();
                }
            }
        }

        public void SetGameState(GameState state)
        {
            Debug.Log($"Is This Called From Game Manaer?");
            Debug.Log($"GameState Before : {gameState}");
            gameState = state;
            Debug.Log($"GameState After : {gameState}");
        }
        
        public void TakeDamage(Character attacker)
        {
            if(attacker.characterType == CharacterType.Player)
            {
                // Enemy Attack Player
                StartCoroutine(BattleManager.Instance.PrepareBattle(attacker, this, false)); 
            }
            else 
            {
                // Player Attack Enemy
                StartCoroutine(BattleManager.Instance.PrepareBattle(attacker, this, true)); 
            }
        }

    }
}


