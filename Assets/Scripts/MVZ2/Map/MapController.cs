using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MukioI18n;
using MVZ2.Extensions;
using MVZ2.Managers;
using MVZ2.Resources;
using MVZ2.UI;
using PVZEngine;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MVZ2.Map
{
    public class MapController : MainScenePage
    {
        public override void Display()
        {
            base.Display();
            ResetCamera();
            ui.SetButtonActive(MapUI.ButtonType.Almanac, Main.SaveManager.IsAlmanacUnlocked());
            ui.SetButtonActive(MapUI.ButtonType.Store, Main.SaveManager.IsStoreUnlocked());
            ui.SetHintText(Main.LanguageManager._(Main.IsMobile() ? HINT_TEXT_MOBILE : HINT_TEXT));
            ui.SetDragRootVisible(false);
            ui.SetOptionsDialogActive(false);
        }
        public override void Hide()
        {
            base.Hide();
            if (model)
            {
                model.OnMapButtonClick -= OnMapButtonClickCallback;
                model.OnEndlessButtonClick -= OnEndlessButtonClickCallback;
                Destroy(model.gameObject);
                model = null;
            }
            SetCameraBackgroundColor(Color.black);
        }
        public void SetMap(NamespaceID mapId)
        {
            var modelMeta = Main.ResourceManager.GetMapMeta(mapId);
            if (modelMeta == null)
                return;
            var modelPrefab = Main.ResourceManager.GetMapModel(modelMeta.path);
            model = Instantiate(modelPrefab.gameObject, modelRoot).GetComponent<MapModel>();
            model.OnMapButtonClick += OnMapButtonClickCallback;
            model.OnEndlessButtonClick += OnEndlessButtonClickCallback;

            for (int i = 0; i < model.GetMapButtonCount(); i++)
            {
                model.SetMapButtonText(i, (i + 1).ToString());
            }
            model.SetEndlessButtonText("E");

            SetCameraBackgroundColor(modelMeta.backgroundColor);
        }
        private void Awake()
        {
            ui.OnButtonClick += OnButtonClickCallback;
        }
        private void Update()
        {
            UpdateTouchDatas();
            if (Input.touchCount > 0)
            {
                UpdateTouch();
            }
            else
            {
                UpdateMouse();
            }
            mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize + cameraScaleSpeed, minCameraSize, maxCameraSize);
            LimitCameraPosition();
            cameraScaleSpeed *= 0.8f;
        }
        private void OnButtonClickCallback(MapUI.ButtonType button)
        {
            switch (button)
            {
                case MapUI.ButtonType.Back:
                    Main.Scene.DisplayPage(MainScenePageType.Mainmenu);
                    break;
                case MapUI.ButtonType.Almanac:
                    break;
                case MapUI.ButtonType.Store:
                    break;
                case MapUI.ButtonType.Setting:
                    ui.SetOptionsDialogActive(true);
                    optionsLogic = new OptionsLogicMap(ui.OptionsDialog);
                    optionsLogic.InitDialog();
                    optionsLogic.OnClose += OnOptionsDialogCloseCallback;
                    break;
            }
        }
        private void OnOptionsDialogCloseCallback()
        {
            optionsLogic.OnClose -= OnOptionsDialogCloseCallback;
            optionsLogic = null;
            ui.SetOptionsDialogActive(false);
        }
        private void OnMapButtonClickCallback(int index)
        {
        }
        private void OnEndlessButtonClickCallback()
        {

        }
        private void ResetCamera()
        {
            mapCamera.transform.localPosition = Vector3.zero;
        }
        private void SetCameraBackgroundColor(Color color)
        {
            mapCamera.backgroundColor = color;
        }
        private void LimitCameraPosition()
        {
            var position = mapCamera.transform.position;
            var aspect = mapCamera.aspect;
            var fullHeight = mapCamera.orthographicSize * 2;
            var cameraHeight = mapCamera.rect.height * fullHeight;
            var cameraWidth = cameraHeight * aspect;
            position.x = Mathf.Clamp(position.x, minCameraPosition.x + cameraWidth * 0.5f, maxCameraPosition.x - cameraWidth * 0.5f);
            position.y = Mathf.Clamp(position.y, minCameraPosition.y + cameraHeight * 0.5f, maxCameraPosition.y - cameraHeight * 0.5f);
            mapCamera.transform.position = position;
        }
        #region ��������
        private void UpdateTouchDatas()
        {
            touchDatas.RemoveAll(d => !Input.touches.Any(t => d.fingerId == t.fingerId));
            for (int i = 0; i < Input.touchCount; i++)
            {
                UpdateTouchData(i, Input.GetTouch(i));
            }
        }
        private void UpdateTouch()
        {
            if (touchDatas.Count > 1)
            {
                var touch0 = touchDatas[0];
                var touch1 = touchDatas[1];
                var position0 = (Vector2)mapCamera.ScreenToWorldPoint(touch0.position);
                var position1 = (Vector2)mapCamera.ScreenToWorldPoint(touch1.position);
                var lastPosition0 = (Vector2)mapCamera.ScreenToWorldPoint(touch0.position - touch0.delta);
                var lastPosition1 = (Vector2)mapCamera.ScreenToWorldPoint(touch1.position - touch1.delta);

                var lastLength = (lastPosition0 - lastPosition1).magnitude;
                var currentLength = (position0 - position1).magnitude;
                var scale = currentLength / lastLength;

                var lastCenter = (lastPosition0 + lastPosition1) * 0.5f;
                var currentCenter = (position0 + position1) * 0.5f;
                var motion = lastCenter - currentCenter;

                mapCamera.orthographicSize = Mathf.Clamp(mapCamera.orthographicSize * scale, minCameraSize, maxCameraSize);
                mapCamera.transform.position += (Vector3)motion;
            }
            else if (touchDatas.Count > 0)
            {
                var touch0 = touchDatas[0];
                var position0 = (Vector2)mapCamera.ScreenToWorldPoint(touch0.position);
                var lastPosition0 = (Vector2)mapCamera.ScreenToWorldPoint(touch0.position - touch0.delta);

                var motion = lastPosition0 - position0;

                mapCamera.transform.position += (Vector3)motion;
            }
        }
        private TouchData GetTouchData(int fingerId)
        {
            return touchDatas.FirstOrDefault(t => t.fingerId == fingerId);
        }
        private void UpdateTouchData(int index, Touch touch)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (IsPositionOnReceiver(touch.position))
                {
                    touchDatas.Add(new TouchData()
                    {
                        fingerId = touch.fingerId,
                        position = touch.position,
                    });
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchDatas.RemoveAll(t => t.fingerId == touch.fingerId);
            }
            else
            {
                var touchData = GetTouchData(touch.fingerId);
                if (touchData != null)
                {
                    touchData.delta = touch.deltaPosition;
                    touchData.position = touch.position;
                }
            }
        }
        #endregion

        #region �������
        private void UpdateMouse()
        {
            var position = Input.mousePosition;
            if (Input.GetMouseButtonDown(1))
                OnRightMouseDown(position);
            if (Input.GetMouseButton(1))
                OnRightMouse(position);
            if (Input.GetMouseButtonUp(1))
                OnRightMouseUp();
            OnMouseScroll(position, Input.mouseScrollDelta);
        }
        private void OnRightMouseDown(Vector2 position)
        {
            if (!IsPositionOnReceiverOrButton(position))
                return;
            ui.SetDragRootVisible(true);
            ui.SetDragRootPosition(position);
            draggingView = true;
            mapDragStartPos = position;
        }
        private void OnRightMouse(Vector2 position)
        {
            if (!draggingView)
                return;
            ui.SetDragArrowTargetPosition(position);
            var cameraPos = mapCamera.transform.position;
            var targetWorldPos = (Vector2)mapCamera.ScreenToWorldPoint(position);
            var fromWorldPos = (Vector2)mapCamera.ScreenToWorldPoint(mapDragStartPos);
            cameraPos += (Vector3)((targetWorldPos - fromWorldPos) * 0.1f);
            mapCamera.transform.position = cameraPos;
        }
        private void OnRightMouseUp()
        {
            draggingView = false;
            ui.SetDragRootVisible(false);
        }
        private void OnMouseScroll(Vector2 position, Vector2 scrollDelta)
        {
            if (scrollDelta.y == 0)
                return;
            if (!IsPositionOnReceiverOrButton(position))
                return;
            cameraScaleSpeed = Mathf.Clamp(cameraScaleSpeed + -scrollDelta.y * 0.1f, -1, 1);
        }
        private bool IsPositionOnReceiver(Vector2 position)
        {
            var eventSystem = EventSystem.current;
            var pointerEventData = new PointerEventData(eventSystem)
            {
                position = position
            };
            raycastResultCache.Clear();
            eventSystem.RaycastAll(pointerEventData, raycastResultCache);
            var first = raycastResultCache.FirstOrDefault(r => r.gameObject).gameObject;
            return !first || first == raycastHitbox;
        }
        private bool IsPositionOnReceiverOrButton(Vector2 position)
        {
            raycastResultCache.Clear();
            var eventSystem = EventSystem.current;
            var pointerEventData = new PointerEventData(eventSystem)
            {
                position = position
            };
            eventSystem.RaycastAll(pointerEventData, raycastResultCache);
            var first = raycastResultCache.FirstOrDefault(r => r.gameObject).gameObject;
            return !first || first == raycastHitbox || first.GetComponentInParent<MapButton>();
        }
        #endregion


        [TranslateMsg("��ͼ����ʾ�ı�")]
        public const string HINT_TEXT = "��ס�Ҽ��϶����ƶ���ͼ\n������������ͼ";
        [TranslateMsg("��ͼ����ʾ�ı�")]
        public const string HINT_TEXT_MOBILE = "��ָ�϶����ƶ���ͼ\n˫ָ������������ͼ";
        private MainManager Main => MainManager.Instance;
        private MapModel model;
        private bool draggingView;
        private Vector2 mapDragStartPos;
        private float cameraScaleSpeed;
        private OptionsLogicMap optionsLogic;
        private List<RaycastResult> raycastResultCache = new List<RaycastResult>();
        private List<TouchData> touchDatas = new List<TouchData>();
        [SerializeField]
        private MapUI ui;
        [SerializeField]
        private GameObject raycastHitbox;
        [SerializeField]
        private Transform modelRoot;
        [SerializeField]
        private Camera mapCamera;
        [SerializeField]
        private Vector2 minCameraPosition = new Vector2(-16, -12);
        [SerializeField]
        private Vector2 maxCameraPosition = new Vector2(16, 12);
        [SerializeField]
        private float minCameraSize = 2;
        [SerializeField]
        private float maxCameraSize = 6;

        private class TouchData
        {
            public int fingerId;
            public Vector2 position;
            public Vector2 delta;
        }
    }
}
