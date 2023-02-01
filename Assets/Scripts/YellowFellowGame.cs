using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YellowFellowGame : MonoBehaviour
{
    public AudioClip startClip;
    public AudioClip diedClip;
    public int countdownTime;
    public Text countdownDisplay;
    public Text remainText;
    public Text eatenText;
    public Text scoreText;
    public Text lifeText;

    [SerializeField]
    Fellow playerObject;

    GameObject[] pellets;

    [SerializeField]
    Ghost ghostOject;

    [SerializeField]
    Ghostone ghostOneOject;

    [SerializeField]
    Ghostthree ghostThreeOject;

    [SerializeField]
    GameObject highScoreUI;

    [SerializeField]
    GameObject mainMenuUI;

    [SerializeField]
    GameObject gameUI;

    [SerializeField]
    GameObject winUI;

    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    GameObject countDownUI;




    enum GameMode
    {
        InGame,
        MainMenu,
        HighScores,
    }

    GameMode gameMode = GameMode.MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        pellets = GameObject.FindGameObjectsWithTag("Pellet");

        SetGameState(false);
        StartMainMenu();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.PelletsEaten() == pellets.Length)
        {
            winUI.SetActive(true);
            //AudioSource.PlayClipAtPoint(startClip, Vector3.zero);
            StopAllCoroutines();
            SetGameState(false);
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReStart();
            }

        }
        if (playerObject.LifeCounter() == 0)
        {
            gameOverUI.SetActive(true);
            playerObject.gameObject.SetActive(false);
            //AudioSource.PlayClipAtPoint(diedClip, Vector3.zero);
            StopAllCoroutines();
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReStart();
            }

        }
        switch (gameMode)
        {
            case GameMode.MainMenu: UpdateMainMenu(); break;
            case GameMode.HighScores: UpdateHighScores(); break;
            case GameMode.InGame: UpdateMainGame(); break;
            
        }
    }


    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

        SetGameState(true);
        countDownUI.SetActive(false);


    }

    void SetGameState(bool state)
    {
        playerObject.gameObject.SetActive(state);
        ghostOject.gameObject.SetActive(state);
        ghostOneOject.gameObject.SetActive(state);
        ghostThreeOject.gameObject.SetActive(state);
    }



    void UpdateMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            countDownUI.SetActive(true);
            StartCoroutine(CountdownToStart());
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SetGameState(false);
            StartHighScores();
        }
    }

    void UpdateHighScores()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartMainMenu();
        }
    }

    void UpdateMainGame()
    {
        remainText.text = "Remain:\n" + (pellets.Length - playerObject.PelletsEaten());
        eatenText.text = "Eaten:\n" + playerObject.PelletsEaten();
        scoreText.text = "Score:\n" + playerObject.Score();
        lifeText.text = "Life:\n" + playerObject.LifeCounter();
    }


    void StartMainMenu()
    {
        gameMode = GameMode.MainMenu;
        mainMenuUI.gameObject.SetActive(true);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
    }


    void StartHighScores()
    {
        gameMode = GameMode.HighScores;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }

    void StartGame()
    {
        gameMode = GameMode.InGame;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    void ReStart()
    {
        SceneManager.LoadScene(0);
    }

}

