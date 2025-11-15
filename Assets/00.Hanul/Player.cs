using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    AnimController animController; // 애니메이션 컨트롤러
    public float directionScale = 1f; // 캐릭터 방향 전환을 위한 스케일 값 (기본: 1)

    void Start()
    {
        animController = GetComponent<AnimController>();
    }

    void Update()
    {
        // UI 터치 감지를 무시 (UI 버튼과 충돌 방지)
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // 모바일 터치 입력
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleAttack(touch.position);
            }
        }

        // 마우스 입력 (에디터 테스트용)
        if (Input.GetMouseButtonDown(0))
        {
            HandleAttack(Input.mousePosition);
        }
    }

    // 공격 처리 및 방향 전환
    void HandleAttack(Vector3 inputPosition)
    {
        // 화면 클릭 또는 터치 위치 → 월드 좌표로 변환
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
        worldPosition.z = 0f;

        // 방향 전환: 클릭한 위치가 캐릭터의 왼쪽인지 오른쪽인지 비교
        if (worldPosition.x < transform.position.x)
        {
            // 왼쪽 방향
            transform.localScale = new Vector3(-directionScale, directionScale, 1f);
        }
        else
        {
            // 오른쪽 방향
            transform.localScale = new Vector3(directionScale, directionScale, 1f);
        }

        // 공격 애니메이션 실행
        Attack();
    }

    void Attack()
    {
        animController.anim.SetTrigger("Jab"); // 공격 애니메이션 재생
        Debug.Log("Attack Triggered!");
    }
}
