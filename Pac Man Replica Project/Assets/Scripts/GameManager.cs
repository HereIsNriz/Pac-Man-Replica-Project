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
    [SerializeField] private AudioSource backToLobbySound;
    private int score;
    private int startingScore = 0;
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
    }

    private void PauseGame()
    {
        if (gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.P) && !gamePaused)
            {
                Time.timeScale = 0;
                gamePaused = true;
                pausePanel.SetActive(true);
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        pausePanel.SetActive(false);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
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
