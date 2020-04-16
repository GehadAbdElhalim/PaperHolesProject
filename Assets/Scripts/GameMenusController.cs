using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameMenusController : MonoBehaviour
{
    public static GameMenusController instance;
    public bool paused;
    public bool isGameOver;

    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject LoadingPanel;
    public GameObject loadingScreen;
    public GameObject TryAgainPanel;
    Vector3 loadingCircleOriginalScale;
    public TextMeshProUGUI Score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        paused = false;
        isGameOver = false;
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        TryAgainPanel.SetActive(false);
        LoadingPanel.SetActive(true);
        loadingCircleOriginalScale = new Vector3(30, 30, 30);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameOver)
            {
                QuitToMainMenu();
                return;
            }

            if (paused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isGameOver)
            {
                Restart();
            }
        }

        if(!paused && !isGameOver)
        {
            GameMaster.instance.timeElapsed += Time.deltaTime;
        }
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        GameOverMenu.SetActive(false);
    }

    public void Restart()
    {
        paused = false;
        isGameOver = false;
        GameMaster.instance.timeElapsed = 0;
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f;
        PauseMenu.SetActive(false);

        Score.text = Mathf.Round(GameMaster.instance.timeElapsed).ToString() + " seconds";

        GameOverMenu.SetActive(true);
    }

    public void ShowTryAgain()
    {
        Time.timeScale = 0.01f;
        Time.fixedDeltaTime = Time.fixedDeltaTime / 100;
        TryAgainPanel.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        paused = false;
        isGameOver = false;
        GameMaster.instance.timeElapsed = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PlayAdAndContinue()
    {
        //Play an Ad here and continue game After it
        ContinueGame();
    }

    public void ContinueGame()
    {

    }

    public void Skip()
    {
        TryAgainPanel.SetActive(false);
        GameOver();
    }

    public void SetLoadingCircleStartSize()
    {
        LoadingPanel.SetActive(true);
        loadingCircleOriginalScale = new Vector3(30, 30, 30);
        loadingScreen.transform.localScale = loadingCircleOriginalScale;
    }

    public IEnumerator ShrinkLoadingCircle(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        loadingScreen.transform.DOScale(1, duration);
        yield return new WaitForSeconds(duration);
        LoadingPanel.SetActive(false);
    }
}
