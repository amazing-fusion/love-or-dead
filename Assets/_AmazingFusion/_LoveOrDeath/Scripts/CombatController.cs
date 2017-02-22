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
            return _rivalCharacter.CurrentLife <= _rivalCharacter.LovingLife;
        }

        void StartCombat() {
            _playerCharacter.Initialize();
            _rivalCharacter.Initialize();
        }

        void EndCombat() {

        }

        public void PlayAction(CharacterAction playerAction) {

            CharacterAction rivalAction = _rivalCharacter.GetAction();
            
            if(playerAction.Type == CharacterAction.ActionType.Ultimate)
            {
                //EVENTO playerAction.OnUltimate
                _playerCharacter.ultimateEvent();
                _rivalCharacter.CurrentLife -= playerAction.Damage; 
            }
            else if (rivalAction.Type == CharacterAction.ActionType.Ultimate)
            {
                //EVENTO rivalAction.OnUltimate
                _rivalCharacter.ultimateEvent();
                _playerCharacter.CurrentLife -= rivalAction.Damage;
            }
            else
            {
                _playerCharacter.CurrentEnergy += playerAction.EnergyEarned;
                _rivalCharacter.CurrentEnergy += rivalAction.EnergyEarned;

                if(playerAction.Type == CharacterAction.ActionType.Offensive)
                {
                    if(rivalAction.Type == CharacterAction.ActionType.Offensive)
                    {
                        //EVENTO playerAction.OnAttack
                        _playerCharacter.attackEvent();
                        _rivalCharacter.CurrentLife -= playerAction.Damage;
                        //EVENTO rivalAction.OnAttack
                        _rivalCharacter.attackEvent();
                        _playerCharacter.CurrentLife -= rivalAction.Damage;
                    }
                    else
                    {
                        //EVENTO playerAction.OnAttack
                        //EVENTO rivalAction.OnDefense
                        _playerCharacter.attackEvent();
                        _rivalCharacter.defenseEvent();
                        _playerCharacter.CurrentLife -= rivalAction.Damage;
                    }
                }
                else if(playerAction.Type == CharacterAction.ActionType.Defensive)
                {
                    if (rivalAction.Type == CharacterAction.ActionType.Offensive)
                    {
                        //EVENTO rivalAction.OnAttack
                        //EVENTO playerAction.OnDefense
                        _rivalCharacter.attackEvent();
                        _playerCharacter.defenseEvent();
                        _rivalCharacter.CurrentLife -= playerAction.Damage;

                    }
                    else
                    {
                        //EVENTO playerAction.OnDefense
                        //EVENTO rivalAction.OnDefense
                        _playerCharacter.defenseEvent();
                        _rivalCharacter.defenseEvent();
                    }
                }
            }
            
        }

        public void Kiss()
        {
            if(CheckKissVictoryCondition())
            {
                //TODO: Win playerAction
            }
            else
            {
                //TODO: Win rivalAction
            }
        }
    }
}