using Model;

namespace Systems.StateMachine {
    public class PlayerStateMachine: StateMachine<Player, PlayerStates> {
        
    }
    
    public enum PlayerStates {
        Attack,
        Defend,
        Movement,
    }
}