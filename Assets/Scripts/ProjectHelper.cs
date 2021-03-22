using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProjectHelper : MonoBehaviour
{
    [SerializeField]
    
    public Text scoreText, bombText, playerScoreText,highScoreText;
    public GameObject gameoverMenu;
    
    public List<ProjectPipeHelper> pipes;
    
    [HideInInspector]
    public List<GameObject> pipesAccepted;
    public int score,bomb,highScore;
    
    
    void Start()
    {
        
        if (PlayerPrefs.HasKey("SaveHighScore"))
        {
            highScore = PlayerPrefs.GetInt("SaveHighScore");
        }
        pipesAccepted = new List<GameObject>();
        StartCoroutine(SpawnPipes());
        
    }

    
    void Update()
    {
        scoreText.text = ""+ score;
        bombText.text = "Bombs:" + bomb;
        playerScoreText.text = "Your score:" + score;
        highScoreText.text = "Highscore:" + highScore;
        Highscore();
    }
    IEnumerator SpawnPipes()
    {
        Vector2 position;
        while (true)
        {
            position = transform.position;

            TryAcceptPipe(score);

            Instantiate(ChoosePipe(), position, Quaternion.identity);
            
            yield return new WaitForSeconds(2.5F);
        }
    }
    public void Highscore()
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("SaveHighScore", highScore);
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }


    private void TryAcceptPipe(int score)
    {
        foreach (var item in pipes)
        {
            if(item.itemScore <= score && !pipesAccepted.Contains(item.item))
            {
                pipesAccepted.Add(item.item);
            }
        }
    }

    private GameObject ChoosePipe()
    {
        var random = new System.Random();
        int index = random.Next(pipesAccepted.Count);
        var choosenPipe = pipesAccepted[index];

        return choosenPipe;
    }

}

[System.Serializable]
public class ProjectPipeHelper
{
    public GameObject item;
    public int itemScore;
}