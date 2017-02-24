using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.AmazingFusion.LoveOrDeath {
    [RequireComponent(typeof(Image))]
    public class RivalActionView : OptimizedBehaviour {

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideWinAnimation;

        [SerializeField]
        EasingAnimation _hideLoseAnimation;

        Image _image;

        void Awake() {
            _image = GetComponent<Image>();
            CombatController.Instance.RivalCharacter.OnActionPicked += Show;
        }

        public void Show(CharacterAction action) {
            _image.sprite = action.Sprite;
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
