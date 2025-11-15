using UnityEngine;

public class Start_Button : MonoBehaviour
{
    [SerializeField] private string[] targetObjectNames; // 비활성화/활성화할 오브젝트 이름 배열
    [SerializeField] private string stagePanelName;      // Stage UI 패널 이름
    [SerializeField] private string explainPanelName;    // Explain 오브젝트 이름

    private GameObject[] targetObjects;
    private GameObject stagePanel;
    private GameObject explainPanel;

    private void Start()
    {
        FindObjects(); // 게임 시작 시 오브젝트 찾기
    }

    private void Update()
    {
        // 오브젝트가 null인 경우 다시 찾기
        if (targetObjects == null || stagePanel == null || explainPanel == null)
        {
            FindObjects();
        }

        // Explain 패널이 활성화 상태인지 확인
        if (explainPanel != null && explainPanel.activeSelf)
        {
            SetTargetObjectsActive(false);
            Debug.Log($"{explainPanel.name}이 활성화되었습니다. 모든 타겟 오브젝트 비활성화됨.");
        }
        // Stage 패널이 활성화 상태인지 확인
        else if (stagePanel != null && stagePanel.activeSelf)
        {
            SetTargetObjectsActive(false);
            Debug.Log($"{stagePanel.name}이 활성화되었습니다. 모든 타겟 오브젝트 비활성화됨.");
        }
        else
        {
            SetTargetObjectsActive(true);
            Debug.Log($"{stagePanel.name}과 {explainPanel.name}이 비활성화되었습니다. 모든 타겟 오브젝트 활성화됨.");
        }
    }

    /// <summary>
    /// 이름을 기준으로 targetObjects, stagePanel, explainPanel을 찾아 설정
    /// </summary>
    private void FindObjects()
    {
        if (targetObjectNames == null || targetObjectNames.Length == 0 || string.IsNullOrEmpty(stagePanelName) || string.IsNullOrEmpty(explainPanelName))
        {
            Debug.LogWarning("targetObjectNames, stagePanelName 또는 explainPanelName이 설정되지 않았습니다.");
            return;
        }

        // targetObjects 배열 초기화 및 할당
        targetObjects = new GameObject[targetObjectNames.Length];
        for (int i = 0; i < targetObjectNames.Length; i++)
        {
            if (!string.IsNullOrEmpty(targetObjectNames[i]))
            {
                targetObjects[i] = GameObject.Find(targetObjectNames[i]);
                if (targetObjects[i] != null)
                    Debug.Log($"targetObject '{targetObjectNames[i]}'를 찾았습니다.");
                else
                    Debug.LogWarning($"targetObject '{targetObjectNames[i]}'를 찾을 수 없습니다.");
            }
        }

        if (stagePanel == null)
        {
            stagePanel = GameObject.Find(stagePanelName);
            if (stagePanel != null)
                Debug.Log($"stagePanel '{stagePanelName}'를 찾았습니다.");
            else
                Debug.LogWarning($"stagePanel '{stagePanelName}'를 찾을 수 없습니다.");
        }

        if (explainPanel == null)
        {
            explainPanel = GameObject.Find(explainPanelName);
            if (explainPanel != null)
                Debug.Log($"explainPanel '{explainPanelName}'를 찾았습니다.");
            else
                Debug.LogWarning($"explainPanel '{explainPanelName}'를 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 모든 타겟 오브젝트의 활성/비활성 상태 설정
    /// </summary>
    /// <param name="isActive">true면 활성화, false면 비활성화</param>
    private void SetTargetObjectsActive(bool isActive)
    {
        if (targetObjects == null) return;

        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
                Debug.Log($"{obj.name}이 {(isActive ? "활성화" : "비활성화")}되었습니다.");
            }
        }
    }
}
