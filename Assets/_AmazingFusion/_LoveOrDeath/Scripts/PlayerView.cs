using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion.LoveOrDeath {
    public class PlayerView : Singleton<PlayerView> {

        [SerializeField]
        TextValueEasingAnimation _lifeAnimation;

        [SerializeField]
        TextValueEasingAnimation _energyAnimation;

        [SerializeField]
        FillAmountImageEasingAnimation _ultimateAnimation;

        public event Action OnValuesChanged;

        void Start() {
            DialogueView.Instance.OnClose += SetPlayerValues;
        }

        public void Initialize() {
            SetPlayerValues();
        }

        void SetPlayerValues() {
            _lifeAnimation.SetEndValue(CombatController.Instance.PlayerCharacter.CurrentLife);
            _energyAnimation.SetEndValue(CombatController.Instance.PlayerCharacter.CurrentEnergy);
            
            _ultimateAnimation.MaxValue = CombatController.Instance.PlayerCharacter.UltimateActivationSuccess;
            _energyAnimation.SetEndValue(CombatController.Instance.PlayerCharacter.UltimateCounter);

            _ultimateAnimation.OnEnd += ValuesChanged;

            _lifeAnimation.Play();
            _energyAnimation.Play();
            _ultimateAnimation.Play();
        }

        void ValuesChanged(IEffectable effect) {
            _ultimateAnimation.OnEnd -= ValuesChanged;
            if (OnValuesChanged != null) OnValuesChanged();
        }
    }
}
