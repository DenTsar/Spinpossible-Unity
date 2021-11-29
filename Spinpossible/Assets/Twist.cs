using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twist : MonoBehaviour
{
    float t = 0;
    float speed = 1f;
    Vector3 smallerSize = new Vector3(0.75f, 0.75f, 1f);
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        t += Time.deltaTime / speed;
        if (t <= 1)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, smallerSize, t);
        }
        else if (t - 1 <= 1)
        {
            transform.localScale = smallerSize;
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(180, 180, 0), t - 1);
        }
        else if (t - 2 <= 1)
        {
            transform.rotation = Quaternion.Euler(180, 180, 0);
            transform.localScale = Vector3.Lerp(smallerSize, Vector3.one, t - 2);
        }
        else
        {
            transform.localScale = Vector3.one;
            GameObject.Find("BoardController").GetComponent<BoardController>().isRotating = false;
            transform.DetachChildren();
            Destroy(gameObject);
        }
    }
}
