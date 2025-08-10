using Doofus.Systems;
using UnityEngine;

namespace Systems.Lightweight_DI {
    public class GameBootstrapper : MonoBehaviour
    {

        [SerializeField] private BootstrapInstaller _bootstrapInstaller;
        
        public ControllerContext<BaseController> Context { get; private set; }

        #region Singleton
        public static GameBootstrapper Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Init() => Instance = default;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            } else {
                Instance = this;
            }
            
            Context = new ControllerContext<BaseController>();
            
            ControllerProvider.Initialize(Context);

            // Initialize the ultra-fast injection processor
            UltraFastInjectionProcessor.Initialize();

            _bootstrapInstaller.Install(Context);
        }
        #endregion
        
        
        
    }
}