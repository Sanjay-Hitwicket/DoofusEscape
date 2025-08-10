using UnityEngine;
using DoofusEscape;
using Model;

namespace Systems.Lightweight_DI {
    /// <summary>
    /// Example 1: Using InjectableMonoBehaviour (Automatic injection in Awake)
    /// </summary>
    public class ExampleInjectableMonoBehaviour : InjectableMonoBehaviour {
        [Inject] private TileSpawner _tileSpawner;
        
        private void Start() {
            // _tileSpawner is already injected and ready to use
            Debug.Log($"TileSpawner injected: {_tileSpawner != null}");
        }
    }

    /// <summary>
    /// Example 2: Using InjectableObject (Automatic injection in constructor)
    /// </summary>
    public class ExampleInjectableObject : InjectableObject {
        [Inject] private TileSpawner _tileSpawner;
        
        public void DoSomething() {
            // _tileSpawner is already injected and ready to use
            Debug.Log($"TileSpawner injected: {_tileSpawner != null}");
        }
    }

    /// <summary>
    /// Example 3: Manual injection with regular MonoBehaviour
    /// </summary>
    public class ExampleManualInjection : MonoBehaviour {
        [Inject] private TileSpawner _tileSpawner;
        
        private void Start() {
            // Manually trigger injection
            InjectionHelper.InjectInto(this);
            
            // Now _tileSpawner is injected and ready to use
            Debug.Log($"TileSpawner injected: {_tileSpawner != null}");
        }
    }

    /// <summary>
    /// Example 4: Manual injection with regular class
    /// </summary>
    public class ExampleManualInjectionClass {
        [Inject] private TileSpawner _tileSpawner;
        
        public ExampleManualInjectionClass() {
            // Manually trigger injection
            InjectionHelper.InjectInto(this);
            
            // Now _tileSpawner is injected and ready to use
            Debug.Log($"TileSpawner injected: {_tileSpawner != null}");
        }
    }

    /// <summary>
    /// Example 5: Multiple dependencies
    /// </summary>
    public class ExampleMultipleDependencies : InjectableMonoBehaviour {
        [Inject] private TileSpawner _tileSpawner;
        [Inject] private Player _player; // Assuming you have a Player controller
        
        private void Start() {
            // Both dependencies are automatically injected
            Debug.Log($"TileSpawner: {_tileSpawner != null}, Player: {_player != null}");
        }
    }

    /// <summary>
    /// Example 6: Conditional injection
    /// </summary>
    public class ExampleConditionalInjection : MonoBehaviour {
        [Inject] private TileSpawner _tileSpawner;
        
        private void Start() {
            // Only inject if the system is initialized
            if (InjectionHelper.IsInitialized()) {
                InjectionHelper.InjectInto(this);
                Debug.Log("Dependencies injected successfully");
            } else {
                Debug.LogWarning("DI system not initialized, skipping injection");
            }
        }
    }
} 