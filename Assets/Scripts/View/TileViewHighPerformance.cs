using DoofusEscape;
using Systems.Lightweight_DI;
using UnityEngine;

namespace View {
    /// <summary>
    /// High-performance TileView using public properties for injection
    /// No reflection, no runtime overhead - maximum performance
    /// Just an example of how to use public properties for injection
    /// </summary>
    public class TileViewHighPerformance : InjectableMonoBehaviourWithProperties {
        [SerializeField] private Transform tile;
        
        // Public property for injection - fastest approach
        public TileSpawner TileSpawner { get; set; }
        
        private void Start() {
            Render();
        }
        
        public void Render() {
            SetTileColor();
            SetTimer();
        }
        
        protected override void OnInjectDependencies() {
            // Direct assignment - no reflection, maximum performance
            TileSpawner = ControllerProvider.Get<TileSpawner>();
        }
        
        private void SetTileData() {
        }
        
        private void SetTimer() {
             
        }
        
        private void SetTileColor() {
        }
        
        public void OnDataUpdated() {
        }
    }
} 