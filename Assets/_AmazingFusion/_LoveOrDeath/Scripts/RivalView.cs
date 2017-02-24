using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class RivalView : MonoBehaviour {

        [SerializeField]
        TextValueEasingAnimation _lifeAnimation;

        void Start() {
            CombatController.Instance.RivalCharacter.OnInitialized += Initialize;

            DialogueView.Instance.OnClose += SetPlayerValues;
        }

        public void Initialize() {
            SetPlayerValues();
        }

        void SetPlayerValues() {
            _lifeAnimation.SetStartValueAsCurrentValue();
            _lifeAnimation.SetEndValue(CombatController.Instance.PlayerCharacter.CurrentLife);

            _lifeAnimation.Play();
        }
    }
}