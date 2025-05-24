using UnityEngine;

namespace Doofus.Systems {
    public abstract class FeatureBootstrapper : MonoBehaviour
    {
        public abstract void Register(ControllerContext<BaseController> context);
    }
}