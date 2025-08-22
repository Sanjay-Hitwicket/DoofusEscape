using Doofus.Systems;
using DoofusEscape;
using Systems.Lightweight_DI;

namespace Bootstrappers {
    public class PlayerBootstrapper: FeatureBootstrapper {
        public override void Register(ControllerContext<BaseController> context) {
            context.Register(new PlayerMovementController());
        }
    }
}