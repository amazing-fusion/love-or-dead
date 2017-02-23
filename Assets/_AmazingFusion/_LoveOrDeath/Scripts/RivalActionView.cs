using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class RivalActionView : OptimizedBehaviour {

        EasingAnimation _showAnimation;
        EasingAnimation _hideWinAnimation;
        EasingAnimation _hideLoseAnimation;

        AICharacterController _rivalCharacter;

        void Awake() {
            _rivalCharacter.GetComponent<AICharacterController>();
            _hideWinAnimation.OnEnd += (IEffectable effect) => { gameObject.SetActive(true); };
            _hideLoseAnimation.OnEnd += (IEffectable effect) => { gameObject.SetActive(true); };
        }

        public void Show() {
            gameObject.SetActive(true);
            _showAnimation.Play();
        }

        public void Win() {
            _showAnimation.Play();
        }

        public void Lose() {
            _hideLoseAnimation.Play();
        }
    }
}
