using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class CombatController : Singleton<CombatController> {

        [SerializeField]
        int _maxTurns;

        [SerializeField]
        PlayerCharacterController _playerCharacter;

        [SerializeField]
        AICharacterController _rivalCharacter;

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

public event System.Action<CharacterAction.ActionResult> OnCombatActionsResolved;public PlayerCharacterController PlayerCharacter
        {
            get
            {
                return _playerCharacter;
            }
        }

        void Start()
        {
            StartCombat();
        }
        public event System.Action OnCombatStart;
        public event System.Action OnCombatEnd;

        public event System.Action OnTurnChange;

        bool CheckTurnEndCondition() {
            return _turn <= 0;
        }

        bool CheckPlayerLifeEndCondition() {
            return _playerCharacter.CurrentLife <= 0;
        }

        bool CheckRivalLifeEndCondition() {
            return _rivalCharacter.CurrentLife <= 0;
        }

        bool CheckKissVictoryCondition() {
            return _rivalCharacter.CurrentLife <= _rivalCharacter.LovingLife;
        }


        void StartCombat() {
            Turn = _maxTurns;
            _playerCharacter.Initialize();
            _rivalCharacter.Initialize();

            if (OnCombatStart != null) OnCombatStart();
        }

        void EndCombat(bool win) {
            if (OnCombatEnd != null) OnCombatEnd();
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
                    playerAction.ActionResolved(false);
                    rivalAction.ActionResolved(false);

                    break;
                case CharacterAction.ActionResult.Win:
                    playerAction.ActionResolved(true);
                    rivalAction.ActionResolved(false);

                    _rivalCharacter.CurrentLife -= playerAction.Damage;

                    break;
                case CharacterAction.ActionResult.Lose:
                    playerAction.ActionResolved(false);
                    rivalAction.ActionResolved(true);

                    _playerCharacter.CurrentLife -= rivalAction.Damage;

                    break;
                case CharacterAction.ActionResult.Both:
                    playerAction.ActionResolved(true);
                    rivalAction.ActionResolved(true);

                    _rivalCharacter.CurrentLife -= playerAction.Damage;
                    _playerCharacter.CurrentLife -= rivalAction.Damage;

                    break;
            }

            _playerCharacter.CurrentEnergy += playerAction.EnergyEarned;
            _rivalCharacter.CurrentEnergy += rivalAction.EnergyEarned;
            
            Debug.Log("Final Player Life " + _playerCharacter.CurrentLife);
            Debug.Log("Final Player Energy " + _playerCharacter.CurrentEnergy);
            Debug.Log("Final Rival Life " + _rivalCharacter.CurrentLife);
            Debug.Log("Final Rival Energy " + _rivalCharacter.CurrentEnergy);

            if (OnCombatActionsResolved != null) OnCombatActionsResolved(actionResult);

            if (CheckPlayerLifeEndCondition() || CheckRivalLifeEndCondition()) {
                EndCombat(false);
            } else {
                ++Turn;
                if (CheckTurnEndCondition()) {
                    EndCombat(false);
                }
            }
        }

        public void Kiss()
        {
            if(CheckKissVictoryCondition())
            {
                EndCombat(true);
            }
            else
            {
                EndCombat(false);
            }
        }
    }
}