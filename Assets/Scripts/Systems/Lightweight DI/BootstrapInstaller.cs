using Systems.Lightweight_DI;
using UnityEngine;

namespace Doofus.Systems {
    public class BootstrapInstaller : MonoBehaviour{
        
        [SerializeField] private FeatureBootstrapper[] _features;
        
        public void Install(ControllerContext<BaseController> context) {
            Debug.Log($"BootstrapInstaller: Installing {_features?.Length ?? 0} features");
            
            if (_features == null || _features.Length == 0) {
                Debug.LogWarning("BootstrapInstaller: No features to install!");
                return;
            }
            
            foreach (var feature in _features) {
                if (feature == null) {
                    Debug.LogWarning("BootstrapInstaller: Found null feature, skipping");
                    continue;
                }
                
                Debug.Log($"BootstrapInstaller: Registering feature: {feature.GetType().Name}");
                feature.Register(context);
            }
            
            Debug.Log("BootstrapInstaller: Installation complete");
        }
    }
}