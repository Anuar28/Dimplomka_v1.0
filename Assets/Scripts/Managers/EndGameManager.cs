using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager endManager;
    public bool gameOver;
    public bool possibleWin;

    private PanelController panelController;
    private TextMeshProUGUI scoreTextComponent;
    private PlayerStats player;
    private RewardedAd rewardedAd;

    public int score;
    [HideInInspector]
    public string lvlUnlock = "LevelUnlock";
    private void Awake()
    {
        if (endManager == null)
        {
            endManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);               
    }
        
    void Start()
    {
        
    }
    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreTextComponent.text = "Score: " + score.ToString();
    }

    public void StartResolveSequence()
    {
        StopCoroutine(nameof(ResolveSequence));
        StartCoroutine(ResolveSequence());
    }
    private IEnumerator ResolveSequence() 
    {
        yield return new WaitForSeconds(2.5f);
        ResolveGame();
    }
    public void ResolveGame() 
    {
        if (possibleWin == true && gameOver == false)
        {
            WinGame();
        }
        else if (possibleWin == false && gameOver == true)
        {
            AdLoseGame();
        }
        else if (possibleWin == true && gameOver == true)
        {
            LoseGame();
        }
    }
    // Update is called once per frame
    public void WinGame()
    {
        player.canTakeDmg = false;
        ScoreSet();
        panelController.ActivateWin();
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock, 0))
        {
            PlayerPrefs.SetInt(lvlUnlock, nextLevel);
        }
    }
    public void LoseGame()
    {
        ScoreSet();
        panelController.ActivateLose();
    }
    public void AdLoseGame()
    {
        ScoreSet();
        if (rewardedAd.adNumber > 0)
        {
            rewardedAd.adNumber -= 1;
            panelController.ActivateAdLose();
        }
        else
        {
            panelController.ActivateLose();
        }
    }

    private void ScoreSet()
    {
        PlayerPrefs.SetInt("Score" + SceneManager.GetActiveScene().name, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        if (score > highScore)
            PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name, score);
        //reset the score
        score = 0;
    }
    public void RegisterPanelController(PanelController pC)
    {
        panelController = pC;
    }

    public void RegisterScoreText(TextMeshProUGUI scoreTextComp)
    {
        scoreTextComponent = scoreTextComp;
    }

    public void RegisterPlayerStats(PlayerStats statsPlayer)
    {
        player = statsPlayer;
    }
    public void RegisterRewardedAd(RewardedAd ad)
    {
        rewardedAd = ad;
    }
}
