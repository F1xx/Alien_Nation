using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MenuBase
{
    public TMPro.TMP_Text LivesText = null;
    public TMPro.TMP_Text ScoreText = null;
    public TMPro.TMP_Text HealthText = null;
    public TMPro.TMP_Text TimePassed = null;
    public TMPro.TMP_Text HighScoreText = null;

    public GameObject DeathPanel = null;
    public GameObject PausePanel = null;
    public GameObject SettingsPanel = null;

    static float Score = 0;

    int Lives = 99;
    float PHealth = 100.0f;

    protected override void Awake()
    {
        base.Awake();

        ScoreText.text = "Score: " + Score;
        LivesText.text = "Lives: " + PlayerHealth.Lives;
        Lives = PlayerHealth.Lives;
        PHealth = PlayerHealth.PHealth;
    }

    protected override void Update()
    {
        base.Update();

        UpdateScore();
        UpdateHealth();
        UpdateLives();
        UpdateTime();
    }

    void UpdateLives()
    {
        if (Lives != PlayerHealth.Lives)
        {
            Lives = PlayerHealth.Lives;
            if(Lives < 0)
            {
                ActivateFinalDeath();
                return;
            }

            LivesText.text = "Lives: " + Lives;
        }
    }

    void UpdateHealth()
    {
        if (PHealth != PlayerHealth.PHealth)
        {
            PHealth = PlayerHealth.PHealth;

            HealthText.text = "Health: " + PHealth;
        }
    }

    void UpdateScore()
    {
        if (Lives >= 0)
        {
            Score += Time.deltaTime * 60;
            string txt = "Score: ";
            txt += ((int)Score).ToString();

            ScoreText.text = txt;
        }
    }

    public void Pause()
    {
        PauseManager.Pause();
        OpenPanel(PausePanel);
    }

    public void Unpause()
    {
        ClosePanel(PausePanel);
        ClosePanel(SettingsPanel);
        PauseManager.Unpause();
    }

    void UpdateTime()
    {
        TimePassed.text = WorldTimer.GetTimeAsFormattedString();
    }

    public static void AddScore(float amount)
    {
        Score += amount;
    }

    void ActivateFinalDeath()
    {
        OpenPanel(DeathPanel);

        HighScoreText.text = "HighScore:\n" + ((int)HandleHighScore()).ToString();
    }

    float HandleHighScore()
    {
        float highScore = 0;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");

            //if our current score is higher than the current best
            if(highScore < Score)
            {
                SetScoreAsHighScore();
                highScore = Score;
            }
            else
            {
                HighScoreText.color = Color.white;
            }
        }
        else //if we don't have a high score then this is it
        {
            SetScoreAsHighScore();
            highScore = Score;
        }

        return highScore;
    }

    void SetScoreAsHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", Score);
        PlayerPrefs.Save();
        HighScoreText.color = Color.green;
    }
}
