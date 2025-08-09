namespace Systems.StateMachine {
    public interface IState<T> {
        protected internal abstract void OnEnter(T stateObject);
        protected internal abstract void OnExit();
        protected void OnStay();
    }
}