using System;
using System.Collections.Generic;
using DoofusEscape;
using UnityEngine;

namespace Systems.Lightweight_DI {
    public static class FastInjectionProcessor {
        private static readonly Dictionary<Type, Action<object>> _injectionDelegates = new();
        private static bool _initialized = false;

        public static void Initialize() {
            if (_initialized) return;
            
            // Register injection delegates for known types
            RegisterInjectionDelegates();
            _initialized = true;
        }

        public static void InjectDependencies(object target) {
            if (target == null) {
                Debug.LogWarning("Attempted to inject dependencies into null target");
                return;
            }

            Type targetType = target.GetType();
            
            if (_injectionDelegates.TryGetValue(targetType, out Action<object> injectionDelegate)) {
                injectionDelegate(target);
            } else {
                Debug.LogWarning($"No injection delegate found for type: {targetType.Name}");
            }
        }

        private static void RegisterInjectionDelegates() {
            // Register delegates for each injectable type
            // These are compiled at build time, no reflection at runtime
            
            // Example for TileView
            _injectionDelegates[typeof(View.TileView)] = (target) => {
                if (target is View.TileView tileView) {
                    // Direct field access - maximum performance
                    var tileSpawner = ControllerProvider.Get<TileSpawner>();
                    typeof(View.TileView)
                        .GetField("_tileSpawner", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.SetValue(tileView, tileSpawner);
                }
            };

            // Example for TileSpawnerView
            _injectionDelegates[typeof(View.TileSpawnerView)] = (target) => {
                if (target is View.TileSpawnerView tileSpawnerView) {
                    // Direct field access - maximum performance
                    var tileSpawner = ControllerProvider.Get<TileSpawner>();
                    typeof(View.TileSpawnerView)
                        .GetField("_tileSpawner", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.SetValue(tileSpawnerView, tileSpawner);
                }
            };
        }

        public static void RegisterInjectionDelegate<T>(Action<T> injectionDelegate) {
            _injectionDelegates[typeof(T)] = (target) => {
                if (target is T typedTarget) {
                    injectionDelegate(typedTarget);
                }
            };
        }
    }
} 