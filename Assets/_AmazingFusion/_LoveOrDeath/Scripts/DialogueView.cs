using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace com.AmazingFusion.LoveOrDeath {
    public class DialogueView : Singleton<DialogueView> {

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _closeAnimation;

        [SerializeField]
        TMP_Text _text;

        [SerializeField]
        CharacterAnimator _animatorController;

        public event System.Action OnClose;

        void Start() {
            CombatController.Instance.OnCombatActionsResolved += (CharacterAction.ActionResult result) => {
                SetText(result);
                EffectsManager.Instance.AddEffect(_showAnimation);
            };
        }

        public void SetText(CharacterAction.ActionResult result) {
            switch (result) {
                case CharacterAction.ActionResult.None:
                    //SmartLocalization.LanguageManager.Instance.
                    _text.text = "Nadie gana";
                    _animatorController.PlayNoneDamageAnimation();
                    break;
                case CharacterAction.ActionResult.Win:
                    _text.text = "¡Has ganado!";
                    _animatorController.PlayRivalLovingAnimation();
                    break;
                case CharacterAction.ActionResult.Lose:
                    _text.text = "Has perdido";
                    _animatorController.PlayRivalHitAnimation();
                    break;
                case CharacterAction.ActionResult.Both:
                    _text.text = "Los dos ganais";
                    _animatorController.PlayBothAttackAnimation();
                    break;
            }
        }

        public void Close() {
            _closeAnimation.OnEnd += Closed;
            _closeAnimation.Play();

            _animatorController.PlayIdleAnimation();
        }

        void Closed(IEffectable effect) {
            _closeAnimation.OnEnd -= Closed;
            if (OnClose != null) OnClose();
        }
    }
}