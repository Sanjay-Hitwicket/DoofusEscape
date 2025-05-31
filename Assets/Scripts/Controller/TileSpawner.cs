using System.Collections.Generic;
using Doofus.Systems;
using UnityEngine;
using Model;

namespace  DoofusEscape {
    public class TileSpawner : BaseController{
        
        private List<Vector3> spawnedPositions = new List<Vector3>();
        private float maxJumpDistance = 5f;
        private float maxJumpHeight = 2f;
        private float minGap = 1f;
        private int maxRetries = 10;
        
        public override void Initialize() {
            StartTileSpawn();
        }
        
        public void Init(float maxDist, float maxHeight, float gap)
        {
            maxJumpDistance = maxDist;
            maxJumpHeight = maxHeight;
            minGap = gap;
        }

        public bool TryGetNextValidPosition(Vector3 current, Vector3 tileSize, out Vector3 nextPos) {
            int maxAttempts = 50;

            for (int i = 0; i < maxAttempts; i++) {
                float xOffset = Random.Range(-maxJumpDistance, maxJumpDistance);
                float zOffset = Random.Range(-maxJumpDistance, maxJumpDistance);
                float yOffset = Random.Range(-maxJumpHeight, maxJumpHeight);

                nextPos = current + new Vector3(xOffset, yOffset, zOffset);

                if (!IsOverlapping(nextPos, tileSize)) {
                    spawnedPositions.Add(nextPos);
                    return true;
                }
            }

            nextPos = Vector3.zero;
            return false;
        }

        private bool IsOverlapping(Vector3 newPos, Vector3 tileSize) {
            foreach (Vector3 pos in spawnedPositions) {
                if (Vector3.Distance(pos, newPos) < tileSize.magnitude + minGap) {
                    return true;
                }
            }
            return false;
        }
        
        private void StartTileSpawn() {
        }

        public void SetTileData(TileData tileData) {
            
        }
    }
}
