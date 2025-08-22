using System;
using System.Collections.Generic;

namespace Systems.StateMachine {
    public abstract class StateMachineManager<T, EState> where EState : Enum{
        
        private IState<T> currentStateObject;
        private T stateModel = default;
        
        protected Dictionary<EState, IState<T>> statesDict = new ();
        
        public void SetStateObject(IState<T> stateObject, T stateModel) {
            this.currentStateObject = stateObject;
            this.stateModel = stateModel;
            currentStateObject?.OnEnter(stateModel);
        }
        
        public void ChangeState(EState stateName) {
            var newState = GetState(stateName);
            if (newState == null) {
                var stateType = Type.GetType($"Systems.StateMachine.States.{stateName}State`1");
                newState = Activator.CreateInstance(stateType) as IState<T>;
                SetState(stateName, newState);
            }
            currentStateObject?.OnExit();
            currentStateObject = newState;
            currentStateObject?.OnEnter(stateModel);
        }

        private IState<T> GetState(EState stateName) {
            return statesDict.ContainsKey(stateName) ? statesDict[stateName] : null;
        }

        private void SetState(EState stateName, IState<T> stateObject) {
            statesDict[stateName] = stateObject;
        }
        
        public abstract void Init(T stateModel);
    }
}