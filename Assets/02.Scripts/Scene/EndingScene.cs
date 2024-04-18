using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EndingScene : MonoBehaviour
{
    public Text subtitleText;
    public Button resetButton;
    private List<string> subtitles = new List<string>();

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        // 초기 자막 추가
        subtitles.Add("안녕?");
        subtitles.Add("'너 누구야'");
        subtitles.Add("나는 너의 고난들을 지켜봐온 자야");
        subtitles.Add("몸과 마음이 다 다치고 힘들겠네");
        subtitles.Add("네가 생각하는 삶은 뭐니?");
        subtitles.Add("...");
        subtitles.Add("네게 새로운 삶을 줄 수 있다면 어떨까?");
        subtitles.Add("어떤 선택을 해나갈지 기대된다");
        subtitles.Add("'2회차 인생으로 리셋하시겠습니까?'");

        // 리셋 버튼 비활성화
        resetButton.gameObject.SetActive(false);

        // 자막 코루틴 시작
        StartCoroutine(ShowSubtitles());
    }

    IEnumerator ShowSubtitles()
    {
        foreach (string subtitle in subtitles)
        {
            subtitleText.text = subtitle;
            yield return new WaitForSeconds(3f);

            // '2회차 인생으로 리셋하시겠습니까?' 자막 이후에 리셋 버튼 활성화
            if (subtitle == "'2회차 인생으로 리셋하시겠습니까?'")
            {
                resetButton.gameObject.SetActive(true);
            }
        }
    }

    public void OnClickResetButton()
    {
        // LobbyScene으로 돌아가기
        SceneManager.LoadScene("LobbyScene");
    }
}
