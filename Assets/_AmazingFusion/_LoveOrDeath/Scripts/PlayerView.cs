using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion.LoveOrDeath {
    public class PlayerView : Singleton<PlayerView> {

        [SerializeField]
        TMP_Text _lifeText;

        [SerializeField]
        TMP_Text _energyText;

        [SerializeField]
        Image _ultimateImage;

        [SerializeField]
        EasingAnimation _setValuesAnimation;

        [SerializeField]
        FillAmountImageEasingAnimation _ultimateAnimation;

        public event Action OnValuesChanged;

        void Start() {
            DialogueView.Instance.OnClose += SetPlayerValues;
        }

        public void Initialize() {
            //_lifeText.text = CombatController.Instance.
        }

        void SetPlayerValues() {
            //_ultimateAnimation.MaxValue = CombatController.Instance.
            _setValuesAnimation.OnEnd += ValuesChanged;
            _setValuesAnimation.Play();
        }

        void ValuesChanged(IEffectable effect) {
            _setValuesAnimation.OnEnd -= ValuesChanged;
            if (OnValuesChanged != null) OnValuesChanged();
        }
    }
}
