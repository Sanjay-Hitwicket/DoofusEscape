using Doofus.Systems;
using UnityEngine;

namespace Systems.Lightweight_DI {
    public static class ControllerProvider {
        private static ControllerContext<BaseController> _context;
        private static bool _initialized = false;

        public static bool IsInitialized => _initialized;

        public static void Initialize(ControllerContext<BaseController> context) {
            if (_initialized) {
                Debug.Log("ControllerProvider: Already initialized");
                return;
            }

            Debug.Log("ControllerProvider: Initializing...");
            _context = context;
            _initialized = true;
            Debug.Log("ControllerProvider: Initialized successfully");
        }

        public static T Get<T>() where T : BaseController {
            if (!_initialized) {
                Debug.LogError("ControllerProvider: Not initialized - cannot get controller");
                return default(T);
            }

            if (_context == null) {
                Debug.LogError("ControllerProvider: Context is null - cannot get controller");
                return default(T);
            }

            var controller = _context.Get<T>();
            
            if (controller == null) {
                Debug.LogError($"ControllerProvider: Failed to get controller of type {typeof(T).Name}");
            } else {
                Debug.Log($"ControllerProvider: Successfully retrieved {typeof(T).Name}");
            }
            
            return controller;
        }
    }
}