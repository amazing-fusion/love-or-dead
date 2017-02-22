using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.AmazingFusion.LoveOrDeath
{
    public class Slot : OptimizedBehaviour, IDropHandler
    {
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

        public void OnDrop(PointerEventData eventData)
        {
            if (!_card)
            {
                ActionView.cardBeingDragged.transform.SetParent(transform);
            }
        }
    }
}

