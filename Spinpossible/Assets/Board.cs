using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public (GameObject, bool)[,] board;
    int size = 25;



    public Board(int dimX, int dimY, GameObject square)
    {
        board = new (GameObject, bool)[dimX, dimY];

        for (int i = 0; i < dimY; i++)
        {
            for (int j = 0; j < dimX; j++)
            {
                GameObject block = GameObject.Instantiate(square, new Vector2(j - dimX / 2f + 0.5f, -i + dimY / 2f - 0.5f) * size, Quaternion.identity);//, GameObject.Find("BoardController").transform);
                TextMesh text = block.GetComponent<TextMesh>();
                text.text = i * 3 + j + 1 + "";
                text.anchor = TextAnchor.MiddleCenter;

                board[i, j] = (block, true);
            }
        }
    }

    public void Rotate(int topLeft, int botRight)
    {
        Debug.Log("Rotating: " + topLeft + " " + botRight);

        Transform twistser = new GameObject("Temp-Twister", typeof(Twist)).transform;
        int x = board.GetLength(0);
        int y = board.GetLength(1);
        float xPos = (topLeft % 3 + botRight % 3) / 2f - (int)(3 / 2f);
        float yPos = -(topLeft / 3 + botRight / 3) / 2f + (int)(3 / 2f);
        twistser.position = new Vector2(xPos, yPos) * size;

        for (int i = topLeft / y; i <= botRight / y; i++)
            for (int j = topLeft % x; j <= botRight % x; j++)
            {
                int pos = topLeft + botRight - (i * x + j);
                if (pos >= i * x + j)
                {
                    var pair = board[pos / 3, pos % 3];
                    pair.Item1.transform.parent = twistser.transform;
                    board[i, j].Item1.transform.parent = twistser.transform;
                    Debug.Log("Flipping: " + board[i, j].Item1.GetComponent<TextMesh>().text + " " + pair.Item1.GetComponent<TextMesh>().text);
                    board[i, j].Item2 = !board[i, j].Item2;
                    pair.Item2 = !pair.Item2;
                    board[pos / 3, pos % 3] = board[i, j];
                    board[i, j] = pair;
                }
            }
        // for (int i = 0; i < board.GetLength(0); i++)
        // {
        //     for (int j = 0; j < board.GetLength(1); j++)
        //     {
        //         Debug.Log(board[i, j].Item1.GetComponent<TextMesh>().text + ' ' + board[i, j].Item2);
        //     }
        // }
    }
}
