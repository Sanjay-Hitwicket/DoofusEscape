using Doofus.Systems;
using DoofusEscape;
using Systems.Lightweight_DI;
using UnityEngine;

namespace View {
    public class TileSpawnerView : InjectableMonoBehaviour {
        public GameObject tilePrefab;
        public int initialTileCount = 10;
        public float maxJumpDistance = 8f;  // Increased for better spacing
        public float maxJumpHeight = 3f;    // Increased for more vertical movement
        public Vector3 tileSize = new Vector3(2, 1, 2);
        public float minGap = 2f;           // Increased minimum gap
        public float characterHeight = 2f;

        [Inject] private TileSpawner _tileSpawner;
        private Vector3 lastSpawnedPosition;

        void Start() {
            Debug.Log("TileSpawnerView: Start called");
            
            // Check if injection worked
            if (_tileSpawner == null) {
                Debug.LogError("TileSpawnerView: _tileSpawner is null! Injection may have failed.");
                return;
            }
            
            Debug.Log("TileSpawnerView: _tileSpawner is not null, proceeding with initialization");
            
            InitializeTileSpawner();
            SpawnInitialTiles();
        }

        private void InitializeTileSpawner() {
            if (_tileSpawner == null) {
                Debug.LogError("TileSpawnerView: Cannot initialize - _tileSpawner is null!");
                return;
            }
            
            Debug.Log("TileSpawnerView: Initializing TileSpawner with parameters");
            _tileSpawner.Init(maxJumpDistance, maxJumpHeight, minGap, characterHeight);
        }

        private void SpawnInitialTiles() {
            if (_tileSpawner == null) {
                Debug.LogError("TileSpawnerView: Cannot spawn tiles - _tileSpawner is null!");
                return;
            }
            
            Debug.Log("TileSpawnerView: Starting to spawn initial tiles");
            
            // Start with a center tile
            var centerPosition = Vector3.zero;
            Instantiate(tilePrefab, centerPosition, Quaternion.identity, transform);
            lastSpawnedPosition = centerPosition;

            // Spawn remaining tiles in a scattered pattern
            for (int i = 1; i < initialTileCount; i++) {
                Vector3 nextPos;
                if (_tileSpawner.TryGetNextValidPosition(lastSpawnedPosition, tileSize, out nextPos)) {
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
            if (_tileSpawner == null) {
                Debug.LogError("TileSpawnerView: Cannot spawn tile - _tileSpawner is null!");
                return;
            }
            
            Vector3 nextPos;
            if (_tileSpawner.TryGetNextValidPosition(lastSpawnedPosition, tileSize, out nextPos)) {
                Instantiate(tilePrefab, nextPos, Quaternion.identity, transform);
                lastSpawnedPosition = nextPos;
            }
            else {
                Debug.LogWarning("Failed to spawn new tile on event trigger.");
            }
        }
    }
}