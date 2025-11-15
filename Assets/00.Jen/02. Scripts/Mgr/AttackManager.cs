using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // EventSystem 사용

public class AttackManager : MonoBehaviour
{
    public GameObject attackPoint;

    [Header("Preview")]
    public LineRenderer previewLineRenderer;
    public Color[] previewColors = new Color[5];

    public int previewSinCount = 1000;
    public int previewTanCount = 5000;
    public int previewAbsCount = 1000;
    public float previewDivide = 0.01f;

    public float previewPointDistance = 0.1f;
    public float previewLength = 10f;

    [Header("Sin Attack")]
    public GameObject sinAttackPrefab;
    public float sinAttackAmplitude = 1.0f;
    public float sinAttackFrequency = 1.0f;
    public float sinAttackSpeed = 5.0f;

    [Header("Cos Attack")]
    public GameObject cosAttackPrefab;
    public float cosAttackAmplitude = 1.0f;
    public float cosAttackFrequency = 1.0f;
    public float cosAttackSpeed = 5.0f;

    [Header("Tan Attack")]
    public GameObject tanAttackPrefab;
    public float tanAttackAmplitude = 1.0f;
    public float tanAttackFrequency = 1.0f;
    public float tanAttackSpeed = 5.0f;

    [Header("Abs Attack")]
    public GameObject absAttackPrefab;
    public float absAttackAmplitude = 1.0f;
    public float absAttackSpeed = 5.0f;

    private enum AttackType { None, Sin, Cos, Tan, Abs }
    private AttackType selectedAttackType = AttackType.None;
    private Vector3 previewDirection;

    public GameObject sin_Panel;
    public GameObject cos_Panel;
    public GameObject tan_Panel;
    public GameObject abs_Panel;

    private void Start()
    {
        SetPanelsActive(AttackType.None);
    }

