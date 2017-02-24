using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.AmazingFusion.LoveOrDeath
{
    public class Slot : OptimizedBehaviour, IDropHandler
    {
        public GameObject _slotDrag
        {
            get
            {
                if (transform.name == "DragZone")
                    {
                        return gameObject;
                    }
                return null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (_slotDrag && PlayerActionView.cardBeingDragged != null)
            {
                PlayerActionView.cardBeingDragged.Transform.SetParent(Transform.parent.GetChild(0), true);
                ((RectTransform)PlayerActionView.cardBeingDragged.Transform).sizeDelta = ((RectTransform)PlayerActionView.cardBeingDragged.Transform.parent).sizeDelta;
                PlayerActionView.cardBeingDragged.Transform.localScale = Vector3.one;
                PlayerActionView.cardBeingDragged.Transform.localPosition = Vector3.zero;
                PlayerActionView.cardBeingDragged.PlayAction();
            }
        }
    }
}

