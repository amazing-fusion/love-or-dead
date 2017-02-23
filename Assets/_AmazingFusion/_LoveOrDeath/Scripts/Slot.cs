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
            if ( _slotDrag)
            {
                PlayerActionView.cardBeingDragged.transform.SetParent(transform.parent.GetChild(0),true);
                PlayerActionView.cardBeingDragged.PlayAction();
            }
        }
    }
}

