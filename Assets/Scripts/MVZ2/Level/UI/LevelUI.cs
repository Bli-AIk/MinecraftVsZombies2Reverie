using System;
using System.Collections;
using System.Collections.Generic;
using MVZ2.GameContent;
using MVZ2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MVZ2.Level.UI
{
    public class LevelUI : MonoBehaviour
    {
        #region ���з���

        #region ����
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        public void SetRaycasterMask(LayerMask mask)
        {
            foreach (var raycaster in raycasters)
            {
                raycaster.blockingMask = mask;
            }
        }
        #endregion

        #region �����
        public void SetCameraLerp(float lerp)
        {
            foreach (var rectTrans in limitRectTransforms)
            {
                if (!rectTrans)
                    return;
                var anchorX = Mathf.Lerp(cameraLimitMinAnchorX, cameraLimitMaxAnchorX, lerp);
                var anchorMin = rectTrans.anchorMin;
                var anchorMax = rectTrans.anchorMax;
                var pivot = rectTrans.pivot;
                anchorMin.x = anchorX;
                anchorMax.x = anchorX;
                pivot.x = anchorX;
                rectTrans.anchorMin = anchorMin;
                rectTrans.anchorMax = anchorMax;
                rectTrans.pivot = pivot;
                rectTrans.anchoredPosition = Vector2.zero;
            }
        }
        #endregion

        #region ���Ͻ�

        #region ����
        public void SetEnergyVisible(bool value)
        {
            energyPanel.SetActive(value);
        }
        public void SetEnergy(string value)
        {
            energyPanel.SetEnergy(value);
        }
        #endregion

        #region ��ͼ
        public void SetBlueprintsVisible(bool value)
        {
            blueprints.SetActive(value);
        }
        public void SetBlueprints(BlueprintViewData[] viewDatas)
        {
            blueprints.SetBlueprints(viewDatas);
        }
        public void SetBlueprintRecharges(float[] recharges)
        {
            blueprints.SetRecharges(recharges);
        }
        public void SetBlueprintDisabled(bool[] disabledValues)
        {
            blueprints.SetDisabled(disabledValues);
        }
        public void SetBlueprintCount(int count)
        {
            blueprints.SetBlueprintCount(count);
        }
        #endregion

        #region ����
        public void SetPickaxeSlotVisible(bool visible)
        {
            pickaxeSlot.SetActive(visible);
        }
        public void SetPickaxeVisible(bool visible)
        {
            pickaxeSlot.SetPickaxeVisible(visible);
        }
        #endregion

        #endregion

        #region ���½�

        #region Ǯ
        public void SetMoney(string money)
        {
            moneyPanel.SetMoney(money);
        }
        public void HideMoney()
        {
            moneyPanel.Hide();
        }
        public void SetMoneyFade(bool fade)
        {
            moneyPanel.SetFade(fade);
        }
        public void ResetMoneyFadeTime()
        {
            moneyPanel.ResetTimeout();
        }
        #endregion

        #region ��֮��Ƭ
        public void SetStarshardVisible(bool visible)
        {
            starshardPanel.SetActive(visible);
        }
        public void SetStarshardCount(int count, int maxCount)
        {
            starshardPanel.SetPoints(count, maxCount);
        }
        #endregion

        #endregion

        #region ���Ͻ�

        public void SetTopRightVisible(bool visible)
        {
            topRightObj.SetActive(visible);
        }

        #region ��Ϸ�Ѷ�
        public void SetDifficulty(string difficulty)
        {
            difficultyText.text = difficulty;
        }
        #endregion

        #region ����
        public void SetSpeedUpVisible(bool visible)
        {
            speedUpButton.gameObject.SetActive(visible);
        }
        public void SetSpeedUp(bool speedUp)
        {
            speedUpEnabledObject.SetActive(speedUp);
            speedUpDisabledObject.SetActive(!speedUp);
        }
        #endregion

        #endregion

        #region �ؿ�����

        #region �ؿ���
        public void SetLevelNameVisible(bool visible)
        {
            levelNameText.gameObject.SetActive(visible);
        }
        public void SetLevelName(string name)
        {
            levelNameText.text = name;
        }
        #endregion

        #region �ؿ�����
        public void SetProgressVisible(bool visible)
        {
            progressBar.gameObject.SetActive(visible);
        }
        public void SetProgress(float progress)
        {
            progressBar.SetProgress(progress);
        }
        public void SetBannerProgresses(float[] progresses)
        {
            progressBar.SetBannerProgresses(progresses);
        }
        #endregion

        #endregion

        #region �ֳ���Ʒ
        public void SetHeldItemPosition(Vector2 worldPos)
        {
            heldItem.transform.position = worldPos;
        }
        public void SetHeldItemIcon(Sprite sprite)
        {
            heldItem.SetIcon(sprite);
        }
        #endregion

        #region ��ʾ�ı�
        public void SetHugeWaveTextVisible(bool visible)
        {
            hugeWaveText.SetActive(visible);
        }
        public void SetFinalWaveTextVisible(bool visible)
        {
            finalWaveText.SetActive(visible);
        }
        public void SetReadySetBuildVisible(bool visible)
        {
            readyText.SetActive(visible);
        }
        public void SetLevelTextAnimationSpeed(float speed)
        {
            hugeWaveText.TextAnimator.speed = speed;
            finalWaveText.TextAnimator.speed = speed;
        }
        #endregion

        #endregion

        #region ˽�з���
        private void Awake()
        {
            sideReceiver.OnPointerDown += (data) => OnRaycastReceiverPointerDown?.Invoke(Receiver.Side);
            lawnReceiver.OnPointerDown += (data) => OnRaycastReceiverPointerDown?.Invoke(Receiver.Lawn);
            bottomReceiver.OnPointerDown += (data) => OnRaycastReceiverPointerDown?.Invoke(Receiver.Bottom);

            blueprints.OnBlueprintPointerDown += (index, data) => OnBlueprintPointerDown?.Invoke(index, data);
            pickaxeSlot.OnPointerDown += (data) => OnPickaxePointerDown?.Invoke(data);
            starshardPanel.OnPointerDown += (data) => OnStarshardPointerDown?.Invoke(data);
            menuButton.onClick.AddListener(() => OnMenuButtonClick?.Invoke());
            speedUpButton.onClick.AddListener(() => OnSpeedUpButtonClick?.Invoke());
            readyText.OnStartGameCalled += () => OnStartGameCalled?.Invoke();
        }

        #endregion

        #region �¼�
        public event Action<Receiver> OnRaycastReceiverPointerDown;
        public event Action<int, PointerEventData> OnBlueprintPointerDown;
        public event Action<PointerEventData> OnPickaxePointerDown;
        public event Action<PointerEventData> OnStarshardPointerDown;
        public event Action OnMenuButtonClick;
        public event Action OnSpeedUpButtonClick;
        public event Action OnStartGameCalled;
        #endregion

        #region �����ֶ�
        [Header("Blueprints")]
        [SerializeField]
        EnergyPanel energyPanel;
        [SerializeField]
        BlueprintList blueprints;
        [SerializeField]
        PickaxeSlot pickaxeSlot;

        [Header("Texts")]
        [SerializeField]
        LevelHintText hugeWaveText;
        [SerializeField]
        LevelHintText finalWaveText;
        [SerializeField]
        ReadySetBuild readyText;

        [Header("Raycast Receivers")]
        [SerializeField]
        GraphicRaycaster[] raycasters;
        [SerializeField]
        RaycastReceiver sideReceiver;
        [SerializeField]
        RaycastReceiver lawnReceiver;
        [SerializeField]
        RaycastReceiver bottomReceiver;

        [Header("CameraLimit")]
        [SerializeField]
        RectTransform[] limitRectTransforms;
        [SerializeField]
        float cameraLimitMinAnchorX;
        [SerializeField]
        float cameraLimitMaxAnchorX;

        [Header("HeldItem")]
        [SerializeField]
        HeldItem heldItem;

        [Header("Bottom")]
        [SerializeField]
        MoneyPanel moneyPanel;
        [SerializeField]
        StarshardPanel starshardPanel;
        [SerializeField]
        TextMeshProUGUI levelNameText;
        [SerializeField]
        ProgressBar progressBar;

        [Header("Right Top")]
        [SerializeField]
        GameObject topRightObj;
        [SerializeField]
        Button speedUpButton;
        [SerializeField]
        GameObject speedUpEnabledObject;
        [SerializeField]
        GameObject speedUpDisabledObject;
        [SerializeField]
        Button menuButton;
        [SerializeField]
        TextMeshProUGUI difficultyText;
        #endregion

        #region ��Ƕ��
        public enum Receiver
        {
            Side,
            Lawn,
            Bottom
        }
        #endregion
    }
}
