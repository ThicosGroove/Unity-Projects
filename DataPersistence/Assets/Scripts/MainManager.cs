using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text currentPlayerText;
    public Text BestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    //two var that holds the static info
    private string BestPlayer;
    private string CurrentPlayer;
    private int HighScore;


    private void Awake()
    {
        LoadScore();    
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        CurrentPlayer = PlayerDataHandler.Instance.PlayerName;

        AddCurrentNameCanvas();
        SetBestPlayer();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }     
    }

    void AddPoint(int point)
    {
        m_Points += point;
        PlayerDataHandler.Instance.Score = m_Points;
        ScoreText.text = $"Score : {m_Points}";

        CheckBestPlayer();
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void AddCurrentNameCanvas()
    {
        currentPlayerText.text = $"Name: {CurrentPlayer}";
    }

    void CheckBestPlayer()
    {
        int currentScore = PlayerDataHandler.Instance.Score;
        if (currentScore > HighScore)
        {
            HighScore = currentScore;
            BestPlayer = CurrentPlayer;

            PlayerDataHandler.Instance.BestPlayerName = BestPlayer;
            PlayerDataHandler.Instance.BestPlayerScore = HighScore;
           
            BestScoreText.text = $"Best Score: {BestPlayer} : {HighScore}";

            SaveScore(BestPlayer, HighScore);
        }
    }

    private void SetBestPlayer()
    {
        if (BestPlayer == null && HighScore == 0)
        {
            BestScoreText.text = "";
        }
        else
        {
            BestScoreText.text = $"Best Score: {BestPlayer} : {HighScore}";
        }
    }

    [System.Serializable]
    public class SaveHighScore
    {
        public int HighScore;
        public string BestPlayer;
    }

    public void SaveScore(string bestPlayerName, int highScore)
    {
        SaveHighScore data = new SaveHighScore();

        data.BestPlayer = bestPlayerName;
        data.HighScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/saveData.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveHighScore data = JsonUtility.FromJson<SaveHighScore>(json);

            BestPlayer = data.BestPlayer;
            HighScore = data.HighScore;
        }
    }
}
