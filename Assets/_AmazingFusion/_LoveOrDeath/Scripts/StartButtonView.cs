using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath
{
    public class StartButtonView : OptimizedBehaviour
    {
        void Start()
        {
            AudioController.Instance.PlayMenuMusic();
        }

        public void StartGamePlay()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.GameScene);
        }
    }
}

