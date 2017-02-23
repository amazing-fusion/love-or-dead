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
        Vector3 startPosition;
        Transform startParent;

        MoveToEasingAnimation _moveAnimation;
        ScaleEasingAnimation _scaleAnimation;

        bool _canDrag;
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
            _moveAnimation = GetComponent<MoveToEasingAnimation>();
            _scaleAnimation = GetComponent<ScaleEasingAnimation>();

            CombatController.Instance.PlayerCharacter.OnActionPicked += (CharacterAction action) => { _canDrag = false; };
            /*
             Evento de acabar todos lo eventos del flujo
             */
            CombatController.Instance.OnTurnChange += () => { _canDrag = true; };
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_canDrag && CombatController.Instance.PlayerCharacter.CanPlayAction(Action))
            {
                _dragging = true;
                cardBeingDragged = this;
                startPosition = Transform.localPosition;
                startParent = Transform.parent;
                GetComponent<CanvasGroup>().blocksRaycasts = false;

                startParent.SetAsLastSibling();

                _scaleAnimation.SetStartValue(1);
                _scaleAnimation.SetChangeValue(0.5);
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

                if (transform.parent == startParent)
                {
                    _moveAnimation.SetStartPositionAsCurrentLocalPosition();
                    _moveAnimation.SetEndPosition(startPosition);
                    _moveAnimation.Play();

                    _scaleAnimation.SetStartScaleAsCurrentScale();
                    _scaleAnimation.SetEndValue(1);
                    _scaleAnimation.Play();
                }
            }
        }

        public void ActionResolved(bool win)
        {
            _moveAnimation.SetStartPositionAsCurrentLocalPosition();
            _moveAnimation.SetEndPosition(startPosition);
            _moveAnimation.Play();

            _scaleAnimation.SetStartScaleAsCurrentScale();
            _scaleAnimation.SetEndValue(1);
            _scaleAnimation.Play();
        }

        public void PlayAction()
        {
            Transform.parent = startParent;
            _action.OnActionResolved += ActionResolved;
            CombatController.Instance.PlayerCharacter.PlayAction(Action);
        }
    }
}

