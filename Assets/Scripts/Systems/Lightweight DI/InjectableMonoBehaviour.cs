using UnityEngine;

namespace Systems.Lightweight_DI {
    public abstract class InjectableMonoBehaviour : MonoBehaviour, IInjectable {
        protected virtual void Awake() {
            InjectDependencies();
        }

        public virtual void InjectDependencies() {
            // Use reflection-based injection by default (easier to use, no manual registration needed)
            // For maximum performance, override this method to use UltraFastInjectionProcessor
            InjectionProcessor.InjectDependencies(this);
        }
    }
} 