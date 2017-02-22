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
        }

        bool CheckTurnEndCondition() {
            return _turn >= _maxTurns;
        }

        bool CheckPlayerLifeEndCondition() {
            return _playerCharacter.CurrentLife <= 0;
        }

        bool CheckRivalLifeEndCondition() {
            return _rivalCharacter.CurrentLife <= 0;
        }

        bool CheckKissVictoryCondition() {
            return _rivalCharacter.CurrentLife <= 0;
        }

        void StartCombat() {
            _playerCharacter.Initialize();
            _rivalCharacter.Initialize();
        }

        void EndCombat() {

        }

        public void PlayAction(CharacterAction action) {
            CharacterAction rivalAction = _rivalCharacter.GetAction();

            if (action.ClashAction(rivalAction)) {

            } else {

            }
        }
    }
}