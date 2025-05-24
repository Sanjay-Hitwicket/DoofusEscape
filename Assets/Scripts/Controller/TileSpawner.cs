using Doofus.Systems;
using UnityEngine;
using Model;

namespace  DoofusEscape {
    public class TileSpawner : BaseController{
        public override void Initialize() {
            StartTileSpawn();
        }

        private void StartTileSpawn() {
            // Logic for tile spawning
        }

        public void SetTileData(TileData tileData) {
            
        }

        public string GetTestString() {
            return "TestString";
        }
    }
}
