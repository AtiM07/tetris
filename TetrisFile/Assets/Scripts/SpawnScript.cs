using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] tetramino; // Массив всех тетрамино
    GameObject activeTetramino; // Тетрамино, выходящее на игровое поле

    [SerializeField]
    GameObject nextTetramino; // Тетрамино, выходящее на игровое поле
    bool flag = true;

    void Start()
    {
        ShowNewTetramino();
    }
    /// <summary>
    /// Текущее и следующее тетрамино
    /// </summary>
    public void ShowNewTetramino()
    {
        if (flag)
        {
            activeTetramino = Instantiate(tetramino[Random.Range(0, tetramino.Length)], transform.position, Quaternion.identity);
            flag = false;
            nextTetramino = Instantiate(tetramino[Random.Range(0, tetramino.Length)], nextTetramino.transform.position, Quaternion.identity);
            nextTetramino.GetComponent<TetrisScript>().enabled = false;
        }
        else
        {
            activeTetramino = Instantiate(nextTetramino, transform.position, Quaternion.identity);
            activeTetramino.GetComponent<TetrisScript>().enabled = true;
            Destroy(nextTetramino);
            nextTetramino = Instantiate(tetramino[Random.Range(0, tetramino.Length)], nextTetramino.transform.position, Quaternion.identity);
            nextTetramino.GetComponent<TetrisScript>().enabled = false;
        }
    }
    /// <summary>
    /// Сброс информации о тетрамино
    /// </summary>
    public void Reset()
    {
        flag = true;
        Destroy(activeTetramino);
        Destroy(nextTetramino);
    }

}
