using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame
{
    public class Character : MonoBehaviour
    {
        public Animator animator;
        private CharacterAbility[] characterAbilities;
        private bool hasCachedAbility = false;

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

    }
}

