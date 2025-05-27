using System;
using System.Collections.Generic;
using Doofus.Systems;
using DoofusEscape;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View {
    public class TileView : MonoBehaviour {
        [SerializeField] private Transform tile;
        
        private TileSpawner tileSpawner;
        
        private void Start() {
            Render();
        }
        
        public void Render() {
            SetConrollers();
            SetTileColor();
            SetTimer();
        }
        
        private void SetTileData() {
        }
        
        private void SetConrollers() {
            tileSpawner = ControllerProvider.Get<TileSpawner>();
        }
        
        private void SetTimer() {
            Debug.Log("Testing with Test string in controller ::: ");
            var str = tileSpawner.GetTestString();
            Debug.Log(str);
        }
        
        private void SetTileColor() {
        }
        
        public void OnDataUpdated() {
        }
    }
}