using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public (GameObject, bool)[,] board;
    int size = 25;



    public Board(int dimX, int dimY, GameObject square)
    {
        board = new (GameObject, bool)[dimY, dimX];

        for (int i = 0; i < dimY; i++)
        {
            for (int j = 0; j < dimX; j++)
            {
                GameObject block = GameObject.Instantiate(square, new Vector2(j - dimX / 2f + 0.5f, -i + dimY / 2f - 0.5f) * size, Quaternion.identity);//, GameObject.Find("BoardController").transform);
                TextMesh text = block.GetComponent<TextMesh>();
                text.text = i * dimX + j + 1 + "";
                text.anchor = TextAnchor.MiddleCenter;

                board[i, j] = (block, true);
            }
        }
    }

    public void Rotate(int topLeft, int botRight)
    {
        Debug.Log("Rotating: " + topLeft + " " + botRight);

        Transform twistser = new GameObject("Temp-Twister", typeof(Twist)).transform;
        int x = board.GetLength(1);
        int y = board.GetLength(0);
        float xPos = (topLeft % x + botRight % x) / 2f - (x - 1) / 2f;
        float yPos = -(topLeft / x + botRight / x) / 2f + (y - 1) / 2f;
        twistser.position = new Vector2(xPos, yPos) * size;

        for (int i = topLeft / x; i <= botRight / x; i++)
            for (int j = topLeft % x; j <= botRight % x; j++)
            {
                int pos = topLeft + botRight - (i * x + j);
                if (pos >= i * x + j)
                {
                    var pair = board[pos / x, pos % x];
                    pair.Item1.transform.parent = twistser.transform;
                    board[i, j].Item1.transform.parent = twistser.transform;
                    Debug.Log("Flipping: " + board[i, j].Item1.GetComponent<TextMesh>().text + " " + pair.Item1.GetComponent<TextMesh>().text);
                    board[i, j].Item2 = !board[i, j].Item2;
                    pair.Item2 = !pair.Item2;
                    board[pos / x, pos % x] = board[i, j];
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
