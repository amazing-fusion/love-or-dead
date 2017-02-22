using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath {
    public class LevelManager : Singleton<LevelManager> {

        int _currentLevel;

        public int CurrentLevel {
            get {
                return _currentLevel;
            }
        }

        public void SetFirstLevel() {
            _currentLevel = 1;
        }

        public void NextLevel() {
            ++_currentLevel;
        }
    }
}