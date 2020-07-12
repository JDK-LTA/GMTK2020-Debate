using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoresManager : MonoBehaviour
{
    [SerializeField] private Text highScoresText;
    [SerializeField] private Text goToMenu;

    dreamloLeaderBoard dl;
    List<dreamloLeaderBoard.Score> scoreList;

    string playerName = "";

    bool canGoToMenu = false;
    private void Start()
    {
        dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
    }
    private void Update()
    {
        if (canGoToMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }

    }

    enum gameState
    {
        enterscore,
        leaderboard
    };
    gameState gs = gameState.enterscore;

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    private void OnGUI()
    {
        //GUI.backgroundColor = Color.white;
        var width200 = new GUILayoutOption[] { GUILayout.Width(300), GUILayout.Height(30) };
        var guiStyle = new GUIStyle();
        guiStyle.fontSize = 30;
        guiStyle.alignment = TextAnchor.MiddleCenter;
        var guiStyle2 = new GUIStyle();
        guiStyle2.fontSize = 36;
        guiStyle2.fontStyle = FontStyle.Bold;
        var width = 700;  // Make this wider to add more columns
        var height = 500;
        GUIStyle box = new GUIStyle(GUI.skin.box);
        box.normal.background = MakeTex(2, 2, new Color(1, 1f, 1, 0.5f));
        var r = new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height);
        GUILayout.BeginArea(r, box);

        GUILayout.BeginVertical();

        if (gs == gameState.enterscore)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.fontSize = 30;
            GUILayout.Label("Total Score: " + ((int)Player.Instance.Points).ToString(), guiStyle2);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Your Name: ", guiStyle2);
            this.playerName = GUILayout.TextField(this.playerName, width200);

            if (GUILayout.Button("Save Score", style))
            {
                // add the score...
                if (dl.publicCode == "") Debug.LogError("You forgot to set the publicCode variable");
                if (dl.privateCode == "") Debug.LogError("You forgot to set the privateCode variable");

                dl.AddScore(this.playerName, (int)Player.Instance.Points);
                gs = gameState.leaderboard;
            }
            GUILayout.EndHorizontal();
        }
        if (gs == gameState.leaderboard)
        {
            GUILayout.Label("High Scores:", guiStyle2);
            List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();

            if (scoreList == null)
            {
                GUILayout.Label("(loading...)", guiStyle);
            }
            else
            {
                int maxToDisplay = 20;
                int count = 0;
                foreach (dreamloLeaderBoard.Score currentScore in scoreList)
                {
                    count++;
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(currentScore.playerName, guiStyle, width200);
                    GUILayout.Label(currentScore.score.ToString(), guiStyle, width200);
                    GUILayout.EndHorizontal();

                    if (count >= maxToDisplay) break;
                }
                canGoToMenu = true;
                goToMenu.gameObject.SetActive(true);
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
