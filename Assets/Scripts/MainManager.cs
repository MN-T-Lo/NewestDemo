using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public GameObject Name;
    public Text ScoreText;
    public GameObject GameOverText;
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    private GameObject ScoreField;
    private GameObject HighScoreField;
    private GameObject NameField;
    private GameObject HighScoreNameField;

    // Start is called before the first frame update
    void Start()
    {
        NameField = GameObject.Find("Name");
        NameField.GetComponent<TextMeshProUGUI>().text = "Name : " + DataPersistence.Instance.TheGuy.Name;
        HighScoreNameField = GameObject.Find("HighScoreName");
        HighScoreNameField.GetComponent<TextMeshProUGUI>().text = "Name : " + DataPersistence.Instance.TheGuy.HighScoreName;
        ScoreField = GameObject.Find("Score");
        ScoreField.GetComponent<TextMeshProUGUI>().text = "Score : 0";
        HighScoreField = GameObject.Find("HighScore");
        HighScoreField.GetComponent<TextMeshProUGUI>().text = "High Score : " + DataPersistence.Instance.TheGuy.HighScore;
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
            if (DataPersistence.Instance != null)
            {
                if (m_Points > DataPersistence.Instance.TheGuy.HighScore)
                {
                    DataPersistence.Instance.TheGuy.HighScore = m_Points;
                    DataPersistence.Instance.TheGuy.HighScoreName = DataPersistence.Instance.TheGuy.Name;
                    HighScoreField.GetComponent<TextMeshProUGUI>().text = "High Score : " + DataPersistence.Instance.TheGuy.HighScore;
                    HighScoreNameField.GetComponent<TextMeshProUGUI>().text = "Name : " + DataPersistence.Instance.TheGuy.HighScoreName;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreField.GetComponent<TextMeshProUGUI>().text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
