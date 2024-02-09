using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
    {
    public List<GameObject> burnableTiles = new List<GameObject>();
    float totalTiles;

    #region Timer Variables
    [SerializeField]
    float totalTime = 90f;
    float timeRemaining;
    private bool isTimerActive = false;
    public bool IsTimerActive
        {
        get { return isTimerActive; }
        set
            {
            isTimerActive = value;
            if (isTimerActive) timerTxt.color = new Color(255, 0, 0, 255);
            }
        }
    TMP_Text timerTxt;
    #endregion
    #region Pause Variables
    [SerializeField]
    GameObject pauseMenu; //grabbing it this way is probably bad and should be done through code but >time xdd
    public bool isPaused = false;
    #endregion
    #region Game Over Variables
    [SerializeField]
    GameObject gameOverScreen;
    bool gameOver = false;
    #endregion
    #region Game Won Variables
    [SerializeField]
    GameObject gameWonScreen;
    bool gameWon = false;
    public float toolsUsed = 0;
    #endregion

    TMP_Text tilesBurntTxt;

    GameObject player;

    AudioSource audioSource;

    //SCRIPT FUNCTIONALITY/PURPOSE
    //Handles all the underlying tracking/systems to allow the game to be playable
    //includes scoring/timers etc
    //also handles most UI Functionality

    //this should probably be split into different scripts handling each function seperately
    //but im time limited so this is how it will stay xdd
    public static GameController instance;
    void Start()
        {
        instance = this;
        burnableTiles = GameObject.FindGameObjectsWithTag("BurnableTile").ToList<GameObject>();
        totalTiles = burnableTiles.Count;
        Debug.Log("Total of " + burnableTiles.Count + " Burnable Tiles in this level");

        timerTxt = GameObject.Find("TextTimer").GetComponent<TMP_Text>();
        tilesBurntTxt = GameObject.Find("TextTilesBurnt").GetComponent<TMP_Text>();
        timeRemaining = totalTime;

        //update timer display once
        float minutes = (int)(timeRemaining / 60);
        float seconds = (float)Math.Round(timeRemaining % 60, 3);
        timerTxt.text = minutes + ":" + seconds;

        player = GameObject.FindGameObjectWithTag("Player");

        audioSource = gameObject.GetComponent<AudioSource>();

        gameOver = false;
        gameWon = false;
        Time.timeScale = 1;

        UIUpdateRemainingTiles();
        UIUpdateTimer();
        }

    void Update()
        {
        //UI Stuff
        //I should separate these functions into UI and Game functions but >time
        //>time is becoming a running theme here xdd
        UIUpdateRemainingTiles();
        UIUpdateTimer();
        UITogglePauseMenu();
        }

    void UIUpdateRemainingTiles()
        {
        tilesBurntTxt.text = "Tiles Burnt:\n" + (totalTiles - burnableTiles.Count) + "/" + totalTiles;

        if (burnableTiles.Count == 0) GameWon();
        }
    void UIUpdateTimer()
        {
        if (!IsTimerActive) return; //early return if the timer isnt active (only active once tripped by a fire alarm)
        if (timeRemaining > 0) timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0) timeRemaining = 0;

        float minutes = (int)(timeRemaining / 60);
        float seconds = (float)Math.Round(timeRemaining % 60, 3);

        timerTxt.text = minutes + ":" + seconds;

        if (timeRemaining <= 0)
            {
            gameOver = true;
            isPaused = true;
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            }
        }
    public void UITogglePauseMenu()
        {
        if((Input.GetKeyDown(KeyCode.P)| Input.GetKeyDown(KeyCode.Escape)) && !gameOver && !gameWon)
            {
            isPaused = !isPaused;
            if(isPaused)
                {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                }
            else
                {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                }
            }
        }
    public void GameWon()
        {
        Time.timeScale = 0;
        gameWon = true;
        gameWonScreen.SetActive(true);
        TMP_Text scoreText = GameObject.Find("ScoreSheet").GetComponent<TMP_Text>();
        scoreText.text = GenerateScoreSheet();
        }

    public string GenerateScoreSheet()
        {
        string scoreSheet;
        float timeBonus = (float)Math.Round((timeRemaining / totalTime) * 10000, 0);
        float tilePoints = totalTiles * 100;
        float toolBonus = 10000 - (toolsUsed * 1000);
        if (toolBonus < 0) toolBonus = 0;
        float totalScore = timeBonus + tilePoints + toolBonus;
        scoreSheet =
            "Time Bonus: " + timeBonus + "\n" +
            "Tile Bonus: " + tilePoints + "\n" +
            "Minimal Tool Use: " + toolBonus + "\n" +
            "Total Score: <color=\"green\">" + totalScore + "Points";
        return scoreSheet;
        }
    }
