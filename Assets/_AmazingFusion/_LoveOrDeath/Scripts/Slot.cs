using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.AmazingFusion.LoveOrDeath
{
    public class Slot : OptimizedBehaviour, IDropHandler
    {
        [SerializeField]
        Vector3 _slotPosition;

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

        public Vector3 SlotPosition
        {
            get
            {
                return _slotPosition;
            }

            set
            {
                _slotPosition = value;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            print(_slotDrag);
            if (_slotDrag)
            {
                PlayerActionView.cardBeingDragged.transform.SetParent(transform.parent.GetChild(0),true);
                PlayerActionView.cardBeingDragged.PlayAction();
            }
        }
    }
}

