using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    private static GameManager m_instance;

    private int score = 0;
    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            //UIManager.instance.UpdateScoreText(score);
        }
    }

    // 게임 오버 처리
    public void EndGame()
    {
        isGameover = true;
        //UIManager.instance.SetActiveGameoverUI(true);
    }
}