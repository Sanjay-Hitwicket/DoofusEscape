using Systems.Lightweight_DI;
using Systems.StateMachine;
using UnityEngine;
using View.Player;

namespace DoofusEscape {
    public class PlayerMovementController : BaseController {
        
        private PlayerStateMachineManager playerStateMachineManager;
        
        public override void Initialize() {
        }
        
        public void SetStateMachine(BasePlayerView playerView) {
            //var player = MockPlayerData();
            playerStateMachineManager = new PlayerStateMachineManager();
            playerStateMachineManager.Init(playerView);
        }

        private void PlayRun() {
            playerStateMachineManager.ChangeState(PlayerStates.Run);
        }
        
        public void PlayIdle() {
            playerStateMachineManager.ChangeState(PlayerStates.Idle);
        }
        
        private void PlayJump() {
            playerStateMachineManager.ChangeState(PlayerStates.Jump);
        }
        
        public void SayHi() {
            // Just for testing
            Debug.Log("Say Hi");
        }
        
        private BasePlayerView MockPlayerData() {
            var player = new BasePlayerView {
            };
            return player;
        }
        
    }
}