using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update


    public int score;
    public int highScore;


    public TextMeshProUGUI scoreTPRO;


    public TextMeshProUGUI scoreEndGame;
    public TextMeshProUGUI bestScore;

    public GameObject gameEnded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTPRO != null)
        {
            scoreTPRO.text = score.ToString();
        }

        if (scoreEndGame != null)
        {
            scoreEndGame.text = "Score: " + score.ToString();
        }

     


    }

   public void startAgain ()
    {
        SceneManager.LoadScene("SampleScene");
    }

    
}
