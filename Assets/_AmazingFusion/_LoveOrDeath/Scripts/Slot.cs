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

        public GameObject _card
        {
            get
            {
                if(transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
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
            if (transform.childCount == 1)
            {
                ActionView.cardBeingDragged.transform.SetParent(transform.GetChild(0),true);
            }
        }
    }
}

