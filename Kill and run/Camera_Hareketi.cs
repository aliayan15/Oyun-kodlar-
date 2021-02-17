using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Hareketi : MonoBehaviour
{

    [SerializeField] float Hız;

    BoxCollider2D col1;
    [SerializeField] GameObject aa;


    float Hızlanma;
    float Maxhız;
    bool Hareket = true;

    // Start is called before the first frame update
    void Start()
    {
        Hız = 0.4f;
        Hızlanma = 0.05f;
        Maxhız = 2f;
        col1 = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D col2 = aa.GetComponent<BoxCollider2D>();
        float camDeger= Camera.main.orthographicSize * Camera.main.aspect;
        col1.offset = new Vector2(-camDeger-0.5f, col1.offset.y);
        aa.transform.position = new Vector2(camDeger+0.5f, aa.transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        if (Hareket)
        {
            CameraYürüt();
        }
        
    }
    void CameraYürüt()
    {
        transform.position += transform.right * Hız * Time.deltaTime;
        Hız += Hızlanma * Time.deltaTime;
        if (Hız > Maxhız)
        {
            Hız = Maxhız;
        }
    }
}
