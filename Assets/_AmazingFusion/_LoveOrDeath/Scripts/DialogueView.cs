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

        public System.Action OnClose;

        public void SetText(CharacterAction.ActionResult result) {
            switch (result) {
                case CharacterAction.ActionResult.None:
                    break;
                case CharacterAction.ActionResult.Win:
                    break;
                case CharacterAction.ActionResult.Lose:
                    break;
                case CharacterAction.ActionResult.Both:
                    break;
            }
        }

        public void Show() {
            _showAnimation.Play();
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