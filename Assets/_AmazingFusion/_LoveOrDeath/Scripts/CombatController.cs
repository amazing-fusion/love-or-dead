﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion.LoveOrDeath {
    public class CombatController : Singleton<CombatController> {

        [SerializeField]
        int _maxTurns;

        [SerializeField]
        PlayerCharacterController _playerCharacter;

        [SerializeField]
        AICharacterController _rivalCharacter;

        [SerializeField]
        CharacterAnimator _animatorController;

        int _turn;

        public int Turn {
            get {
                return _turn;
            }
            private set {
                if(_turn != value)
                {
                    _turn = value;
                    if (OnTurnChange != null) OnTurnChange();
                }
            }
        }

        public PlayerCharacterController PlayerCharacter
        {
            get
            {
                return _playerCharacter;
            }
        }

        public AICharacterController RivalCharacter {
            get {
                return _rivalCharacter;
            }
        }

        public event System.Action<CharacterAction, CharacterAction.ActionResult> OnCombatActionsResolved;

        void Start()
        {
            StartCombat();

            AudioController.Instance.PlayCombatMusic();
            AudioController.Instance.PlayMenuMusic();
        }
        public event System.Action OnCombatStart;
        public event System.Action<bool> OnCombatEnd;
		public event System.Action <bool> OnStartTurn;

        public event System.Action OnTurnChange;

        public bool CheckTurnEndCondition() {
            return _turn <= 0;
        }

        public bool CheckPlayerLifeEndCondition() {
            return _playerCharacter.CurrentLife <= 0;
        }

        public bool CheckRivalLifeEndCondition() {
            return _rivalCharacter.CurrentLife <= 0;
        }

        bool CheckKissVictoryCondition() {
            return _rivalCharacter.CurrentLife <= _rivalCharacter.LovingLife;
        }


        public void StartCombat() {
			
			_turn = _maxTurns;
			_playerCharacter.UltimateCounter = 0;
            _playerCharacter.Initialize();
            _rivalCharacter.Initialize();

            _animatorController.PlayIdleAnimation();
            AudioController.Instance.PlayCombatMusic();
            AudioController.Instance.PlayMenuMusic();

            if (OnCombatStart != null) OnCombatStart();
        }

        void EndCombat(bool win) {
            if (OnCombatEnd != null) OnCombatEnd(win);
        }

		public void StartTurn(bool win){
			if(OnStartTurn != null)OnStartTurn(win);
		}

        public void PlayAction(CharacterAction playerAction) {

            CharacterAction rivalAction = _rivalCharacter.GetAction();

            CharacterAction.ActionResult actionResult = playerAction.ClashAction(rivalAction);

            Debug.Log("Player Action " + playerAction.Type);
            Debug.Log("Player Life " + _playerCharacter.CurrentLife);
            Debug.Log("Player Energy " + _playerCharacter.CurrentEnergy);
            Debug.Log("Rival Action " + rivalAction.Type);
            Debug.Log("Rival Life " + _rivalCharacter.CurrentLife);
            Debug.Log("Rival Energy " + _rivalCharacter.CurrentEnergy);

            switch (actionResult) {
                case CharacterAction.ActionResult.None:
                    _playerCharacter.ActionResolved(false);
                    _rivalCharacter.ActionResolved(false);

                    break;
                case CharacterAction.ActionResult.Win:
                    _playerCharacter.ActionResolved(true);
                    _rivalCharacter.ActionResolved(false);

                    _rivalCharacter.CurrentLife -= playerAction.Damage;

                    break;
                case CharacterAction.ActionResult.Lose:
                    _playerCharacter.ActionResolved(false);
                    _rivalCharacter.ActionResolved(true);

                    _playerCharacter.CurrentLife -= rivalAction.Damage;

                    break;
                case CharacterAction.ActionResult.Both:
                    _playerCharacter.ActionResolved(true);
                    _rivalCharacter.ActionResolved(true);

                    _rivalCharacter.CurrentLife -= playerAction.Damage;
                    _playerCharacter.CurrentLife -= rivalAction.Damage;

                    break;
            }

            if (playerAction.Type == CharacterAction.ActionType.Ultimate) {
                _playerCharacter.UltimateCounter = 0;

            } else if (playerAction.Type == CharacterAction.ActionType.Offensive && rivalAction.Type == CharacterAction.ActionType.Energetic ||
                    playerAction.Type == CharacterAction.ActionType.Defensive && rivalAction.Type == CharacterAction.ActionType.Offensive) {

                ++_playerCharacter.UltimateCounter;
            }

            _playerCharacter.CurrentEnergy += playerAction.EnergyEarned;
            _rivalCharacter.CurrentEnergy += rivalAction.EnergyEarned;
            
            Debug.Log("Final Player Life " + _playerCharacter.CurrentLife);
            Debug.Log("Final Player Energy " + _playerCharacter.CurrentEnergy);
            Debug.Log("Final Rival Life " + _rivalCharacter.CurrentLife);
            Debug.Log("Final Rival Energy " + _rivalCharacter.CurrentEnergy);

            if (OnCombatActionsResolved != null) OnCombatActionsResolved(playerAction, actionResult);

            if (CheckPlayerLifeEndCondition() || CheckRivalLifeEndCondition()) {
            
			} 
			else 
			{
                --Turn;
            }
        }
        
        IEnumerator <float> DoEndCombat(bool combatResult)
        {
            _animatorController.PlayKissAnimation();
            AudioController.Instance.PlayKissUISound();

            yield return Timing.WaitForSeconds(2);

            if (CheckKissVictoryCondition())
            {
                Debug.Log("Life enemy " + _rivalCharacter.CurrentLife + " Loving " + _rivalCharacter.LovingLife);
                Debug.Log(CheckKissVictoryCondition());
                _animatorController.PlayKissWinAnimation();
                AudioController.Instance.PlayRivalKissWinSound();
            }
            else
            {
                Debug.Log("Life enemy " + _rivalCharacter.CurrentLife + " Loving " + _rivalCharacter.LovingLife);
                Debug.Log(CheckKissVictoryCondition());
                _animatorController.PlayKissLoseAnimation();
                AudioController.Instance.PlayRivalKissLoseSound();
            }
            yield return Timing.WaitForSeconds(2);
            EndCombat(combatResult);
        }

        public void Kiss()
        {
            if(CheckKissVictoryCondition())
            {
                Timing.RunCoroutine(DoEndCombat(true));
            }
            else
            {
                Timing.RunCoroutine(DoEndCombat(false));
            }
        }
    }
}