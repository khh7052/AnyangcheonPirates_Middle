using UnityEngine;

public class Google_Button : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // 활성화/비활성화할 오브젝트

    /// <summary>
    /// 버튼 클릭 시 실행될 함수
    /// </summary>
    public void ToggleObject()
    {
        if (targetObject != null)
        {
            // 현재 활성화 상태를 반전시킴
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);

            // 상태 확인용 로그 출력
            if (targetObject.activeSelf)
            {
                Debug.Log($"{targetObject.name}이 활성화되었습니다.");
            }
            else
            {
                Debug.Log($"{targetObject.name}이 비활성화되었습니다.");
            }
        }
        else
        {
            Debug.LogWarning("Target Object가 설정되지 않았습니다.");
        }
    }
}
