using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject square;
    Board board;
    int pos1;
    int pos2;
    float size;
    int dimX, dimY;
    public bool isRotating = false;
    // Start is called before the first frame update
    void Start()
    {
        dimX = 2;
        dimY = 4;
        board = new Board(dimX, dimY, square);
        size = 25f * 1.5f;

        transform.GetComponent<BoxCollider2D>().size = new Vector2(dimX, dimY) * size / 1.5f;
    }

    // Update is called once per frame
    void Update() { }
    private void OnMouseDown()
    {
        Debug.Log("Clicked" + getMousePos());
        pos1 = getMousePos();
    }
    private void OnMouseUp()
    {
        pos2 = getMousePos();
        if (!isRotating && pos1 != -1)
        {
            int min = Mathf.Max(0, Mathf.Min(pos1, pos2));//thi currently lets you drag beyond the border and the spin to count
            int max = Mathf.Max(pos1, pos2);
            if (min % dimX > max % dimX)
            {
                int col = min % dimX;
                min = min / dimX * dimX + max % dimX;
                max = max / dimX * dimX + col;
            }
            Debug.Log("Swapping " + min + " " + max);
            board.Rotate(min, max);
            isRotating = true;
            pos1 = -1;
            pos2 = -1;
        }
    }

    int getMousePos()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float m = size / 1.5f;
        int x = (int)((point.x + (dimX / 2f) * m) / m);
        int y = (int)(dimY - (point.y + (dimY / 2f) * m) / m);
        return x + y * dimX;
    }
}
