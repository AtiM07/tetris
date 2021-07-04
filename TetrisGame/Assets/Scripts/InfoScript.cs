using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text speedText;

    [SerializeField]
    Text linesText;

    [SerializeField]
    GameObject panelGameOver;

    /// <summary>
    /// ќбновление тестовой строки с количеством очков
    /// </summary>
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: \n" + score.ToString();
    }

    /// <summary>
    /// ќбновление тестовой строки со скоростью
    /// </summary>
    public void UpdateSpeedText(int speed)
    {
        speedText.text = "Speed: \n" + speed.ToString();
    }

    /// <summary>
    /// ќбновление тестовой строки с количеством разрушенных линий
    /// </summary>
    public void UpdateLinesText(int line)
    {
        linesText.text = "Lines: \n" + line.ToString();
    }

    public void GameOver(bool i)
    {
        if (panelGameOver.activeSelf != i)
        { 
            panelGameOver.SetActive(i);
            System.Threading.Thread.Sleep(1000);
        }
    }
}
