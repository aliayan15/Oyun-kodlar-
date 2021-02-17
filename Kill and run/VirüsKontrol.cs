using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirüsKontrol : MonoBehaviour
{

    float can;
    OyunKontrol oyunKontrol;
    [SerializeField] GameObject patlamaPrepab;
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        can = 80;
        oyunKontrol = FindObjectOfType<OyunKontrol>().GetComponent<OyunKontrol>();
        scale = transform.localScale.x;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("mermi"))
        {
            Destroy(col.gameObject);
            can -= 20;
            if (can <= 0)
            {
                GameObject patlama = Instantiate(patlamaPrepab, transform.position, Quaternion.identity);
                patlama.transform.localScale = new Vector2(scale + 0.2f, scale + 0.2f);
                Destroy(gameObject);
                Destroy(patlama, 0.6f);
                oyunKontrol.Puan += 50;
            }
        }
    }

}
