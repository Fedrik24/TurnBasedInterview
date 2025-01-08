using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Ability
{
    /// <summary>
    /// This class is ability for character to be able to attack.
    /// </summary>
    public class AttackController : CharacterAbility
    {
        [Header("Attack Settings")]
        public float attackRange = 5f;
        public float attackCooldown = 1f;
        public int damage = 10;
        public LayerMask enemyLayer;

        public void Attack()
        {
            if (!IsAlive) return;

            UpdateAnimatorBool("IsAttacking", true);

            StartCoroutine(PerformAttack());
        }

        private IEnumerator PerformAttack()
        {
            yield return new WaitForSeconds(0.3f); 

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            HashSet<Character> processedCharacters = new HashSet<Character>();

            foreach (Collider enemy in hitEnemies)
            {
                Character character = enemy.GetComponent<Character>();
                if (character != null && !processedCharacters.Contains(character))
                {
                    processedCharacters.Add(character);
                    character.TakeDamage(this.character);
                }
            }

            UpdateAnimatorBool("IsAttacking", false);
        }
    }
}
