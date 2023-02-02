using UnityEngine;

namespace GGJ.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        State currentState;


        private void Update()
        {
            currentState?.Tick(Time.deltaTime);
        }

        public void SwitchState(State nextState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState?.Enter();
        }
    }
}