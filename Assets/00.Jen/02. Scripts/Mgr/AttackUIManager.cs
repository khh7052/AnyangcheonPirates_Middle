using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackUIManager : MonoBehaviour
{
    public Slider sinAmplitudeSlider;
    public TextMeshProUGUI sinAmplitudeText;
    public Slider sinFrequencySlider;
    public TextMeshProUGUI sinFrequencyText;

    public Slider cosAmplitudeSlider;
    public TextMeshProUGUI cosAmplitudeText;
    public Slider cosFrequencySlider;
    public TextMeshProUGUI cosFrequencyText;

    public Slider tanAmplitudeSlider;
    public TextMeshProUGUI tanAmplitudeText;
    public Slider tanFrequencySlider;
    public TextMeshProUGUI tanFrequencyText;

    public Slider absAmplitudeSlider;
    public TextMeshProUGUI absAmplitudeText;

    public AttackManager attackManager;
    public float sliderStep = 0.1f;

    private enum AttackType { Sin, Cos, Tan, Abs }
    private AttackType selectedAttackType = AttackType.Sin; // 기본 Sin 타입 활성화

    void Start()
    {
        InitializeSlider(sinAmplitudeSlider, 0f, 5f, attackManager.sinAttackAmplitude, OnSinAmplitudeChanged);
        InitializeSlider(sinFrequencySlider, 0f, 3f, attackManager.sinAttackFrequency, OnSinFrequencyChanged);
        InitializeSlider(cosAmplitudeSlider, 0f, 5f, attackManager.cosAttackAmplitude, OnCosAmplitudeChanged);
        InitializeSlider(cosFrequencySlider, 0f, 3f, attackManager.cosAttackFrequency, OnCosFrequencyChanged);
        InitializeSlider(tanAmplitudeSlider, 1f, 3f, attackManager.tanAttackAmplitude, OnTanAmplitudeChanged);
        InitializeSlider(tanFrequencySlider, 1f, 3f, attackManager.tanAttackFrequency, OnTanFrequencyChanged);
        InitializeSlider(absAmplitudeSlider, 0f, 5f, attackManager.absAttackAmplitude, OnAbsAmplitudeChanged);

        //UpdateAllTexts();
    }

    /// <summary>
    /// 공격 유형 선택 함수 (버튼에서 호출)
    /// </summary>
    public void SelectAttackType(string type)
    {
        if (type == "Sin") selectedAttackType = AttackType.Sin;
        else if (type == "Cos") selectedAttackType = AttackType.Cos;
        else if (type == "Tan") selectedAttackType = AttackType.Tan;
        else if (type == "Abs") selectedAttackType = AttackType.Abs;

        Debug.Log($"Selected Attack Type: {selectedAttackType}");
    }

    /// <summary>
    /// UP 버튼 클릭: 활성화된 유형의 모든 슬라이더 값을 증가
    /// </summary>
    public void IncreaseActiveSliders()
    {
        AdjustActiveSliders(sliderStep);
    }

    /// <summary>
    /// DOWN 버튼 클릭: 활성화된 유형의 모든 슬라이더 값을 감소
    /// </summary>
    public void DecreaseActiveSliders()
    {
        AdjustActiveSliders(-sliderStep);
    }

    /// <summary>
    /// 활성화된 공격 유형의 슬라이더 값을 조정
    /// </summary>
    private void AdjustActiveSliders(float step)
    {
        switch (selectedAttackType)
        {
            case AttackType.Sin:
                AdjustSliderValue(step, sinAmplitudeSlider, sinFrequencySlider);
                UpdateText(sinAmplitudeText, sinAmplitudeSlider);
                UpdateText(sinFrequencyText, sinFrequencySlider);
                break;
            case AttackType.Cos:
                AdjustSliderValue(step, cosAmplitudeSlider, cosFrequencySlider);
                UpdateText(cosAmplitudeText, cosAmplitudeSlider);
                UpdateText(cosFrequencyText, cosFrequencySlider);
                break;
            case AttackType.Tan:
                AdjustSliderValue(step, tanAmplitudeSlider, tanFrequencySlider);
                UpdateText(tanAmplitudeText, tanAmplitudeSlider);
                UpdateText(tanFrequencyText, tanFrequencySlider);
                break;
            case AttackType.Abs:
                AdjustSliderValue(step, absAmplitudeSlider);
                UpdateText(absAmplitudeText, absAmplitudeSlider);
                break;
        }
    }

    /// <summary>
    /// 슬라이더 값 조정 함수
    /// </summary>
    private void AdjustSliderValue(float step, params Slider[] sliders)
    {
        foreach (var slider in sliders)
        {
            slider.value = Mathf.Clamp(slider.value + step, slider.minValue, slider.maxValue);
        }
    }

    void InitializeSlider(Slider slider, float minValue, float maxValue, float initialValue, UnityEngine.Events.UnityAction<float> callback)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = initialValue;
        slider.onValueChanged.AddListener(callback);
    }

    void UpdateText(TextMeshProUGUI text, Slider slider)
    {
        text.text = $"{slider.value:F1} / {slider.maxValue:F1}";
    }

    void OnSinAmplitudeChanged(float value)
    {
        attackManager.sinAttackAmplitude = value;
        UpdateText(sinAmplitudeText, sinAmplitudeSlider);
    }

    void OnSinFrequencyChanged(float value)
    {
        attackManager.sinAttackFrequency = value;
        UpdateText(sinFrequencyText, sinFrequencySlider);
    }

    void OnCosAmplitudeChanged(float value)
    {
        attackManager.cosAttackAmplitude = value;
        UpdateText(cosAmplitudeText, cosAmplitudeSlider);
    }

    void OnCosFrequencyChanged(float value)
    {
        attackManager.cosAttackFrequency = value;
        UpdateText(cosFrequencyText, cosFrequencySlider);
    }

    void OnTanAmplitudeChanged(float value)
    {
        attackManager.tanAttackAmplitude = value;
        UpdateText(tanAmplitudeText, tanAmplitudeSlider);
    }

    void OnTanFrequencyChanged(float value)
    {
        attackManager.tanAttackFrequency = value;
        UpdateText(tanFrequencyText, tanFrequencySlider);
    }

    void OnAbsAmplitudeChanged(float value)
    {
        attackManager.absAttackAmplitude = value;
        UpdateText(absAmplitudeText, absAmplitudeSlider);
    }
}
