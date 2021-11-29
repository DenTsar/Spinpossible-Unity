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
        dimX = 3;
        dimY = 3;
        board = new Board(dimX, dimY, square);
        size = 25f * 1.5f;

        transform.GetComponent<BoxCollider2D>().size = new Vector2(size / 1.5f * dimX, size / 1.5f * dimY);
    }

    // Update is called once per frame
    void Update() { }
    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        pos1 = getMousePos();
    }
    private void OnMouseUp()
    {
        pos2 = getMousePos();
        if (!isRotating && pos1 != -1)
        {
            int min = Mathf.Max(0, Mathf.Min(pos1, pos2));
            int max = Mathf.Max(pos1, pos2);
            if (min % dimX > max % dimX)
            {
                int col = min % dimX;
                min = min / dimY * dimX + max % dimX;
                max = max / dimY * dimX + col;
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
        return ((dimX - 1) - (int)(point.y / size + dimY / 2f)) * dimX + (int)(point.x / size + dimX / 2f);
    }
}
