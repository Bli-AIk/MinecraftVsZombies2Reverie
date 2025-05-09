﻿using UnityEngine;

namespace MVZ2.UI
{
    public class HintArrow : MonoBehaviour
    {
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
        public void SetTarget(Transform target, Vector2 offset, float angle)
        {
            targetTransform = target;
            targetOffset = offset;
            transform.localEulerAngles = Vector3.forward * angle;
            UpdatePosition();
        }
        private void Update()
        {
            if (updatesPosition)
                UpdatePosition();
        }
        private void UpdatePosition()
        {
            if (targetTransform)
            {
                var position = transform.position;
                position.x = targetTransform.position.x + targetOffset.x;
                position.y = targetTransform.position.y + targetOffset.y;
                transform.position = position;
            }
            else
            {
                transform.position = new Vector3(-1000, -1000, 0);
            }
        }
        [SerializeField]
        private bool updatesPosition = true;
        [SerializeField]
        private Transform targetTransform;
        [SerializeField]
        private Vector2 targetOffset;
    }
}
