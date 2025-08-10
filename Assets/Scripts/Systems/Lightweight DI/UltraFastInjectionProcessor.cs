using System;
using System.Collections.Generic;
using DoofusEscape;
using UnityEngine;

namespace Systems.Lightweight_DI {
    public static class UltraFastInjectionProcessor {
        private static readonly Dictionary<Type, Action<object>> _injectionDelegates = new();
        private static bool _initialized = false;

        public static void Initialize() {
            if (_initialized) return;
            
            Debug.Log("UltraFastInjectionProcessor: Initializing...");
            
            // Register ultra-fast injection delegates
            RegisterUltraFastDelegates();
            _initialized = true;
            
            Debug.Log($"UltraFastInjectionProcessor: Initialized with {_injectionDelegates.Count} registered types");
        }

        public static void InjectDependencies(object target) {
            if (target == null) {
                Debug.LogWarning("UltraFastInjectionProcessor: Attempted to inject dependencies into null target");
                return;
            }

            Type targetType = target.GetType();
            
            if (_injectionDelegates.TryGetValue(targetType, out Action<object> injectionDelegate)) {
                Debug.Log($"UltraFastInjectionProcessor: Injecting dependencies into {targetType.Name}");
                injectionDelegate(target);
            } else {
                Debug.LogWarning($"UltraFastInjectionProcessor: No injection delegate found for type: {targetType.Name}");
            }
        }

        private static void RegisterUltraFastDelegates() {
            // These delegates are compiled at build time
            // No reflection, no runtime overhead, maximum performance
            
            // NOTE: Uncomment and customize these delegates for maximum performance
            // This approach requires manual registration for each type but provides the best performance
            
            #region Examples - Uncomment and customize as needed
            
            // TileView injection - direct field access
            // _injectionDelegates[typeof(View.TileView)] = (target) => {
            //     var tileView = (View.TileView)target;
            //     var tileSpawner = ControllerProvider.Get<TileSpawner>();
            //     
            //     if (tileSpawner == null) {
            //         Debug.LogError("UltraFastInjectionProcessor: Failed to get TileSpawner from ControllerProvider");
            //         return;
            //     }
            //     
            //     // Direct field assignment using reflection only once at startup
            //     var field = typeof(View.TileView).GetField("_tileSpawner", 
            //         System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //     field?.SetValue(tileView, tileSpawner);
            //     
            //     Debug.Log($"UltraFastInjectionProcessor: Successfully injected TileSpawner into TileView");
            // };

            // TileSpawnerView injection - direct field access
            // _injectionDelegates[typeof(View.TileSpawnerView)] = (target) => {
            //     var tileSpawnerView = (View.TileSpawnerView)target;
            //     var tileSpawner = ControllerProvider.Get<TileSpawner>();
            //     
            //     if (tileSpawner == null) {
            //         Debug.LogError("UltraFastInjectionProcessor: Failed to get TileSpawner from ControllerProvider");
            //         return;
            //     }
            //     
            //     // Direct field assignment using reflection only once at startup
            //     var field = typeof(View.TileSpawnerView).GetField("_tileSpawner", 
            //         System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //     field?.SetValue(tileSpawnerView, tileSpawner);
            //     
            //     Debug.Log($"UltraFastInjectionProcessor: Successfully injected TileSpawner into TileSpawnerView");
            // };
            
            #endregion
        }

        // Alternative approach: Use public properties for even better performance
        public static void RegisterInjectionDelegate<T>(Action<T> injectionDelegate) {
            _injectionDelegates[typeof(T)] = (target) => {
                if (target is T typedTarget) {
                    injectionDelegate(typedTarget);
                }
            };
        }
    }
} 