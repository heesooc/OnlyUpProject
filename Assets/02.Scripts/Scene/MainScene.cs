using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 관리를 위한 네임스페이스 추가

public class MainScene : MonoBehaviour
{
    // 플레이어가 지구 오브젝트에 닿았을 때 
    // EndingScene으로 넘어가도록 하는 코드
    private void OnTriggerEnter(Collider other)
    {
        // 다른 오브젝트가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
