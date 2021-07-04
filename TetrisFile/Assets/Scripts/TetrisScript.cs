using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisScript : MonoBehaviour
{

    [SerializeField]
    float fallTime = 0.8f;
    static int height = 20;
    static int width = 11;
    float previousTime;
    static Transform[,] desk = new Transform[width, height];
    bool gameOver = false;
    static int speed = 1;
    static int score = 0;
    static int line = 0;
    static int preScore = 0;
    void Update()
    {
        UserControl();
        FindObjectOfType<InfoScript>().GameOver(gameOver);
        FindObjectOfType<InfoScript>().UpdateScoreText(score);
        FindObjectOfType<InfoScript>().UpdateSpeedText(speed);
        FindObjectOfType<InfoScript>().UpdateLinesText(line);
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? fallTime / (10 * speed) : fallTime / speed))
        {

            transform.position += new Vector3(0, -1, 0);
            if (!CheckMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                if (!gameOver)
                {
                    AddToDesk();
                    this.enabled = false;
                    CheckLine();
                    FindObjectOfType<SpawnScript>().ShowNewTetramino();
                }
                else
                {
                    this.enabled = false;
                    RestartGame();
                    
                }
            }
            previousTime = Time.time;
        }
    }

    /// <summary>
    /// ”правление пользователем тетрамино (смещение (вправо, влево, вниз) и поворот), а также выход из игры
    /// </summary>
    void UserControl()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!CheckMove())
                transform.position -= new Vector3(-1, 0, 0);

            System.Threading.Thread.Sleep(100);
        }
        else
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!CheckMove())
                    transform.position -= new Vector3(1, 0, 0);
                System.Threading.Thread.Sleep(100);
            }
            
        else
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(0, 0, 90);
                if (!CheckMove())
                    transform.Rotate(0, 0, -90);
            }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus) && speed < 10)
        {
            speed++;
            FindObjectOfType<InfoScript>().UpdateSpeedText(speed);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) && speed > 1)
        {
            speed--;
            FindObjectOfType<InfoScript>().UpdateSpeedText(speed);
        }
    }

    /// <summary>
    /// ѕроверка нахождени€ тетрамино в допустимых границах пол€
    /// </summary>
    /// <returns></returns>
    bool CheckMove()
    {
        foreach (Transform mino in transform)
        {
            int x = (int)mino.transform.position.x;
            int y = (int)mino.transform.position.y;

            if (x <= 0 || x >= width || y <= 0 || y >= height)
                return false;

            if (desk[x, y] != null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// ƒобавление тетрамино в общую таблицу
    /// </summary>
    void AddToDesk()
    {
        foreach (Transform mino in transform)
        {
            int x = (int) mino.transform.position.x;
            int y = (int) mino.transform.position.y;
            

            if (desk[x, y] != null)
            {
                gameOver = true;
            }
            else
            {
                desk[x, y] = mino;
            }
        }
    }

    /// <summary>
    /// ѕроверка линий
    /// </summary>
    void CheckLine()
    {
        for (int i = height-1; i > 0; i--)
        {
            if (CheckFillLine(i))
            {
                DeleteLine(i);
                DownLine(i);
                line++;


                if (line != 0)
                {
                    FindObjectOfType<InfoScript>().UpdateLinesText(line);
                    UpdateScore(line);
                    CheckScoreSpeed();
                }
            }
        }
    }

    /// <summary>
    /// ѕоиск заполненных линий на поле
    /// </summary>
    bool CheckFillLine(int i)
    {
        for(int j=1; j<width; j++)
        {
            if (desk[j, i] == null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// ”даление заполненных линий на поле
    /// </summary>
    void DeleteLine(int i)
    {
        for (int j = 1; j <width; j++)
        {
            if (desk[j,i] != null)
                Destroy(desk[j, i].gameObject);
            desk[j, i] = null;
        }
    }

    /// <summary>
    /// —мещение вниз вышесто€щих над заполненными лини€ми фигур
    /// </summary>
    void DownLine(int i)
    {
        for (int hgt = i; hgt < height; hgt++)
        {
            for (int wdh = 0; wdh < width; wdh++)
            {
                if (desk[wdh, hgt] != null)
                {
                    desk[wdh, hgt - 1] = desk[wdh, hgt];
                    desk[wdh, hgt] = null;
                    desk[wdh, hgt - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    /// <summary>
    /// ѕерезапуск игры
    /// </summary>
    void RestartGame()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            DeleteLine(i);
        }
        speed = 1;
        score = 0;
        line = 0;
        //gameOver = false;
        FindObjectOfType<InfoScript>().UpdateScoreText(score);
        FindObjectOfType<InfoScript>().UpdateSpeedText(speed);
        FindObjectOfType<InfoScript>().UpdateLinesText(line);

        FindObjectOfType<SpawnScript>().Reset();
        FindObjectOfType<SpawnScript>().ShowNewTetramino();



    }

    /// <summary>
    /// ”величение скорости в зависимости от набранных очков
    /// </summary>
    void CheckScoreSpeed()
    {
        if (score / 1000 != preScore)
            if (score / 1000 != 0 && speed < 10)
            {
                speed++; 
                preScore += 1;
            }

    }

    /// <summary>
    /// ”величение очков взависимости от количества разрушенных линий
    /// </summary>
    void UpdateScore(int line)
    {
        switch (line)
        {
            case 1:
                score += 100;
                break;
            case 2:
                score += 300;
                break;
            case 3:
                score += 700;
                break;
            case 4:
                score += 1500;
                break;
        }
        FindObjectOfType<InfoScript>().UpdateScoreText(score);
    }
}
