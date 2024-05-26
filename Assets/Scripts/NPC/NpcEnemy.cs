using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPCE
{
    public class NpcEnemy : NPCEn
    {
        public Animator anim;
        public float attackRange = 2.0f;
        public float chaseRange = 10.0f;
        public float stopAndAttackRange = 1.5f;
        public float damage = 10.0f;

        private NavMeshAgent agent;
        private Transform playerTransform;
        private bool isChasing = false;
        private bool isAttacking = false;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        public override void Interact()
        {
            if (!isChasing)
            {
                isChasing = true;
                StartCoroutine(ChaseAndAttackPlayer());
            }
        }

        private IEnumerator ChaseAndAttackPlayer()
        {
            while (isChasing)
            {
                float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

                if (distanceToPlayer <= attackRange && !isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(AttackPlayer());
                }
                else if (distanceToPlayer <= chaseRange && distanceToPlayer > stopAndAttackRange)
                {
                    agent.SetDestination(playerTransform.position);
                    float speed = agent.velocity.magnitude;
                    float normalizedSpeed = speed / agent.speed; // Normalize speed between 0 and 1
                    anim.SetFloat("Speed", normalizedSpeed);
                }
                else
                {
                    // Stop chasing if the player is out of chase range
                    isChasing = false;
                }

                yield return new WaitForSeconds(0.1f); // Small delay to optimize performance
            }
        }

        private IEnumerator AttackPlayer()
        {
            while (isAttacking)
            {
                float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

                if (distanceToPlayer > attackRange)
                {
                    isAttacking = false;
                    yield break;
                }

                // Perform attack (example: reduce player health)
                Debug.Log("Enemy attacks the player!");
                //playerTransform.GetComponent<PlayerHealth>().TakeDamage(damage);

                anim.SetTrigger("Attack");
                Debug.Log("player takes damage");
                playerTransform.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);

                // Wait for 4 seconds between attacks
                yield return new WaitForSeconds(4.0f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}
