using UnityEngine;
using UnityEngine.UI;

public class UI_ShowTime : MonoBehaviour
{
    public Text timeText;  // 시간을 표시할 Text 컴포넌트의 참조

    void Start()
    {
        // 저장된 시간 불러오기
        float levelTime = PlayerPrefs.GetFloat("LevelTime", 0);

        // 시간을 초에서 분으로 변환하고 정수로 변환 (소수점 제거)
        int levelTimeMinutes = Mathf.FloorToInt(levelTime / 60);

        // Text 컴포넌트에 분 단위의 시간 표시
        timeText.text = "Time: " + levelTimeMinutes.ToString() + " minutes";
    }
}
