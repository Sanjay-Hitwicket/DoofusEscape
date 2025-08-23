using System;
using System.Collections.Generic;
using Doofus.Systems;
using DoofusEscape;
using Systems.Lightweight_DI;
using Systems.TimeSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View {
    public class TileView : InjectableMonoBehaviour {
        [SerializeField] private Transform tile;
        [SerializeField] private CountDownTimer countDownTimer;
        
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
            countDownTimer.SetCountDown(new () {
                duration = 1000,
                onCompleteCallback = null,
            });
        }
        
        private void SetTileColor() {
        }
        
        public void OnDataUpdated() {
        }
    }
}