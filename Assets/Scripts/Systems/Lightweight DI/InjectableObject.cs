namespace Systems.Lightweight_DI {
    public abstract class InjectableObject : IInjectable {
        protected InjectableObject() {
            InjectDependencies();
        }

        public virtual void InjectDependencies() {
            // Use ultra-fast injection processor instead of reflection
            UltraFastInjectionProcessor.InjectDependencies(this);
        }
    }
} 