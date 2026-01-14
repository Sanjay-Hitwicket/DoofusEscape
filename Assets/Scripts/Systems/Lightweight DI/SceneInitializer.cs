using System.Collections;
using Cysharp.Threading.Tasks;
using Doofus.Systems;
using UnityEngine;

namespace Systems.Lightweight_DI {
    /// <summary>
    /// Primary initialization for DI system. Handles scene-based initialization before any Awake calls.
    /// Similar to Zenject's scene-based initialization approach.
    /// 
    /// Setup:
    /// 1. Add this component to a GameObject in your scene
    /// 2. Assign your BootstrapInstaller in the inspector
    /// 3. The DI system will initialize automatically before any Awake calls
    /// </summary>
    public class SceneInitializer : MonoBehaviour {
        [SerializeField] private BootstrapInstaller _bootstrapInstaller;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad() {
            // This ensures our initialization happens before any scene objects are created
            Debug.Log("SceneInitializer: Before scene load");
            var initializer = FindObjectOfType<SceneInitializer>();
            if (initializer != null) {
                initializer.InitializeScene();
            }else {
                Debug.LogWarning("SceneInitializer: No SceneInitializer found in scene. DI system may not initialize properly.");
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnAfterSceneLoad() {
            // Find the SceneInitializer in the scene and initialize
            var initializer = FindObjectOfType<SceneInitializer>();
            // if (initializer != null) {
            //     initializer.InitializeScene();
            // } else {
            //     Debug.LogWarning("SceneInitializer: No SceneInitializer found in scene. DI system may not initialize properly.");
            // }
        }
        
        private void InitializeScene() {
            Debug.Log("SceneInitializer: Initializing scene...");
            
            // Initialize the DI system before any Awake calls
            InitializeBeforeAwake().Forget();
        }
        
        private async UniTask InitializeBeforeAwake() {
            // Wait for the end of frame to ensure all objects are created
            await UniTask.WaitForEndOfFrame();
            
            // Initialize the DI system
            var context = new ControllerContext<BaseController>();
            ControllerProvider.Initialize(context);
            UltraFastInjectionProcessor.Initialize();
            
            if (_bootstrapInstaller != null) {
                _bootstrapInstaller.Install(context);
            } else {
                Debug.LogWarning("SceneInitializer: No BootstrapInstaller assigned!");
            }
            
            // Mark as injectable
            GameBootstrapper.Injectable = true;
            
            Debug.Log("SceneInitializer: Scene initialization complete");
        }
    }
}
