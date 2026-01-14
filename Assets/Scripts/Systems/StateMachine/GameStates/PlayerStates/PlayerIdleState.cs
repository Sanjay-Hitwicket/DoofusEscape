using Model;
using Systems.Animation;
using View.Player;


namespace Systems.StateMachine.GameStates.PlayerStates {
    public class PlayerIdleState : IState<BasePlayerView> {
        private BasePlayerView _playerView;

        public void OnEnter(BasePlayerView stateObject) {
            _playerView = stateObject;
            AnimatorController.Instance.PlayTrigger( stateObject.animator, "Idle");
        }
        
        public void OnExit() {
            AnimatorController.Instance.ResetTrigger(_playerView.animator, "Idle");
        }

        public void OnStay() {

        }
    }
}