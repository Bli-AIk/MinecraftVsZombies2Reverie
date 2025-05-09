﻿using UnityEngine;

namespace MVZ2.Cameras
{
    [ExecuteAlways]
    public class LevelCamera : MonoBehaviour
    {
        public void SetPosition(Vector3 position, Vector2 anchor)
        {
            cameraPosition = position;
            cameraAnchor = anchor;
            UpdatePosition();
        }
        public void SetSpace(float left)
        {
            leftspace = left;
            UpdatePosition();
        }
        public void SetRotation(float rotation)
        {
            cameraRotation = rotation;
            UpdatePosition();
        }
        public float GetRotation()
        {
            return cameraRotation;
        }
        private void OnEnable()
        {
            UpdatePosition();
        }
        private void Update()
        {
            UpdatePosition();
        }
        private void UpdatePosition()
        {
            if (!_camera)
                return;
            var aspect = Screen.width * _camera.rect.width / (Screen.height * _camera.rect.height);
            var height = _camera.orthographicSize * 2;
            var cameraSize = new Vector2(height * aspect, height);
            var clippedSize = new Vector2(cameraSize.x - leftspace, cameraSize.y);
            var clippedOffset = clippedSize * cameraAnchor - clippedSize * 0.5f;
            var viewportCenter = CameraPosition - (Vector3)clippedOffset;
            var quaternion = Quaternion.Euler(0, 0, cameraRotation);
            _viewportTransform.position = viewportCenter;
            _viewportTransform.rotation = quaternion;

            var offset = (Vector3)((Vector2.one * 0.5f - CameraAnchor) * cameraSize);
            _camera.transform.localPosition = CameraPosition + offset + ShakeOffset - viewportCenter;
        }
        public Vector3 CameraPosition
        {
            get => cameraPosition;
            set
            {
                cameraPosition = value;
                UpdatePosition();
            }
        }
        public Vector2 CameraAnchor
        {
            get => cameraAnchor;
            set
            {
                cameraAnchor = value;
                UpdatePosition();
            }
        }
        public Vector3 ShakeOffset
        {
            get => cameraShakeOffset;
            set
            {
                cameraShakeOffset = value;
                UpdatePosition();
            }
        }
        public Camera Camera => _camera;
        [SerializeField]
        private Transform _viewportTransform;
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private Vector2 cameraAnchor;
        [SerializeField]
        private Vector3 cameraPosition;
        private Vector3 cameraShakeOffset;
        private float cameraRotation;
        private float leftspace;
    }
}