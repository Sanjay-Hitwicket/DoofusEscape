using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Systems.Lightweight_DI {
    public abstract class InjectableMonoBehaviour : MonoBehaviour, IInjectable {
        private bool _injectionComplete = false;
        private bool _startCalled = false;
        
        protected virtual void Awake() {
            // Start the injection process
            InjectDependencies();
        }

        public virtual void InjectDependencies() {
            // Use reflection-based injection by default (easier to use, no manual registration needed)
            // For maximum performance, override this method to use UltraFastInjectionProcessor
            InjectionProcessor.InjectDependencies(this);
            _injectionComplete = true;
            
            // If Start has already been called, trigger OnInjectionComplete now
            if (_startCalled) {
                OnInjectionComplete();
            }
        }
        
        private async void Start() {
            _startCalled = true;
            
            // Wait for injection to complete if it hasn't already
            if (!_injectionComplete) {
                await UniTask.WaitUntil(() => _injectionComplete);
            }
            
            // Call the lifecycle method after injection is complete
            OnInjectionComplete();
        }
        
        /// <summary>
        /// Called automatically after injection is complete and Start is called.
        /// Override this method instead of Start() when you need injected dependencies.
        /// </summary>
        protected virtual void OnInjectionComplete() {
            // Override this method in derived classes
        }
        
        /// <summary>
        /// Check if dependency injection has completed
        /// </summary>
        protected bool IsInjectionComplete => _injectionComplete;
    }
} 