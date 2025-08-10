using System;
using System.Collections.Generic;
using UnityEngine;

namespace SO {
    [CreateAssetMenu(fileName = "TilesLibrary", menuName = "SO/TilesLibrary")]
    public sealed class TilesLibrary : ScriptableObject {
        [SerializeField] private List<TileReference> tileReferences;
        
        public TileReference GetTileReference(TileName tileName) {
            foreach (var tileRef in tileReferences) {
                if (tileRef.tileType == tileName) {
                    return tileRef;
                }
            }
            return null;
        }
    }
    
    [Serializable]
    public class TileReference {
        public TileName tileType;
        public GameObject tilePrefab;
    }

    public enum TileName {
        Grass,
        Water,
        Sand,
        Rock
    }
}