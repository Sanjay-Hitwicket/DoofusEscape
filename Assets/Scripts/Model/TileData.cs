using UnityEngine;

namespace Model {
    public class TileData {
        public Transform tileTransform;
        public Color tileColor;
        public TileType tileType;
    }

    public enum TileType {
        None = 0,
        Grass = 1,
        Mud = 2
    }
}