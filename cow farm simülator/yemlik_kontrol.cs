using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yemlik_kontrol : MonoBehaviour {
    public float saman;
    public float yem;
    private float yemekMiktarı;
    private float y_pozizyonu;
    private float alınanSamanDegeri;
    [SerializeField] GameObject zemin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("saman"))
        {
            alınanSamanDegeri = other.gameObject.GetComponent<saman>().deger;
            samanAl();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("yem"))
        {
            yem += 10;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        
        yemekMiktarı = saman + yem;
        if (yemekMiktarı < 9)
        {
            y_pozizyonu = -23.9f;
        }else if (yemekMiktarı > 9 && yemekMiktarı < 30)
        {
            y_pozizyonu = -22.5f;
        }else if (yemekMiktarı > 30 && yemekMiktarı < 100)
        {
            y_pozizyonu = -21.8f;
        }else if (yemekMiktarı > 100 && yemekMiktarı < 200)
        {
            y_pozizyonu = -20.9f;
        }else if (yemekMiktarı > 200)
        {
            y_pozizyonu = -20;
        }
        zemin.transform.localPosition = new Vector3(zemin.transform.localPosition.x, y_pozizyonu, zemin.transform.localPosition.z);

    }


    private void samanAl()
    {
        saman += alınanSamanDegeri * 15;
    }

}
