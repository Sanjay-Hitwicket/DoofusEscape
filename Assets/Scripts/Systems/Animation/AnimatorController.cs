using System.Collections.Generic;
using UnityEngine;

namespace Systems.Animation {
    public class AnimatorController : GenericSingleton<AnimatorController>, IAnimatorController {
        
        public void ResetTrigger(Animator animator, string trigger) {
            animator.ResetTrigger(trigger);
        }
        
        public void PlayTrigger(Animator animator, string trigger) {
            animator.ResetTrigger(trigger);
            animator.SetTrigger(trigger);
            animator.Play(trigger);
        }
    }
}