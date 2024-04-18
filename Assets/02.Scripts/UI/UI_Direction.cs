using UnityEngine;
using UnityEngine.UI;

public class UI_Direction : MonoBehaviour
{
    public Transform targetPosition; // UI 요소를 고정할 위치
    public Canvas canvas; // UI 요소가 속한 Canvas
    public RectTransform uiElement; // 고정할 UI 요소의 RectTransform (예: Text 또는 Image)

    void LateUpdate()
    {
        if (canvas != null && uiElement != null && targetPosition != null)
        {
            // 월드 좌표를 스크린 좌표로 변환
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition.position);

            // Canvas에 맞게 좌표 변환
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out canvasPosition);

            // UI 요소의 위치를 업데이트
            uiElement.localPosition = canvasPosition;
        }
    }
}