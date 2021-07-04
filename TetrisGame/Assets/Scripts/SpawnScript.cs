using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] tetramino; // ������ ���� ���������
    GameObject activeTetramino; // ���������, ��������� �� ������� ����

    [SerializeField]
    GameObject nextTetramino; // ���������, ��������� �� ������� ����
    bool flag = true;

    void Start()
    {
        ShowNewTetramino();
    }
    /// <summary>
    /// ������� � ��������� ���������
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
    /// ����� ���������� � ���������
    /// </summary>
    public void Reset()
    {
        flag = true;
        Destroy(activeTetramino);
        Destroy(nextTetramino);
    }

}
