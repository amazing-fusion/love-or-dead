using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public abstract class EasingAnimation : OptimizedBehaviour, IEffectable {

        [SerializeField]
        EasingInfo _easingInfo;

        [SerializeField]
        double _duration;

        double _starTime;
        double _currentTime;

        public event Action<EasingAnimation> OnStart;
        public event Action<EasingAnimation> OnUpdate;
        public event Action<IEffectable> OnEnd;


        public void Play() {
            _starTime = Time.time;
            Timing.RunCoroutine(DoEasing());
        }

        public virtual IEnumerator<float> DoEasing() {
            if (OnStart != null) OnStart(this);

            double endTime = _starTime + _duration;
            while (Time.time < endTime) {
                _currentTime = Time.time - _starTime;
                _easingInfo.Update(_currentTime, _duration);

                if (OnUpdate != null) OnUpdate(this);
                yield return 0;
            }
            _easingInfo.Update(_duration, _duration);
            if (OnUpdate != null) OnUpdate(this);

            if (OnEnd != null) OnEnd(this);
        }
    }
}