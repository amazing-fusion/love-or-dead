using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath
{
    public class FinalView : OptimizedBehaviour
    {
        [SerializeField]
        RectTransform _victoryScreen;

        [SerializeField]
        RectTransform _defeatedScreen;

        void Start() {
            CombatController.Instance.OnCombatEnd += (bool win) => {
                if (win) {
                    _victoryScreen.gameObject.SetActive(true);
                } else {
                    _defeatedScreen.gameObject.SetActive(true);
                }
            };
        }

        public void GoToMenu()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MenuScene);
        }

        public void CloseVictoryCombat() {
            LevelManager.Instance.NextLevel();
            _victoryScreen.gameObject.SetActive(false);
            CombatController.Instance.StartCombat();

        }

        public void CloseDefeatedCombat() {
            LevelManager.Instance.SetFirstLevel();
            _defeatedScreen.gameObject.SetActive(false);
            CombatController.Instance.StartCombat();

        }
    }
}

