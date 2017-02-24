using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class CharacterAnimator : OptimizedBehaviour {

        Animator _animator;

        public void PlayIdleAnimation()
        {
            _animator.SetInteger("_state", 0);
        }

        public void PlayRivalHitAnimation()
        {
            _animator.SetInteger("_state", 1);
        }

        public void PlayRivalLovingAnimation()
        {
            _animator.SetInteger("_state", 2);
        }

        public void PlayBothAttackAnimation()
        {
            _animator.SetInteger("_state", 3);
        }

        public void PlayNoneDamageAnimation()
        {
            _animator.SetInteger("_state", 4);
        }

        public void PlayKissAnimation()
        {
            _animator.SetInteger("_state", 5);
        }

        public void PlayKissWinAnimation()
        {
            _animator.SetInteger("_state", 6);
        }

        public void PlayKissLoseAnimation()
        {
            _animator.SetInteger("_state", 7);
        }
    }
}