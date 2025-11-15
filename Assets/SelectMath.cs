using System.Collections;
using TMPro;
using UnityEngine;

public class SelectMath : MonoBehaviour
{
    public TextMeshProUGUI[] texts; // 깜빡일 텍스트들을 배열로 관리
    public float blinkDuration = 1.0f; // 색상이 변경되는 간격
    public Color blinkColor = Color.red; // 깜빡일 때의 색상

    private Coroutine[] blinkCoroutines;
    private Color[] originalColors;

    void Start()
    {
        // 텍스트 배열과 동일한 크기의 코루틴 배열 초기화
        blinkCoroutines = new Coroutine[texts.Length];
        originalColors = new Color[texts.Length];

        // 각 텍스트의 원래 색상 저장
        for (int i = 0; i < texts.Length; i++)
        {
            originalColors[i] = texts[i].color;
        }
    }

    /// <summary>
    /// 버튼에서 호출되는 함수: 해당 인덱스의 텍스트를 깜빡이기 시작
    /// </summary>
    public void OnButtonPressed(int index)
    {
        StartBlinking(index);
    }

    void StartBlinking(int index)
    {
        // 이전에 실행 중인 모든 깜빡이는 코루틴 정지
        StopAllBlinkingCoroutines();

        // 선택된 텍스트의 깜빡이는 코루틴 시작
        if (index < texts.Length)
        {
            blinkCoroutines[index] = StartCoroutine(BlinkText(texts[index], originalColors[index]));
        }
    }

    void StopAllBlinkingCoroutines()
    {
        for (int i = 0; i < blinkCoroutines.Length; i++)
        {
            if (blinkCoroutines[i] != null)
            {
                StopCoroutine(blinkCoroutines[i]);
                texts[i].color = originalColors[i]; // 텍스트 색상을 원래 색상으로 복원
                blinkCoroutines[i] = null;
            }
        }
    }

    IEnumerator BlinkText(TextMeshProUGUI text, Color originalColor)
    {
        while (true)
        {
            float elapsedTime = 0f;

            // 서서히 원래 색상에서 깜빡이는 색상으로 변경
            while (elapsedTime < blinkDuration)
            {
                text.color = Color.Lerp(originalColor, blinkColor, elapsedTime / blinkDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;

            // 서서히 깜빡이는 색상에서 원래 색상으로 변경
            while (elapsedTime < blinkDuration)
            {
                text.color = Color.Lerp(blinkColor, originalColor, elapsedTime / blinkDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
