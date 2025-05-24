using UnityEngine;

namespace Doofus.Systems {
    public class BootstrapInstaller : MonoBehaviour{
        
        [SerializeField] private FeatureBootstrapper[] _features;
        
        public void Install(ControllerContext<BaseController> context) {
            foreach (var featrure in _features) {
                featrure.Register(context);
            }
        }
    }
}