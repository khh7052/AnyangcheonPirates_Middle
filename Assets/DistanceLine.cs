using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceLine : MonoBehaviour
{
    public Transform player;
    private Transform object2;

    public LineRenderer lineRenderer; // 두 오브젝트를 연결하는 라인
    public LineRenderer xLineRenderer; // x축 거리 라인
    public LineRenderer yLineRenderer; // y축 거리 라인

    void Update()
    {
        // 화면 터치 감지 및 object2 할당
        if (Input.touchCount > 0) // 터치가 발생했는지 확인
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치 가져오기

            if (touch.phase == TouchPhase.Began) // 터치가 시작되었을 때
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider != null && hit.collider.CompareTag("Enemy"))
                {
                    object2 = hit.transform; // 터치한 Enemy 오브젝트 할당
                }
            }
        }

        // 두 오브젝트가 할당된 경우에만 거리 계산 및 라인 업데이트
        if (player != null && object2 != null)
        {
            // 두 오브젝트의 위치를 가져옴
            Vector2 position1 = player.position;
            Vector2 position2 = object2.position;

            // 두 오브젝트 사이의 거리 계산
            float distance = Vector2.Distance(position1, position2);
            float xDistance = Mathf.Abs(position1.x - position2.x);
            float yDistance = Mathf.Abs(position1.y - position2.y);

            // 전체 거리 라인 업데이트
            lineRenderer.SetPosition(0, position1);
            lineRenderer.SetPosition(1, position2);

            // x축 거리 라인 업데이트
            xLineRenderer.SetPosition(0, position1);
            xLineRenderer.SetPosition(1, new Vector2(position2.x, position1.y));

            // y축 거리 라인 업데이트
            yLineRenderer.SetPosition(0, new Vector2(position2.x, position1.y));
            yLineRenderer.SetPosition(1, position2);
        }
    }
}
