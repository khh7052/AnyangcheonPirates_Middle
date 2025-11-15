using GooglePlayGames;
using UnityEngine;
using TMPro;

public class SampleLogin : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;
    [SerializeField] GameObject loginButton;
    [SerializeField] private string leaderboardId;

    private AchievementManager achievementManager;
    private GameManager gameManager;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        achievementManager = FindObjectOfType<AchievementManager>();
        gameManager = FindObjectOfType<GameManager>(); // GameManager 연결

        AutoLogin();
    }

    private void AutoLogin()
    {
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                resultText.text = $"{Social.localUser.userName}님 환영합니다.\nID : {Social.localUser.id}";
                loginButton.SetActive(false);
            }
            else
            {
                resultText.text = "자동 로그인 실패";
                loginButton.SetActive(true);
            }
        });
    }

    public void Login()
    {
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                resultText.text = $"{Social.localUser.userName}님 환영합니다.\nID : {Social.localUser.id}";
                loginButton.SetActive(false);
            }
            else
            {
                resultText.text = "로그인 실패";
                loginButton.SetActive(true);
            }
        });
    }

    public void Logout()
    {
        var pgp = (PlayGamesPlatform)Social.Active;
        pgp.SignOut();
        resultText.text = "로그아웃 성공";
        loginButton.SetActive(true);
    }

    // 리더보드 UI 표시
    public void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    // 리더보드에 점수 등록
    public void SubmitScoreToLeaderboard()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다.");
            return;
        }

        long currentScore = gameManager.CurrentScore; // GameManager에서 점수 가져오기

        PlayGamesPlatform.Instance.ReportScore(currentScore, leaderboardId, success =>
        {
            if (success)
            {
                Debug.Log($"리더보드 {leaderboardId}에 점수 {currentScore} 등록 성공");
            }
            else
            {
                Debug.Log($"리더보드 {leaderboardId}에 점수 등록 실패");
            }
        });
    }

    // 업적 UI 호출
    public void ShowAchievementUI()
    {
        achievementManager.ShowAchievementUI();
    }

    // 특정 업적 해금
    public void IncrementGPGSAchievement()
    {
        achievementManager.UnlockAchievement(GPGSIds.achievement_test);
    }
}
