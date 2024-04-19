using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_SuccessTimer : MonoBehaviour
{
    public float elapsedTime = 0f;
    private bool timerRunning = true;

    void Update()
    {
        // 타이머 실행
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 다른 오브젝트가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            // 타이머 중지
            timerRunning = false;

            // 걸린 시간을 저장
            PlayerPrefs.SetFloat("LevelTime", elapsedTime);

            // EndingScene으로 전환
            SceneManager.LoadScene("EndingScene");
        }
    }
}
