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

		[SerializeField]
		TextValueEasingAnimation _turnAnimation;

        public event Action OnValuesChanged;

        void Start() {
            CombatController.Instance.PlayerCharacter.OnInitialized += Initialize;

            DialogueView.Instance.OnClose += SetPlayerValues;
        }

        public void Initialize() {
            SetPlayerValues();
        }

        void SetPlayerValues() {
			
            _lifeAnimation.SetStartValueAsCurrentValue();
            _lifeAnimation.SetEndValue(CombatController.Instance.PlayerCharacter.CurrentLife);

            _energyAnimation.SetStartValueAsCurrentValue();
            _energyAnimation.SetEndValue(CombatController.Instance.PlayerCharacter.CurrentEnergy);

			_turnAnimation.SetStartValueAsCurrentValue();
			_turnAnimation.SetEndValue(CombatController.Instance.Turn);

            _ultimateAnimation.SetStartValueAsCurrentValue();
            _ultimateAnimation.MaxValue = CombatController.Instance.PlayerCharacter.UltimateActivationSuccess;
            _ultimateAnimation.SetEndValue(CombatController.Instance.PlayerCharacter.UltimateCounter);

            _energyAnimation.OnEnd += ValuesChanged;

			_turnAnimation.Play ();
            _lifeAnimation.Play();
            _energyAnimation.Play();
            _ultimateAnimation.Play();



			if (CombatController.Instance.CheckPlayerLifeEndCondition() || CombatController.Instance.CheckRivalLifeEndCondition()) {
				CombatController.Instance.StartTurn (false);
			} 

			if (CombatController.Instance.CheckTurnEndCondition ()) {
				CombatController.Instance.StartTurn (false);
			}
        }

        void ValuesChanged(IEffectable effect) {
            _energyAnimation.OnEnd -= ValuesChanged;
            if (OnValuesChanged != null) OnValuesChanged();
        }
    }
}
