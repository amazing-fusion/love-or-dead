using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.AmazingFusion.LoveOrDeath
{
    public class ActionView : OptimizedBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        CharacterAction _action;

        public static GameObject cardBeingDragged;
        Vector3 startPosition;
        Transform startParent;

        MoveToEasingAnimation _moveAnimation;

        public MoveToEasingAnimation MoveAnimation
        {
            get
            {
                return _moveAnimation;
            }
        }

        void Awake()
        {
            _moveAnimation = GetComponent<MoveToEasingAnimation>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            cardBeingDragged = gameObject;
            startPosition = Transform.localPosition;
            startParent = Transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Transform.position = Input.mousePosition;
            Debug.Log("On Drag");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("On End Drag");
            cardBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            MoveAnimation.SetStartPositionAsCurrentLocalPosition();
            MoveAnimation.SetEndPosition(startPosition);
            MoveAnimation.Play();
        }
    }
}

