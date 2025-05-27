using Doofus.Systems;
using DoofusEscape;
using UnityEngine;

namespace View {
    public class TileSpawnerView : MonoBehaviour {
        public GameObject tilePrefab;
        public int numberOfTiles = 20;
        public float maxJumpDistance = 5f;
        public float maxJumpHeight = 2f;
        public Vector3 tileSize = new Vector3(2, 1, 2);
        public float minGap = 1f;

        private TileSpawner tileSpawner;

        private void SetControllers() {
            tileSpawner = ControllerProvider.Get<TileSpawner>();
            tileSpawner.Init(maxJumpDistance, maxJumpHeight, minGap);
        }

        void Start() {
            SetControllers();

            var currentPosition = Vector3.zero;

            for (int i = 0; i < numberOfTiles; i++) {
                Vector3 nextPos;
                if (tileSpawner.TryGetNextValidPosition(currentPosition, tileSize, out nextPos)) {
                    Instantiate(tilePrefab, nextPos, Quaternion.identity, transform);
                    currentPosition = nextPos;
                }
                else {
                    Debug.LogWarning($"Failed to place tile {i}, skipping.");
                }
            }
        }
    }
}