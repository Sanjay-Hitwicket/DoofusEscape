using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Systems.Animation {
    public interface IAnimatorController {
        public virtual void ResetTrigger(Animator animator, string trigger) {
            animator.ResetTrigger(trigger);
        }

        public virtual void PlayTrigger(Animator animator, string trigger) {
            animator.ResetTrigger(trigger);
            animator.SetTrigger(trigger);
            animator.Play(trigger);
        }
    }
}