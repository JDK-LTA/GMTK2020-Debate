using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighScoresManager : MonoBehaviour
{
    [SerializeField] private InputField nameField;
    [SerializeField] private Text highScoresText;
    [SerializeField] private Text goToMenu;

    [SerializeField] private int maxNOfHighScoresShown = 10;

    dreamloLeaderBoard dl;

    bool canGoToMenu = false;
    private void Start()
    {
        dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        ShowLeaderBoard();
    }
    private void Update()
    {
        if (canGoToMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //GO TO MENU
            }
        }
    }
    private /*IEnumerator */ void ShowLeaderBoard()
    {
        List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();
        
        //yield return new WaitUntil(() => scoreList.Count >= 0);

        Debug.Log("Show");
        highScoresText.text = "";

        if (scoreList.Count == 0)
        {
            highScoresText.text = "No score has been registered yet.";
        }
        else
        {
            for (int i = 0; i < scoreList.Count; i++)
            {
                highScoresText.text += scoreList[i].playerName + " - " + scoreList[i].score + "\n";
                if (i >= maxNOfHighScoresShown - 1)
                {
                    break;
                }
            }
        }
    }

    public void EnterHighScore(string name)
    {
        dl.AddScore(name, (int)Player.Instance.Points);
        ShowLeaderBoard();
        canGoToMenu = true;
        goToMenu.gameObject.SetActive(true);
    }
}
