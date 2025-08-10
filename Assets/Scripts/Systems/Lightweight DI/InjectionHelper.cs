using UnityEngine;

namespace Systems.Lightweight_DI {
    public static class InjectionHelper {
        /// <summary>
        /// Manually inject dependencies into any object using reflection-based injection
        /// </summary>
        public static void InjectInto(object target) {
            InjectionProcessor.InjectDependencies(target);
        }

        /// <summary>
        /// Manually inject dependencies into a MonoBehaviour using reflection-based injection
        /// </summary>
        public static void InjectInto(MonoBehaviour target) {
            InjectionProcessor.InjectDependencies(target);
        }

        /// <summary>
        /// Manually inject dependencies using ultra-fast injection (requires manual registration)
        /// </summary>
        public static void InjectIntoUltraFast(object target) {
            UltraFastInjectionProcessor.InjectDependencies(target);
        }

        /// <summary>
        /// Manually inject dependencies into a MonoBehaviour using ultra-fast injection (requires manual registration)
        /// </summary>
        public static void InjectIntoUltraFast(MonoBehaviour target) {
            UltraFastInjectionProcessor.InjectDependencies(target);
        }

        /// <summary>
        /// Check if the DI system is initialized
        /// </summary>
        public static bool IsInitialized() {
            return ControllerProvider.IsInitialized;
        }
    }
} 