using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneNames
{
    Lobby,  // 0
    Main,   // 1
    Ending  // 2
}

public class LobbyScene : MonoBehaviour
{
    public UI_Option UI_Option;

    public void OnContinueButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnOptionButtonClicked()
    {
        UI_Option.Open();
    }

    public void OnEndButtonClicked()
    {
        // 빌드 후 실행했을 경우 종료하는 방법
        Application.Quit();

#if UNITY_EDITOR
        // 유니티 에디터에서 실행했을 경우 종료하는 방법
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
