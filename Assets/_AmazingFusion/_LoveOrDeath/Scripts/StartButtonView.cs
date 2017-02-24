using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath
{
    public class StartButtonView : OptimizedBehaviour
    {
        public void StartGamePlay()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.GameScene);
        }
    }
}

