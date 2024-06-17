using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject[,] cubes;
    private char[,] table;
    private int rows = 0;
    private int cols = 0;
    private int currentRow = 0;
    private int currentCol = 0;

    void Start()
    {
        cubes = new GameObject[3, 3];

        int X = -2;
        int Z = -2;
        int count = 1;

        // Создание массива кубов:
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cubes[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes[i, j].transform.position = new Vector3(X, 0, Z);


                if (count < 3)
                {
                    Z += 2;
                    count++;
                }
                else
                {
                    X += 2;
                    Z = -2;
                    count = 1;
                }
            }       
        }

        LoadTable();
        SetRandomStartPosition();
        UpdateTable();
    }

    void Update()
    {
        HandleInput();
    }

    void SetRandomStartPosition()
    {
        currentRow = Random.Range(0, rows);
        currentCol = Random.Range(0, cols);
    }


    void LoadTable()
    {
        string[] lines = File.ReadAllLines(Path.Combine(Application.dataPath, "Text/TextAboutColor.txt"));

        // Определение размера таблицы с числами:
        rows = lines.Length;
        cols = lines[0].Length;
        table = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                table[i, j] = lines[i][j];
            }
        }
    }

    void UpdateTable()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int row = (currentRow + i) % rows;
                int col = (currentCol + j) % cols;
                SetCubeColor(cubes[i, j], table[row, col]);
            }
        }
    }

    void SetCubeColor(GameObject cube, char id)
    {
        switch (id)
        {
            case '1':
                cube.GetComponent<Renderer>().material.color = Color.red;
                break;
            case '2':
                cube.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case '3':
                cube.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case '4':
                cube.GetComponent<Renderer>().material.color = new Color(0.5f, 0, 0.5f); // Фиолетовый цвет
                break;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) { currentRow = (currentRow - 1 + rows) % rows; UpdateTable(); }
        if (Input.GetKeyDown(KeyCode.S)) { currentRow = (currentRow + 1) % rows; UpdateTable(); }
        if (Input.GetKeyDown(KeyCode.A)) { currentCol = (currentCol - 1 + cols) % cols; UpdateTable(); }
        if (Input.GetKeyDown(KeyCode.D)) { currentCol = (currentCol + 1) % cols; UpdateTable(); }
    }
}
