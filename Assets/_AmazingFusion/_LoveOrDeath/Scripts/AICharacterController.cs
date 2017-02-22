using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class AICharacterController : CharacterController {

        [SerializeField]
        int _patternLength;

        [SerializeField]
        int _lovingLife;

        CharacterAction[] _pattern;

        List<CharacterAction> _actionsList = new List<CharacterAction>();

        CharacterAnimator _characterAnimator;

        void Awake() {
            _characterAnimator = GetComponent<CharacterAnimator>();
        }

        void SetActionsList() {

        }

        public override void Initialize() {
            base.Initialize();
            SetActionsList();
        }

        public void GetAction() {

        }
    }
}
