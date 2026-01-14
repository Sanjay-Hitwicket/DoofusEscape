using System;
using DoofusEscape;
using Systems.Lightweight_DI;
using UnityEngine;

namespace View {
    public class MapSpawner : MonoBehaviour {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private float maxX = 6f;
        [SerializeField] private float maxZ = 5f;
        [SerializeField] private float tileSize = 20f;

        private void Start() {
            GenerateMap();
        }

        private void GenerateMap() {
            for (var x = 0; x < maxX; x++) {
                for (var z = 0; z < maxZ; z++) {
                    var pos = new Vector3(x*tileSize, 0, z*tileSize);
                    GameObject.Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }
}