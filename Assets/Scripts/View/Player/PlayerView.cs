using Cysharp.Threading.Tasks;
using DoofusEscape;
using Model;
using Systems.Lightweight_DI;
using Unity.VisualScripting;
using UnityEngine;

namespace View.Player {
    public class PlayerView : BasePlayerView {
        
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
        
        // Alternative approach using SafeStartMixin
        // private void Start() {
        //     Debug.Log("PlayerView.Start called Here!");
        //     SafeStartMixin.SafeStartAsync(this, async () => {
        //         await Render();
        //     });
        // }

        private void Render() {
            _playerMovementController.SetStateMachine(this);
            _playerMovementController.PlayIdle();
        }
        
    }
}