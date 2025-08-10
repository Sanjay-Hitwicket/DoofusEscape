using UnityEngine;

namespace Systems.Lightweight_DI {
    /// <summary>
    /// InjectableMonoBehaviour that uses the ultra-fast injection processor for maximum performance.
    /// Note: This requires manual registration of injection delegates in UltraFastInjectionProcessor.
    /// </summary>
    public abstract class InjectableMonoBehaviourUltraFast : MonoBehaviour, IInjectable {
        protected virtual void Awake() {
            InjectDependencies();
        }

        public virtual void InjectDependencies() {
            // Use ultra-fast injection processor for maximum performance
            // Requires manual registration of injection delegates
            UltraFastInjectionProcessor.InjectDependencies(this);
        }
    }
} 