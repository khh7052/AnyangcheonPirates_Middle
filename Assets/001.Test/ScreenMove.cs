using UnityEngine;
using UnityEngine.EventSystems; // UI 터치 방지를 위해 필요

public class ScreenMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 화면 이동 속도
    public Camera mainCamera;   // 이동시킬 카메라 (지정하지 않으면 메인 카메라 사용)

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 메인 카메라 자동 설정
        }
    }

    // 왼쪽으로 이동
    public void MoveLeft()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI 클릭 방지
        mainCamera.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    // 오른쪽으로 이동
    public void MoveRight()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI 클릭 방지
        mainCamera.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
