
using UnityEngine;

namespace TurnBasedGame.AI.Controller
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;

        // Substitution Principle, Pass a type without knowing what type is from...
        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}

