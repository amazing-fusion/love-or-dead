using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath
{
    public class PlayerCharacterController : CharacterController
    {
        [SerializeField]
        CharacterAction _talkAction;

        [SerializeField]
        CharacterAction _attackAction;

        [SerializeField]
        CharacterAction _defenseAction;

        [SerializeField]
        CharacterAction _ultimateAction;

        int _ultimateCounter;

        [SerializeField]
        int _ultimateActivationSuccess;

        public int UltimateCounter
        {
            get
            {
                return _ultimateCounter;
            }

            set
            {
                if(_ultimateCounter != value)
                {
                    _ultimateCounter = value;
                    if (OnUltimateChange != null) OnUltimateChange();
                }
            }
        }

        public event System.Action OnUltimateChange;

        public bool PlayAction(CharacterAction action)
        {
            if (CanPlayAction(action))
            {
                CurrentEnergy = CurrentEnergy - action.EnergyCost;

                CombatController.Instance.PlayAction(action);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanPlayUltimate()
        {
            if (_ultimateCounter >= _ultimateActivationSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Kiss()
        {
            CombatController.Instance.Kiss();
        }
    }
}

