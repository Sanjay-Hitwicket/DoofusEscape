using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Systems.TimeSystem {
    /// <summary>
    /// Use for attaching to a GameObject to create a countdown timer on Texts.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class CountDownTimer : MonoBehaviour{
        
        [SerializeField] private TMP_Text timerText;
        
        private CountDownParams _countDownParams;
        
        /// <summary>
        /// Method to call from referenced script
        /// </summary>
        /// <param name="countDownParams"></param>
        public void SetCountDown(CountDownParams countDownParams) {
            Reset();
            if (timerText == null) {
                Debug.LogWarning("Text is null");
                return;
            }
            _countDownParams = countDownParams;
            SetText();
            StartCountDown().Forget();
        }


        private void SetText(bool isComplete = false) {
            if(isComplete) {
                timerText.gameObject.SetActive(false);
                return;
            }
            var currentTime = TimeHelper.ConvertToHHMMSS(_countDownParams.duration);
            timerText.text = currentTime;
            timerText.gameObject.SetActive(true);
        }
        
        private async UniTask StartCountDown() {
            while(_countDownParams.duration > 0) {
                await UniTask.Delay(TimeSpan.FromSeconds(1),cancellationToken: _countDownParams.cancellationToken, ignoreTimeScale: false);
                if (_countDownParams.cancellationToken.IsCancellationRequested) break;
                _countDownParams.duration--;
                SetText();
            }
            SetText(true);
            _countDownParams.onCompleteCallback?.Invoke();
        }
        
        private void Reset() {
            _countDownParams = null;
        }
    }
}