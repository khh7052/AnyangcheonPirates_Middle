using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Changer : MonoBehaviour
{
    /// <summary>
    /// 버튼을 눌렀을 때 호출되는 메서드. 씬 이름을 매개변수로 받아 해당 씬으로 이동합니다.
    /// </summary>
    /// <param name="sceneName">이동할 씬의 이름</param>
    public void ChangeScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("씬 이름이 비어있거나 null입니다. 씬 이동을 중단합니다.");
            return;
        }

        // 씬이 존재하는지 확인 (Optional)
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"씬 '{sceneName}'으로 이동 중...");
        }
        else
        {
            Debug.LogError($"씬 '{sceneName}'을(를) 찾을 수 없습니다. 씬 이름을 확인하세요.");
        }
    }
}
