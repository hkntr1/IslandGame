using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    public GameObject scoreText,finishMenu,gameoverMenu;
    public int theScore;
   
    int currentLevel;
     public void Start()
    {
       currentLevel = PlayerPrefs.GetInt("currentLevel");
    }
    public void ScoreEnemy()
    {
        theScore += 50;
        scoreText.GetComponent<Text>().text = theScore.ToString();
    }
    public void ScoreCoin()
    {
        theScore += 250;
        scoreText.GetComponent<Text>().text = theScore.ToString();
    }
    private void Update()
    {
        if (theScore >= 400)
        {     
            finishMenu.SetActive(true);
        }
        
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Yeni Bölüme Geçildi");
        
    }
    public void GameOver()
    {
        gameoverMenu.SetActive(true);
        

    }
    public void RestartScene()
    {
        SceneManager.LoadScene("Level" + currentLevel);
        theScore = 0;
        scoreText.GetComponent<Text>().text = theScore.ToString();
    }
}

