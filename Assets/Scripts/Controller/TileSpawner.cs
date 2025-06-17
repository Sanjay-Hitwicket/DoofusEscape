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
        private float characterHeight = 2f;
        private int maxRetries = 10;
        
        public override void Initialize() {
            StartTileSpawn();
        }
        
        public void Init(float maxDist, float maxHeight, float gap, float charHeight)
        {
            maxJumpDistance = maxDist;
            maxJumpHeight = maxHeight;
            minGap = gap;
            characterHeight = charHeight;
        }

        public bool TryGetNextValidPosition(Vector3 current, Vector3 tileSize, out Vector3 nextPos) {
            int maxAttempts = 50;

            for (int i = 0; i < maxAttempts; i++) {
                // Generate a random direction that's only forward and up/down
                float forwardAmount = Random.Range(0.5f, 1f); // Always move forward
                float upAmount = Random.Range(-1f, 1f); // Can go up or down
                float sideAmount = Random.Range(-0.3f, 0.3f); // Slight side movement for variety
                
                Vector3 direction = new Vector3(sideAmount, upAmount, forwardAmount).normalized;
                
                // Scale the direction by random distance within limits
                float distance = Random.Range(minGap * 2f, maxJumpDistance); // Increased minimum gap
                Vector3 offset = direction * distance;
                
                // Ensure we're always moving forward (positive Z)
                if (offset.z < 0) {
                    offset.z = -offset.z;
                }
                
                // Clamp the Y component to ensure reasonable height changes
                offset.y = Mathf.Clamp(offset.y, -maxJumpHeight, maxJumpHeight);
                
                nextPos = current + offset;

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
                // Check horizontal overlap (XZ plane)
                Vector2 posXZ = new Vector2(pos.x, pos.z);
                Vector2 newPosXZ = new Vector2(newPos.x, newPos.z);
                float horizontalDistance = Vector2.Distance(posXZ, newPosXZ);
                
                // Check vertical overlap
                float verticalDistance = Mathf.Abs(newPos.y - pos.y);
                float minVerticalGap = (tileSize.y + characterHeight) / 2f;
                
                // If tiles are close horizontally and vertically, they overlap
                if (horizontalDistance < tileSize.x + minGap * 2f && verticalDistance < minVerticalGap) { // Increased horizontal gap
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
