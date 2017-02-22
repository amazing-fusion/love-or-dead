using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath
{
    public class CharacterAction : OptimizedBehaviour
    {
        [SerializeField]
        string _key;

        [SerializeField]
        int _requiredLevel;

        [SerializeField]
        int _energyCost;

        [SerializeField]
        int _energyEarned;

        [SerializeField]
        int _damage;

        public string Key
        {
            get
            {
                return _key;
            }
        }

        public int RequiredLevel
        {
            get
            {
                return _requiredLevel;
            }
        }

        public int EnergyCost
        {
            get
            {
                return _energyCost;
            }
        }

        public int EnergyEarned
        {
            get
            {
                return _energyEarned;
            }
        }

        public int Damage
        {
            get
            {
                return _damage;
            }
        }

        public bool ClashAction(CharacterAction action)
        {
            return false;
        }
    }
}

