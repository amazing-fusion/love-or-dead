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
                    _text.text = "Nadie gana";
                    break;
                case CharacterAction.ActionResult.Win:
                    _text.text = "¡Has ganado!";
                    break;
                case CharacterAction.ActionResult.Lose:
                    _text.text = "Has perdido";
                    break;
                case CharacterAction.ActionResult.Both:
                    _text.text = "Los dos ganais";
                    break;
            }
        }

        public void Close() {
            _closeAnimation.OnEnd += Closed;
            _closeAnimation.Play();
        }

        void Closed(IEffectable effect) {
            _closeAnimation.OnEnd -= Closed;
            if (OnClose != null) OnClose();
        }
    }
}