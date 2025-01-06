using System.Collections;
using System.Collections.Generic;
using TurnBasedGame.Type;
using UnityEngine;

namespace TurnBasedGame
{
    public class Character : MonoBehaviour
    {
        public Animator animator;
        private CharacterAbility[] characterAbilities;
        private bool hasCachedAbility = false;

        public CharacterType characterType;

        private void Awake()
        {
            if(!hasCachedAbility) CacheAbilities();
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
        
        public void TakeDamage(Character attacker)
        {
            Debug.Log($"{attacker.characterType} attacked {characterType}!");
            if(attacker.characterType == CharacterType.Player)
            {
                // Enemy Attack Player
                StartCoroutine(BattleManager.Instance.PrepareBattle(attacker, this, true)); 
            }
            else 
            {
                // Player Attack Enemy
                StartCoroutine(BattleManager.Instance.PrepareBattle(attacker, this, false)); 
            }
        }

    }
}


