using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura_Change : MonoBehaviour
{
    public GameObject aura1;  // 1번 오브젝트
    public GameObject aura2;  // 2번 오브젝트
    public GameObject aura3;  // 3번 오브젝트
    public GameObject aura4;  // 4번 오브젝트

    void Start()
    {
        // 초기화 시 모든 오브젝트 비활성화
        DeactivateAllAuras();
    }

    // 모든 오브젝트 비활성화
    void DeactivateAllAuras()
    {
        aura1.SetActive(false);
        aura2.SetActive(false);
        aura3.SetActive(false);
        aura4.SetActive(false);
    }

    // 1번 아우라 활성화
    public void ActivateAura1()
    {
        DeactivateAllAuras();
        aura1.SetActive(true);
    }

    // 2번 아우라 활성화
    public void ActivateAura2()
    {
        DeactivateAllAuras();
        aura2.SetActive(true);
    }

    // 3번 아우라 활성화
    public void ActivateAura3()
    {
        DeactivateAllAuras();
        aura3.SetActive(true);
    }

    // 4번 아우라 활성화
    public void ActivateAura4()
    {
        DeactivateAllAuras();
        aura4.SetActive(true);
    }
}
