using UnityEngine;

namespace TurnBasedGame
{
    /// <summary>
    /// This is act as for each character ability.
    /// like movement or attacking or even attack with range or dashing etc it will 
    /// and always derived from this. 
    /// </summary>
    public class CharacterAbility : MonoBehaviour
    {
        public bool IsAlive = true;
        private Animator animator;
        [HideInInspector] public Character character;

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
