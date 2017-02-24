using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace com.AmazingFusion.LoveOrDeath {
    public class CombatView : OptimizedBehaviour {

        [SerializeField]
        TMP_Text _turnText;

        // Use this for initialization
        void Start() {
            _turnText.text = CombatController.Instance.Turn.ToString();

            DialogueView.Instance.OnClose += () => {
                _turnText.text = CombatController.Instance.Turn.ToString();
            };
        }
    }
}