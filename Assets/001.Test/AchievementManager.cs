using UnityEngine;
using GooglePlayGames;

public class AchievementManager : MonoBehaviour
{
    // 업적 달성
    public void UnlockAchievement(string achievementId)
    {
        PlayGamesPlatform.Instance.UnlockAchievement(achievementId, success =>
        {
            if (success)
            {
                Debug.Log($"업적 {achievementId} 해금 성공");
            }
            else
            {
                Debug.Log($"업적 {achievementId} 해금 실패");
            }
        });
    }

    // 업적 UI 표시
    public void ShowAchievementUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
}
