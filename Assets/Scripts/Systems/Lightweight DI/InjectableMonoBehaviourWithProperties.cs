using UnityEngine;

namespace Systems.Lightweight_DI {
    /// <summary>
    /// Alternative InjectableMonoBehaviour that uses public properties for maximum performance
    /// No reflection, no runtime overhead - perfect for high-performance games
    /// </summary>
    public abstract class InjectableMonoBehaviourWithProperties : MonoBehaviour, IInjectable {
        protected virtual void Awake() {
            InjectDependencies();
        }

        public virtual void InjectDependencies() {
            // Override this method in derived classes to implement custom injection
            // This approach requires manual implementation but provides maximum performance
            OnInjectDependencies();
        }

        /// <summary>
        /// Override this method to implement custom injection logic
        /// This is the fastest approach - no reflection, no runtime overhead
        /// </summary>
        protected virtual void OnInjectDependencies() {
            // Override in derived classes
        }
    }
} 