using System;
using System.Collections.Generic;
using Doofus.Systems;
using DoofusEscape;
using Systems.Lightweight_DI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View {
    public class TileView : InjectableMonoBehaviour {
        [SerializeField] private Transform tile;
        
        [Inject] private TileSpawner _tileSpawner;
        
        private void Start() {
            Render();
        }
        
        public void Render() {
            SetTileColor();
            SetTimer();
        }
        
        private void SetTileData() {
        }
        
        private void SetTimer() {
             
        }
        
        private void SetTileColor() {
        }
        
        public void OnDataUpdated() {
        }
    }
}