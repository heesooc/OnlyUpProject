using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Option : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void OnEndButtonClicked()
    {
        // 창닫기 버튼 클릭시
        gameObject.SetActive(false);
    }
}
