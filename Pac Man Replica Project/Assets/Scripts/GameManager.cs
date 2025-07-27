using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameRunning;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private AudioSource backToLobbySound;
    [SerializeField] private AudioSource pauseSound;
    [SerializeField] private AudioSource winningGameSound;
    private int score;
    private int startingScore = 0;
    private int numOfCoin;
    private bool gamePaused;
    private float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;

        UpdateScore(startingScore);
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();

        numOfCoin = GameObject.FindObjectsOfType<CoinController>().Length;

        GameWin();
    }

    private void PauseGame()
    {
        if (gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.P) && !gamePaused)
            {
                pauseSound.PlayOneShot(pauseSound.clip);
                Time.timeScale = 0;
                gamePaused = true;
                pausePanel.SetActive(true);
            }
        }
    }

    public void ResumeGame()
    {
        pauseSound.PlayOneShot(pauseSound.clip);
        Time.timeScale = 1;
        gamePaused = false;
        pausePanel.SetActive(false);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    public void GameWin()
    {
        if (numOfCoin < 1 && gameRunning)
        {
            winningGameSound.PlayOneShot(winningGameSound.clip, 1f);
            gameWinPanel.SetActive(true);
            gameRunning = false;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameRunning = false;
    }

    public void BackToLobby()
    {
        backToLobbySound.PlayOneShot(backToLobbySound.clip, 1f);
        StartCoroutine(DelayBeforeBack());
    }

    IEnumerator DelayBeforeBack()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Lobby");
    }
}
