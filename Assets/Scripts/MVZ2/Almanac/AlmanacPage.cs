﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Almanacs
{
    public abstract class AlmanacPage : MonoBehaviour
    {
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        protected virtual void Awake()
        {
            returnButton.onClick.AddListener(() => OnReturnClick?.Invoke());
        }
        public event Action OnReturnClick;
        [SerializeField]
        private Button returnButton;
    }
}
