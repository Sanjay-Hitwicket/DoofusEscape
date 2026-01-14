using Doofus.Systems;
using UnityEngine;

namespace Systems.Lightweight_DI {
    /// <summary>
    /// Fallback initialization for DI system when SceneInitializer is not used.
    /// For robust initialization, prefer using SceneInitializer.
    /// </summary>
    public class GameBootstrapper : MonoBehaviour {

        [SerializeField] private BootstrapInstaller _bootstrapInstaller;
        
        public ControllerContext<BaseController> Context { get; private set; }
        public static bool Injectable { get; set; }
        
        public static GameBootstrapper Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Init() => Instance = default;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            } else {
                Instance = this;
            }
            
            // Only initialize if not already done by SceneInitializer
            if (!Injectable) {
                InitializeDI();
            }
        }
        
        private void InitializeDI() {
            Debug.Log("GameBootstrapper: Initializing DI system (fallback mode)...");
            
            Context = new ControllerContext<BaseController>();
            ControllerProvider.Initialize(Context);
            UltraFastInjectionProcessor.Initialize();

            if (_bootstrapInstaller != null) {
                _bootstrapInstaller.Install(Context);
            } else {
                Debug.LogWarning("GameBootstrapper: No BootstrapInstaller assigned!");
            }
            
            Injectable = true;
            Debug.Log("GameBootstrapper: DI system initialized (fallback mode)");
        }
    }
}