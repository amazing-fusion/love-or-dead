using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.AmazingFusion.LoveOrDeath
{
    public class PlayerActionView : OptimizedBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        CharacterAction _action;

        public static PlayerActionView cardBeingDragged;
        Vector3 _startPosition;
        Transform _startParent;

        [SerializeField]
        MoveToEasingAnimation _moveAnimation;

        [SerializeField]
        ScaleEasingAnimation _scaleAnimation;

        [SerializeField]
        EasingAnimation _hideWinAnimation;

        [SerializeField]
        EasingAnimation _hideLoseAnimation;

        bool _canDrag = true;
        bool _dragging;

        public CharacterAction Action
        {
            get
            {
                return _action;
            }
        }

        public bool Dragging
        {
            get
            {
                return _dragging;
            }
        }

        void Awake()
        {
            _startParent = Transform.parent;
            _startPosition = Vector3.zero;

            CombatController.Instance.PlayerCharacter.OnActionPicked += (CharacterAction action) => { _canDrag = false; };
            /*
             Evento de acabar todos lo eventos del flujo
             */
            PlayerView.Instance.OnValuesChanged += () => {
                _canDrag = true;
                Transform.parent = _startParent;
                ((RectTransform)Transform).sizeDelta = ((RectTransform)Transform.parent).sizeDelta;
                Transform.localScale = Vector3.one;
                Transform.localPosition = _startPosition;
            };
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_canDrag && CombatController.Instance.PlayerCharacter.CanPlayAction(Action))
            {
                _dragging = true;
                cardBeingDragged = this;

                //_startPosition = Vector3.zero;
                //_startParent = Transform.parent;

                GetComponent<CanvasGroup>().blocksRaycasts = false;

                _startParent.SetAsLastSibling();

                _scaleAnimation.SetStartValue(1f);
                _scaleAnimation.SetEndValue(400f / 270f);
                _scaleAnimation.Play();
            }
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Dragging && CombatController.Instance.PlayerCharacter.CanPlayAction(Action))
            {
                Transform.position = Input.mousePosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Dragging /*&& CombatController.Instance.PlayerCharacter.CanPlayAction(Action)*/)
            {
                cardBeingDragged = null;
                _dragging = false;
                GetComponent<CanvasGroup>().blocksRaycasts = true;

                if (Transform.parent == _startParent)
                {
                    _moveAnimation.SetStartPositionAsCurrentLocalPosition();
                    _moveAnimation.SetEndPosition(_startPosition);
                    _moveAnimation.Play();

                    _scaleAnimation.SetStartScaleAsCurrentScale();
                    _scaleAnimation.SetEndValue(1);
                    _scaleAnimation.Play();
                }
            }
        }

        public void ActionResolved(bool win)
        {
            //_moveAnimation.SetStartPositionAsCurrentLocalPosition();
            //_moveAnimation.SetEndPosition(startPosition);
            //_moveAnimation.Play();

            //_scaleAnimation.SetStartScaleAsCurrentScale();
            //_scaleAnimation.SetEndValue(1);
            //_scaleAnimation.Play();

            CombatController.Instance.PlayerCharacter.OnActionResolved -= ActionResolved;

            if (win) {
                EffectsManager.Instance.AddEffect(_hideWinAnimation);
            } else {
                EffectsManager.Instance.AddEffect(_hideLoseAnimation);
            }
        }

        public void PlayAction()
        {
            CombatController.Instance.PlayerCharacter.OnActionResolved += ActionResolved;
            CombatController.Instance.PlayerCharacter.PlayAction(Action);
        }
    }
}

