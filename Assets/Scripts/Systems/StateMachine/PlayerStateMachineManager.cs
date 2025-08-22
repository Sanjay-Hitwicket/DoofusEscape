using System.Collections.Generic;
using System.Linq;
using Model;
using Systems.StateMachine.GameStates.PlayerStates;

namespace Systems.StateMachine {
    public class PlayerStateMachineManager: StateMachineManager<Player, PlayerStates> {
        
        public override void Init(Player player) {    
            statesDict = new Dictionary<PlayerStates, IState<Player>> {
                { PlayerStates.Movement, new PlayerMovementState()},
            };
            
            SetStateObject(statesDict.First().Value, player);
        }
    }

    public enum PlayerStates {
        Attack,
        Defend,
        Movement,
    }
}