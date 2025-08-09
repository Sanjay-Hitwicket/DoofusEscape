using System;
using System.Collections.Generic;

namespace Systems.StateMachine {
    public abstract class StateMachine<T, EState> where EState : Enum{
        
        protected IState<T> currentStateObject;
        protected T stateObject = default;
        protected Dictionary<EState, IState<T>> statesDict = new ();
        
        public void SetStateObject(T stateObject) {
            this.stateObject = stateObject;
            currentStateObject?.OnEnter(stateObject);
        }
        
        public void ChangeState(EState stateName) {
            IState<T> newState = GetState(stateName);
            if (newState == null) {
                Console.WriteLine($"Failed to change state to {stateName}.");
                return;
            }
            currentStateObject?.OnExit();
            currentStateObject = newState;
            currentStateObject?.OnEnter(stateObject);
        }

        protected IState<T> GetState(EState stateName) {
            return statesDict.ContainsKey(stateName) ? statesDict[stateName] : null;
        }
        
    }
}