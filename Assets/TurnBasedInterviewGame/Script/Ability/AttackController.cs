using System.Collections;
using TurnBasedGame.AI.Controller;
using UnityEngine;

namespace TurnBasedGame.Ability
{
    public class AttackController : CharacterAbility
    {
        [Header("Attack Settings")]
        public float attackRange = 5f;
        public float attackCooldown = 1f;
        public int damage = 10;
        public LayerMask enemyLayer;

        private float lastAttackTime;


        private void Update()
        {
            if (character.gameState == Type.GameState.Battle)
            {

            }
            else
            {
                if (Input.GetButtonDown("Fire1") && Time.time > lastAttackTime + attackCooldown)
                {
                    Attack();
                }
            }
        }

        public void Attack()
        {
            if (!IsAlive) return;

            lastAttackTime = Time.time;

            // Play attack animation
            UpdateAnimatorBool("IsAttacking", true);

            // Perform attack after a small delay (to sync with animation if needed)
            StartCoroutine(PerformAttack());
        }

        private IEnumerator PerformAttack()
        {
            yield return new WaitForSeconds(0.3f); // Adjust this delay to sync with the animation

            // Detect enemies within attack range
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<Character>()?.TakeDamage(character);
            }

            // Reset attack animation
            UpdateAnimatorBool("IsAttacking", false);
        }

        private void OnDrawGizmosSelected()
        {
            // Visualize attack range in the editor
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
