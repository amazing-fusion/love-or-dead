using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class MultiEasingAnimation : OptimizedBehaviour, IEffectable {

        [SerializeField]
        EasingInfo[] _easingInfo;

        [SerializeField]
        double _duration;

        double _starTime;
        double _currentTime;

        public event Action<MultiEasingAnimation> OnStart;
        public event Action<MultiEasingAnimation> OnUpdate;
        public event Action<IEffectable> OnEnd;


        public void Play() {
            _starTime = Time.time;
            Timing.RunCoroutine(DoEasing());
        }

        protected virtual IEnumerator<float> DoEasing() {
            if (OnStart != null) OnStart(this);

            double endTime = _starTime + _duration;
            while (Time.time < endTime) {
                _currentTime = Time.time - _starTime;

                EasingUpdate();

                if (OnUpdate != null) OnUpdate(this);
                yield return 0;
            }
            EasingUpdate();
            if (OnUpdate != null) OnUpdate(this);

            if (OnEnd != null) OnEnd(this);
        }

        public virtual void EasingUpdate() {
            for (int i = 0; i < _easingInfo.Length; ++i) {
                EasingUpdate(i);
            }
        }

        public virtual void EasingUpdate(int easingInfoIndex) {
            _easingInfo[easingInfoIndex].Update(_currentTime, _duration);
        }
    }
}