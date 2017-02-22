using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class CharacterController : OptimizedBehaviour {

        [SerializeField]
        int _maxLife;

        [SerializeField]
        int _maxEnergy;

        [SerializeField]
        int _initialEnergy;

        int _currentLife;

        int _currentEnergy;

        public int MaxLife {
            get {
                return _maxLife;
            }
        }

        public int MaxEnergy {
            get {
                return _maxEnergy;
            }
        }

        public int CurrentLife {
            get {
                return _currentLife;
            }

            set {
                if (_currentLife != value) {
                    _currentLife = value;
                    if (OnLifeChange != null) OnLifeChange();
                }
            }
        }

        public int CurrentEnergy {
            get {
                return _currentEnergy;
            }

            set {
                if (_currentEnergy != value) {
                    _currentEnergy = value;
                    if (OnEnergyChange != null) OnEnergyChange();
                }
            }
        }

        public event System.Action OnLifeChange;
        public event System.Action OnEnergyChange;

        public virtual void Initialize() {
            CurrentLife = _maxLife;
            CurrentEnergy = _initialEnergy;
        }

        public bool CanPlayAction(CharacterAction action) {

            if(_currentEnergy >= action.EnergyCost)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}