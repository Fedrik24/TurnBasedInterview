using UnityEngine;
using UnityEngine.AI;

namespace TurnBasedGame.AI.Controller
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent playerAgent;
        private Animator characterAnimator;

        private void Start()
        {
            playerAgent = GetComponent<NavMeshAgent>();
            characterAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            playerAgent.destination = destination;
            playerAgent.isStopped = false;
        }

        public void Cancel()
        {
            playerAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = playerAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float characterSpeed = localVelocity.z;
            characterAnimator.SetFloat("forwardSpeed", characterSpeed);
        }
    }
}

