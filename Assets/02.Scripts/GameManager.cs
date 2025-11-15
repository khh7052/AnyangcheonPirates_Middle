using TMPro; // TextMeshPro 사용
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public enum GameState
{
    IDLE,
    PLAY,
    CLEAR,
    OVER
}

public class GameManager : Singleton<GameManager>
{
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText; // TMP_Text를 통한 점수 UI
    [SerializeField] private string leaderboardId; // 리더보드 ID

    [Header("Game State")]
    public UnityEvent OnPlay = new();
    public UnityEvent OnOver = new();
    public UnityEvent OnClear = new();

    private SampleLogin sampleLogin; // SampleLogin 참조

    public static string PreviousSceneName;
    private static string sceneName;
    private static string[] sceneNameSplit;

    public GameState gameState;
    private float magicCount = 4;
    private int enemyCount = 0; // 현재 스테이지 적 수
    private int enemyDeathCount = 0; // 죽은 적 수
    private long currentScore = 0; // 현재 점수

    public long CurrentScore
    {
        get { return currentScore; }
    }

    void Start()
    {
        // Google Play Games 초기화
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        // SampleLogin 컴포넌트 가져오기
        sampleLogin = FindObjectOfType<SampleLogin>();

        // Google Play Games 로그인
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Google Play Games 로그인 성공!");
            }
            else
            {
                Debug.Log("Google Play Games 로그인 실패!");
            }
        });

        UpdateScoreUI(); // 초기 점수 UI 업데이트
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneNameUpdate();

        if (sceneNameSplit[0] == "Stage")
        {
            Play();
        }
        else
        {
            gameState = GameState.IDLE;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        SceneNameUpdate();

        if (sceneNameSplit[0] == "Stage")
        {
            Play();
        }
        else
        {
            gameState = GameState.IDLE;
        }
    }

    void SceneNameUpdate()
    {
        sceneName = SceneManager.GetActiveScene().name;
        sceneNameSplit = StringUtility.SplitString(sceneName);
    }

    public void MagicCountDown(float num)
    {
        if (sceneNameSplit[0] != "Stage") return;

        magicCount -= num;

        if (magicCount <= 0)
        {
            GameOver();
        }
    }

    public void Play()
    {
        Time.timeScale = 1;
        gameState = GameState.PLAY;
        Debug.Log("Play!");

        MagicCount mc = FindObjectOfType<MagicCount>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        SimpleMonster[] monsters = new SimpleMonster[enemies.Length];

        magicCount = mc.initialMagicCount;
        enemyCount = enemies.Length;
        enemyDeathCount = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            monsters[i] = enemies[i].GetComponent<SimpleMonster>();
            monsters[i].OnDie.AddListener(EnemyDeath);
        }

        OnPlay.Invoke();
    }

    public void EnemyDeath()
    {
        if (sceneNameSplit[0] != "Stage") return;

        enemyDeathCount++;

        if (enemyDeathCount >= enemyCount)
        {
            GameClear();
        }
    }

    [ContextMenu("GameClear")]
    public void GameClear()
    {
        if (gameState != GameState.PLAY) return;

        gameState = GameState.CLEAR;

        // 점수 추가
        int stageNumber = GetCurrentStageNumber();
        long scoreToAdd = stageNumber * 100;
        currentScore += scoreToAdd;


        // UI 업데이트
        UpdateScoreUI();

        OnClear.Invoke();
    }
    private void UnlockStageAchievement()
    {
        if (sceneNameSplit[0] == "Stage")
        {
            int stageNumber = GetCurrentStageNumber();

            switch (stageNumber)
            {
                case 1:
                    UnlockAchievement(GPGSIds.achievement_stage1_clear);
                    break;
                case 2:
                    UnlockAchievement(GPGSIds.achievement_stage2_clear);
                    break;
                case 3:
                    UnlockAchievement(GPGSIds.achievement_stage3_clear);
                    break;
                case 4:
                    UnlockAchievement(GPGSIds.achievement_stage4_clear);
                    break;
                case 5:
                    UnlockAchievement(GPGSIds.achievement_stage5_clear);
                    break;
                case 6:
                    UnlockAchievement(GPGSIds.achievement_stage6_clear);
                    break;
                case 7:
                    UnlockAchievement(GPGSIds.achievement_stage7_clear);
                    break;
                case 8:
                    UnlockAchievement(GPGSIds.achievement_stage8_clear);
                    break;
                case 9:
                    UnlockAchievement(GPGSIds.achievement_stage9_clear);
                    break;
                case 10:
                    UnlockAchievement(GPGSIds.achievement_stage10_clear);
                    break;
            }
        }
    }

    private void UnlockAchievement(string achievementId)
    {
        PlayGamesPlatform.Instance.UnlockAchievement(achievementId, success =>
        {
            if (success)
            {
                Debug.Log($"업적 해금 성공: {achievementId}");
            }
            else
            {
                Debug.Log($"업적 해금 실패: {achievementId}");
            }
        });
    }

    private int GetCurrentStageNumber()
    {
        if (sceneNameSplit[0] == "Stage" && int.TryParse(sceneNameSplit[1], out int stageNumber))
        {
            return stageNumber;
        }
        return 0; // 기본값
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"점수: {currentScore}";
        }
    }

    [ContextMenu("GameOver")]
    public void GameOver()
    {
        if (gameState != GameState.PLAY) return;

        gameState = GameState.OVER;
        Debug.Log("GameOver!");

        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlaySFX("Over");
        }

        OnOver.Invoke();
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void NextStage()
    {
        int stage = GetCurrentStageNumber();
        stage++;

        if (stage <= 10)
        {
            LoadScene($"Stage{stage}");
        }
        else
        {
            LoadScene("Lobby");
        }
    }

    public void RePlay()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadScene(string sceneName)
    {
        PreviousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
