using Doofus.Systems;
using DoofusEscape;

namespace Bootstrappers {
    public class TileBootstrapper : FeatureBootstrapper {
        public override void Register(ControllerContext<BaseController> context) {
            context.Register(new TileSpawner());
        }
    }
}