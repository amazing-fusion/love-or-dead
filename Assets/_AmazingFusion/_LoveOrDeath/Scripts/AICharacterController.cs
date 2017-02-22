using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class AICharacterController : CharacterController {

        [SerializeField]
        int _initialPatternLength;

        int _patternLength;

        [SerializeField]
        int _lovingLife;

        int _currentPatternIndex;

        List<CharacterAction> _pattern = new List<CharacterAction>();

        List<CharacterAction> _actionsList = new List<CharacterAction>();

        CharacterAnimator _characterAnimator;

        public int LovingLife
        {
            get
            {
                return _lovingLife;
            }
        }

        void Awake() {

            _characterAnimator = GetComponent<CharacterAnimator>();
        }

        void SetActionsList() {

            _actionsList.Clear();
            
            foreach(CharacterAction action in CharacterAction.ActionList)
            {
                if(LevelManager.Instance.CurrentLevel >= action.RequiredLevel)
                {
                    _actionsList.Add(action);
                }
            }

        }

        public override void Initialize() {
            base.Initialize();
            SetActionsList();
            _pattern.Clear();
            _currentPatternIndex = 0;
            _patternLength = _initialPatternLength + LevelManager.Instance.CurrentLevel;
        }

        public CharacterAction GetAction() {

            CharacterAction action = null;

            List<CharacterAction> temActionList = new List<CharacterAction>(_actionsList);

            if(_patternLength < _currentPatternIndex && _currentPatternIndex >= _pattern.Count)
            {
               while(temActionList.Count > 0)
                {
                    int random = Random.Range(0, _actionsList.Count);
                    if(CurrentEnergy < temActionList[random].EnergyCost)
                    {
                        temActionList.RemoveAt(random);
                    }
                    else
                    {
                        action = temActionList[random];
                        _pattern[_currentPatternIndex] = action;
                        break;
                    }
                }
            }
            else
            {
                action = _pattern[_currentPatternIndex];
            }

            _currentPatternIndex++;
            if (_currentPatternIndex >= _patternLength)
            {
                _currentPatternIndex = 0;
            }
#if UNITY_EDITOR
            if (action == null)
            {
                Debug.LogError("[AICharacterController] No existe ninguna accion con coste de energia menor a " + CurrentEnergy.ToString());
            }
#endif
            return action;
        }
    }
}
