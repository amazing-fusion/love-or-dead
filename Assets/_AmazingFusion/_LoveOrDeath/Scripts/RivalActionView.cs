using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class RivalActionView : OptimizedBehaviour {

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideWinAnimation;

        [SerializeField]
        EasingAnimation _hideLoseAnimation;

        void Awake() {
            CombatController.Instance.RivalCharacter.OnActionPicked += Show;
        }

        public void Show(CharacterAction action) {
            gameObject.SetActive(true);
            EffectsManager.Instance.AddEffect(_showAnimation);

            //action.OnActionResolved += HideResult;
        }

        public void HideResult(bool win) {
            //_action.OnActionResolved -= HideResult;

            if (win) {
                EffectsManager.Instance.AddEffect(_hideWinAnimation);
            } else {
                EffectsManager.Instance.AddEffect(_hideLoseAnimation);
            }
        }
    }
}