    private void Update()
    {
        // 화면 터치 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // UI 클릭인지 확인
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // 터치 위치를 월드 좌표로 변환
                touchPosition.z = 0f;

                // 공격 시작 지점에서 터치 지점 방향 계산
                previewDirection = (touchPosition - attackPoint.transform.position).normalized;
                ShowPreviewPath(); // 미리보기 경로 그리기
            }
        }
        ShowPreviewPath(); // 미리보기 경로 그리기
    }

    /// <summary>
    /// 버튼에서 호출될 함수: 공격 타입 설정
    /// </summary>
    public void SelectSinAttack()
    {
        selectedAttackType = AttackType.Sin;
        SetPanelsActive(selectedAttackType);
        UpdatePreviewLineColor(selectedAttackType);
    }

    public void SelectCosAttack()
    {
        selectedAttackType = AttackType.Cos;
        SetPanelsActive(selectedAttackType);
        UpdatePreviewLineColor(selectedAttackType);
    }

    public void SelectTanAttack()
    {
        selectedAttackType = AttackType.Tan;
        SetPanelsActive(selectedAttackType);
        UpdatePreviewLineColor(selectedAttackType);
    }

    public void SelectAbsAttack()
    {
        selectedAttackType = AttackType.Abs;
        SetPanelsActive(selectedAttackType);
        UpdatePreviewLineColor(selectedAttackType);
    }

    /// <summary>
    /// 버튼에서 호출될 함수: 공격 실행
    /// </summary>
    public void ExecuteAttack()
    {
        if (selectedAttackType == AttackType.None || !MagicCount.magicCountBool) return;

        MagicCount.currentMagicCount--;

        if (selectedAttackType == AttackType.Sin)
        {
            SpawnAttack(sinAttackPrefab, sinAttackAmplitude, sinAttackFrequency, sinAttackSpeed, previewDirection);
        }
        else if (selectedAttackType == AttackType.Cos)
        {
            SpawnAttack(cosAttackPrefab, cosAttackAmplitude, cosAttackFrequency, cosAttackSpeed, previewDirection);
        }
        else if (selectedAttackType == AttackType.Tan)
        {
            SpawnAttack(tanAttackPrefab, tanAttackAmplitude, tanAttackFrequency, tanAttackSpeed);
        }
        else if (selectedAttackType == AttackType.Abs)
        {
            SpawnAttackAbs(absAttackPrefab, absAttackAmplitude, absAttackSpeed, false);
            SpawnAttackAbs(absAttackPrefab, absAttackAmplitude, absAttackSpeed, true);
        }
    }

    void SetPanelsActive(AttackType type)
    {
        sin_Panel.SetActive(type == AttackType.Sin);
        cos_Panel.SetActive(type == AttackType.Cos);
        tan_Panel.SetActive(type == AttackType.Tan);
        abs_Panel.SetActive(type == AttackType.Abs);
    }

    void UpdatePreviewLineColor(AttackType type)
    {
        int idx = (int)type;
        previewLineRenderer.startColor = previewColors[idx];
        previewLineRenderer.endColor = previewColors[idx];
    }

    Vector2 CalculateCoordinates(float m, float L)
    {
        // 각도 계산
        float theta = Mathf.Atan(m);

        // x와 y 계산
        float x = L * Mathf.Cos(theta);
        float y = L * Mathf.Sin(theta);

        return new Vector2(x, y);
    }

    void ShowPreviewPath()
    {
        int pointCount = (selectedAttackType == AttackType.Abs) ? previewAbsCount : (selectedAttackType == AttackType.Tan) ? previewTanCount : previewSinCount;
        float t = 0f;

        Vector3 startPosition = attackPoint.transform.position;
        List<Vector3> points = new();

        if (selectedAttackType == AttackType.Abs)
        {
            // Abs 그래프는 슬라이더 값을 기준으로 계산
            Vector2 pos = CalculateCoordinates(absAttackAmplitude, previewLength);

            Vector2 point1 = startPosition + new Vector3(pos.x, pos.y);
            Vector2 point2 = startPosition;
            Vector2 point3 = startPosition + new Vector3(-pos.x, pos.y);

            Vector2 currentPoint = point1;
            points.Add(currentPoint);

            while (currentPoint != point2)
            {
                currentPoint = Vector2.MoveTowards(currentPoint, point2, previewPointDistance);
                points.Add(currentPoint);
            }

            points.Add(currentPoint);

            while (currentPoint != point3)
            {
                currentPoint = Vector2.MoveTowards(currentPoint, point3, previewPointDistance);
                points.Add(currentPoint);
            }

            previewLineRenderer.positionCount = points.Count;
            previewLineRenderer.SetPositions(points.ToArray());
        }
        else
        {
            // Sin, Cos, Tan 공격의 경로 계산 (터치 방향 기반)
            previewLineRenderer.positionCount = pointCount;

            for (int i = 0; i < pointCount; i++, t += previewDivide)
            {
                Vector3 offset = previewDirection * t * sinAttackSpeed; // 기본 속도 사용
                float y = 0;

                if (selectedAttackType == AttackType.Sin)
                {
                    y = sinAttackAmplitude * Mathf.Sin(sinAttackFrequency * t * sinAttackSpeed);
                }
                else if (selectedAttackType == AttackType.Cos)
                {
                    y = cosAttackAmplitude * Mathf.Cos(cosAttackFrequency * t * cosAttackSpeed);
                }
                else if (selectedAttackType == AttackType.Tan)
                {
                    offset = new Vector3(t * tanAttackSpeed, 0, 0); // x 방향 고정
                    y = tanAttackAmplitude * Mathf.Tan(tanAttackFrequency * t * tanAttackSpeed);
                    y = Mathf.Clamp(y, -3f, 3f); // y 값 제한
                }

                Vector3 newPosition = startPosition + offset;
                newPosition.y += y;
                points.Add(newPosition);
            }

            // 경로 점들을 새로운 리스트에 추가
            Vector3[] positions = points.ToArray();
            previewLineRenderer.positionCount = positions.Length;
            previewLineRenderer.SetPositions(positions);
        }
    }


    void SpawnAttack(GameObject attackPrefab, float amplitude, float frequency, float speed, Vector3 direction)
    {
        if (attackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackPoint.transform.position, Quaternion.identity);
            SinWaveAttack sinWaveAttack = attackInstance.GetComponent<SinWaveAttack>();
            CosWaveAttack cosWaveAttack = attackInstance.GetComponent<CosWaveAttack>();

            if (sinWaveAttack != null)
            {
                sinWaveAttack.amplitude = amplitude;
                sinWaveAttack.frequency = frequency;
                sinWaveAttack.speed = speed;
                sinWaveAttack.direction = direction;
            }
            else if (cosWaveAttack != null)
            {
                cosWaveAttack.amplitude = amplitude;
                cosWaveAttack.frequency = frequency;
                cosWaveAttack.speed = speed;
                cosWaveAttack.direction = direction;
            }
        }
    }
    void SpawnAttack(GameObject attackPrefab, float amplitude, float frequency, float speed)
    {
        if (attackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackPoint.transform.position, Quaternion.identity);
            TanWaveAttack tanWaveAttack = attackInstance.GetComponent<TanWaveAttack>();

            if (tanWaveAttack != null)
            {
                tanWaveAttack.amplitude = amplitude;
                tanWaveAttack.frequency = frequency;
                tanWaveAttack.speed = speed;
            }
        }
    }

    void SpawnAttackAbs(GameObject attackPrefab, float amplitude, float speed, bool moveLeft)
    {
        if (attackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackPoint.transform.position, Quaternion.identity);
            AbsWaveAttack attack = attackInstance.GetComponent<AbsWaveAttack>();

            if (attack != null)
            {
                attack.amplitude = amplitude;
                attack.speed = speed;
                attack.moveLeft = moveLeft;
            }
        }
    }
}
