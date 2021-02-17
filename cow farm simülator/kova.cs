using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kova : MonoBehaviour
{
    private bool alanaGirdi;
    private bool sütDökümYeri;
    private GameObject obje;
    private float sütMiktarı;

    

    yönetici yönetici;
    [SerializeField] GameObject sütTabakası;
    // Start is called before the first frame update
    void Start()
    {
        yönetici = GameObject.FindGameObjectWithTag("yönetici").GetComponent<yönetici>();
    }

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("inek"))
        {
            obje = other.gameObject;
            alanaGirdi = true;
            
        }
        if (other.CompareTag("süt döküm yeri"))
        {
            sütDökümYeri = true;

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("inek"))
        {
            alanaGirdi = false;
            obje = null;
            
        }
        if (other.CompareTag("süt döküm yeri"))
        {
            sütDökümYeri =false;
        }
        
    }
    public void G_doğru()
    {
        if (alanaGirdi)
        {
            inek_kontrol inek_Kontrol = obje.GetComponent<inek_kontrol>();
            sütMiktarı = inek_Kontrol.süt;
            inek_Kontrol.süt = 0;
            if (sütMiktarı > 0)
            {
                sütTabakası.SetActive(true);
            }
        }
        else if (sütDökümYeri)
        {
            yönetici.toplamSüt += sütMiktarı;
            sütMiktarı = 0;
            sütTabakası.SetActive(false);
        }
        //print("alanagirdi: " + alanaGirdi + "  " + "süt dökümyeri: " + sütDökümYeri);
    }
    
}
