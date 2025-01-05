using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame
{
    public class CharacterAbility : MonoBehaviour
    {
        public bool IsAlive = true;
        private Animator animator;
        private Character character;

        private void Awake()
        {
            character = gameObject.GetComponent<Character>();

            if(character is not null){
                animator = character.animator;
            }
        }

        public virtual void UpdateAnimator()
        {

        }

        public void UpdateAnimatorFloat(string parameter, float value)
        {
            animator.SetFloat(parameter, value);
        }

        public void UpdateAnimatorBool(string parameter, bool value)
        {
            animator.SetBool(parameter, value);
        }
    }
}
