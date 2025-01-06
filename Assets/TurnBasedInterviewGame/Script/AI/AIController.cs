using TurnBasedGame.Ability;
using UnityEngine;

namespace TurnBasedGame.AI.Controller
{
    public class AIController : CharacterAbility, IAction
    {
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private AttackController attack;
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspiscionTime = 3f;
        [SerializeField] private float waypointDwellTime = 3f;

        private GameObject player;
        private Mover mover;
        private Vector3 guardPosition;

        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArriveAtWayPoint = Mathf.Infinity;
        private float wayPointTolerance = 1f;
        private int currentWaypointIndex = 0;

        private void Start()
        {
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (InAttackRange())
            {
                AttackState();
            }
            else if (timeSinceLastSawPlayer < suspiscionTime)
            {
                SuspisionState();
            }
            else
            {
                PatrolState();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArriveAtWayPoint += Time.deltaTime;
        }

        private void PatrolState()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArriveAtWayPoint = 0;
                    CycleWayPoint();
                }

                nextPosition = GetCurrentWayPoint();
            }

            if (timeSinceArriveAtWayPoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition);
            }
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWayPoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWaypoint < wayPointTolerance;
        }

        private void SuspisionState()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackState()
        {
            timeSinceLastSawPlayer = 0;
            // Move to Player
            if(player is not null){
                mover.MoveTo(player.transform.position);
                if(IsInRange())
                {
                    Debug.Log($"Attacking Player");
                    attack.Attack();
                }
            }

        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < 3f;
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }


        public void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}
