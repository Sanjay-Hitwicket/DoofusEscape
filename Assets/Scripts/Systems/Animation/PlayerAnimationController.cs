using System;
using UnityEngine;

namespace Systems.Animation {
    public class PlayerAnimationController : AnimatorController{
        public static PlayerAnimationController Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }
    }
}