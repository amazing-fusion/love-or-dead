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
            CombatController.Instance.RivalCharacter.OnActionResolved += HideResult;
            EffectsManager.Instance.AddEffect(_showAnimation);
        }

        public void HideResult(bool win) {
            CombatController.Instance.RivalCharacter.OnActionResolved -= HideResult;

            if (win) {
                EffectsManager.Instance.AddEffect(_hideWinAnimation);
            } else {
                EffectsManager.Instance.AddEffect(_hideLoseAnimation);
            }
        }
    }
}
