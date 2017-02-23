using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class DialogueView : Singleton<DialogueView> {

        MoveToEasingAnimation _openAnimation;

        public System.Action OnClose;

        protected override void Awake() {
            base.Awake();
            _openAnimation = GetComponent<MoveToEasingAnimation>();
        }

        public void Show() {

        }

        public void Close() {
            _openAnimation.OnEnd += Closed;
        }

        void Closed(IEffectable effect) {
            _openAnimation.OnEnd -= Closed;
            if (OnClose != null) OnClose();
        }
    }
}