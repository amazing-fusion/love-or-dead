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

        public void PlayAction(CharacterAction action)
        {
            if (CanPlayAction(action))
            {
                if (action.EnergyEarned > 0)
                {
                    CurrentEnergy = CurrentEnergy + action.EnergyEarned;
                }
                else
                {
                    CurrentEnergy = CurrentEnergy - action.EnergyCost;
                }
            }
            else
            {

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

        }
    }
}

