using Doofus.Systems;
using DoofusEscape;
using Systems.Lightweight_DI;
using UnityEngine;

namespace View {
    public class TileSpawnerView : MonoBehaviour {
        public GameObject tilePrefab;
        public int initialTileCount = 10;
        public float maxJumpDistance = 8f;  // Increased for better spacing
        public float maxJumpHeight = 3f;    // Increased for more vertical movement
        public Vector3 tileSize = new Vector3(2, 1, 2);
        public float minGap = 2f;           // Increased minimum gap
        public float characterHeight = 2f;

        private TileSpawner tileSpawner;
        private Vector3 lastSpawnedPosition;

        private void SetControllers() {
            tileSpawner = ControllerProvider.Get<TileSpawner>();
            tileSpawner.Init(maxJumpDistance, maxJumpHeight, minGap, characterHeight);
        }

        void Start() {
            SetControllers();
            SpawnInitialTiles();
        }

        private void SpawnInitialTiles() {
            // Start with a center tile
            var centerPosition = Vector3.zero;
            Instantiate(tilePrefab, centerPosition, Quaternion.identity, transform);
            lastSpawnedPosition = centerPosition;

            // Spawn remaining tiles in a scattered pattern
            for (int i = 1; i < initialTileCount; i++) {
                Vector3 nextPos;
                if (tileSpawner.TryGetNextValidPosition(lastSpawnedPosition, tileSize, out nextPos)) {
                    Instantiate(tilePrefab, nextPos, Quaternion.identity, transform);
                    lastSpawnedPosition = nextPos;
                }
                else {
                    Debug.LogWarning($"Failed to place initial tile {i}, skipping.");
                }
            }
        }

        // This method should be called when the spawn event is triggered
        // TODO: Subscribe this method to the appropriate event
        public void OnSpawnTileEvent() {
            Vector3 nextPos;
            if (tileSpawner.TryGetNextValidPosition(lastSpawnedPosition, tileSize, out nextPos)) {
                Instantiate(tilePrefab, nextPos, Quaternion.identity, transform);
                lastSpawnedPosition = nextPos;
            }
            else {
                Debug.LogWarning("Failed to spawn new tile on event trigger.");
            }
        }
    }
}