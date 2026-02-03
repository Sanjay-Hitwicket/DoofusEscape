using Cysharp.Threading.Tasks;
using DoofusEscape;
using Model;
using Systems.Lightweight_DI;
using Unity.VisualScripting;
using UnityEngine;
using View.Entities;

namespace View.Player {
    public class PlayerView : BasePlayerView, IEntity {
        
        [Inject] private readonly PlayerMovementController _playerMovementController;

        protected override void Awake() {
            base.Awake();
            Debug.Log("PlayerView.Awake called Here!");
        }

        protected override void OnInjectionComplete() {
            Debug.Log("PlayerView.OnInjectionComplete called Here!");
            
            // Now safe to use injected dependencies
            Render();
        }

        private void Render() {
            _playerMovementController.SetStateMachine(this);
            _playerMovementController.PlayIdle();
        }

        public void Attack() {
            
        }

        public void TakeDamage(int damage) {
            
        }

        public void Die() {
            
        }
    }
}