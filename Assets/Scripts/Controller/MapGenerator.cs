using Systems.Lightweight_DI;
using UnityEngine;

namespace DoofusEscape {
    public class MapGenerator : BaseController{
        public override void Initialize() {
            
        }

        public void GetMapPosition(Vector3 position) {
            // Logic to get map position based on the provided Vector3 position
            Debug.Log($"Map position requested for: {position}");
            
        }
    }
}