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

        public void OnBeginDrag(PointerEventData eventData)
        {
            cardBeingDragged = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            cardBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (transform.parent == startParent)
            {
                transform.position = startPosition;
            }
        }
    }
}

