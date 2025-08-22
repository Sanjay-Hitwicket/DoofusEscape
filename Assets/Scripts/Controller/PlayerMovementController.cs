using Cysharp.Threading.Tasks;
using Model;
using Systems.Lightweight_DI;
using Systems.StateMachine;
using UnityEngine;

namespace DoofusEscape {
    public class PlayerMovementController : BaseController {

        private PlayerStateMachineManager playerStateMachineManager;
        
        public override void Initialize() {
            var player = MockPlayerData();
            SetStateMachine(player);
        }
        
        private void SetStateMachine(Player player) {
            playerStateMachineManager = new PlayerStateMachineManager();
            playerStateMachineManager.Init(player);
        }

        public void SayHi() {
            Debug.Log("Hi");
        }
        
        private Player MockPlayerData() {
            var player = new Player {
            };
            return player;
        }
        
    }
}