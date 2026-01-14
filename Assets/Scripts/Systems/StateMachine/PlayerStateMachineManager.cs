using System.Collections.Generic;
using System.Linq;
using Systems.StateMachine.GameStates.PlayerStates;
using View.Player;

namespace Systems.StateMachine {
    public class PlayerStateMachineManager: StateMachineManager<BasePlayerView, PlayerStates> {
        
        public override void Init(BasePlayerView basePlayerView) {    
            statesDict = new Dictionary<PlayerStates, IState<BasePlayerView>> {
                { PlayerStates.Idle, new PlayerIdleState()},
                { PlayerStates.Run, new PlayerIdleState()}, // For now just for populating
            };
            
            SetStateObject(statesDict.First().Value, basePlayerView);
        }
    }

    public enum PlayerStates {
        Attack,
        Defend,
        Idle,
        Jump,
        Run
    }
}