namespace Systems.StateMachine {
    public interface IState<T> {
        public abstract void OnEnter(T stateObject);

        public abstract void OnExit();

        public abstract void OnStay();
    }
}