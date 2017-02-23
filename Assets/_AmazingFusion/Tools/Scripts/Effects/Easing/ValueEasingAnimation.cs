using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class ValueEasingAnimation : OptimizedBehaviour, IEffectable {

        [SerializeField]
        protected EasingInfo _easingInfo;

        [SerializeField]
        protected double _duration;

        protected double _starTime;
        protected double _currentTime;

        public event Action<ValueEasingAnimation> OnStart;
        public event Action<ValueEasingAnimation> OnUpdate;
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
                EasingUpdate();
                if (OnUpdate != null) OnUpdate(this);

                yield return 0;
            }
            _currentTime = _duration;
            EasingUpdate();
            if (OnUpdate != null) OnUpdate(this);

            if (OnEnd != null) OnEnd(this);
        }

        protected virtual void EasingUpdate() {
            _easingInfo.Update(_currentTime, _duration);
        }
    }
}