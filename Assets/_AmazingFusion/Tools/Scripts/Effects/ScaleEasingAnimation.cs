using System;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

namespace com.AmazingFusion {
    public class ScaleEasingAnimation : OptimizedBehaviour, IEffectable {

        [SerializeField]
        EasingInfo _xEasingInfo;

        [SerializeField]
        EasingInfo _yEasingInfo;

        [SerializeField]
        EasingInfo _zEasingInfo;

        [SerializeField]
        double _duration;

        double _starTime;
        double _currentTime;

        public event Action<ScaleEasingAnimation> OnStart;
        public event Action<ScaleEasingAnimation> OnUpdate;
        public event Action<IEffectable> OnEnd;

        public void SetStartPosition(Vector3 startPosition) {
            _xEasingInfo.StartValue = startPosition.x;
            _yEasingInfo.StartValue = startPosition.y;
            _zEasingInfo.StartValue = startPosition.z;
        }

        public void SetStartPositionAsCurrentLocalPosition() {
            SetStartPosition(transform.localPosition);
        }

        public void SetChangePosition(Vector3 changePosition) {
            _xEasingInfo.ChangeValue = changePosition.x;
            _yEasingInfo.ChangeValue = changePosition.y;
            _zEasingInfo.ChangeValue = changePosition.z;
        }

        public void SetEndPosition(Vector3 endPosition) {
            _xEasingInfo.ChangeValue = endPosition.x - (float)_xEasingInfo.StartValue;
            _yEasingInfo.ChangeValue = endPosition.y - (float)_yEasingInfo.StartValue;
            _zEasingInfo.ChangeValue = endPosition.z - (float)_zEasingInfo.StartValue;
        }

        public void SetEndPositionAsCurrentLocalPosition() {
            SetEndPosition(transform.localPosition);
        }

        public void Play() {
            _starTime = Time.time;
            Timing.RunCoroutine(DoEasing());
        }

        protected virtual IEnumerator<float> DoEasing() {
            if (OnStart != null) OnStart(this);

            double endTime = _starTime + _duration;
            while (Time.time < endTime) {
                _currentTime = Time.time - _starTime;
                if (_xEasingInfo.ChangeValue != 0) {
                    _xEasingInfo.Update(_currentTime, _duration);
                }
                if (_yEasingInfo.ChangeValue != 0) {
                    _yEasingInfo.Update(_currentTime, _duration);
                }
                if (_zEasingInfo.ChangeValue != 0) {
                    _zEasingInfo.Update(_currentTime, _duration);
                }
                Transform.localScale = new Vector3(
                        (float)_xEasingInfo.CurrentValue,
                        (float)_yEasingInfo.CurrentValue,
                        (float)_zEasingInfo.CurrentValue);

                if (OnUpdate != null) OnUpdate(this);
                yield return 0;
            }
            if (_xEasingInfo.ChangeValue != 0) {
                _xEasingInfo.Update(_duration, _duration);
            }
            if (_yEasingInfo.ChangeValue != 0) {
                _yEasingInfo.Update(_duration, _duration);
            }
            if (_zEasingInfo.ChangeValue != 0) {
                _zEasingInfo.Update(_duration, _duration);
            }
            Transform.localScale = new Vector3(
                        (float)_xEasingInfo.CurrentValue,
                        (float)_yEasingInfo.CurrentValue,
                        (float)_zEasingInfo.CurrentValue);

            if (OnUpdate != null) OnUpdate(this);

            if (OnEnd != null) OnEnd(this);
        }
    }
}