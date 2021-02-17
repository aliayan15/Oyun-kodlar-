using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ekran_Hesaplama : MonoBehaviour
{


    SpriteRenderer SpriteRenderer;

    float ekrangenislik;
    float ekranyüksekligi;

    float mesafe;

    /// <summary>
    /// Ekran genislini verir.
    /// </summary>
    public float EkranGenislik
    {
        get
        {
            return ekrangenislik;
        }
    }
    /// <summary>
    /// Ekran yükseklini verir.
    /// </summary>
    public float EkranYükseklik
    {
        get
        {
            return ekranyüksekligi;
        }
    }


    void EkranaSıgdır()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        float spriteGenislik = SpriteRenderer.size.x;
        float spriteYükseklik = SpriteRenderer.size.y;
        Vector2 istenenScale = transform.localScale;

        // Ekranın toplam uzunluğu
        ekranyüksekligi = Camera.main.orthographicSize * 2;
        ekrangenislik = ekranyüksekligi * Camera.main.aspect;

        istenenScale.x = ekrangenislik / spriteGenislik;
        istenenScale.y = ekranyüksekligi / spriteYükseklik;
        transform.localScale = istenenScale;
    }
    void EkranHesapla()
    {
        
        // Ekranın toplam uzunluğu
        ekranyüksekligi = Camera.main.orthographicSize * 2;
        ekrangenislik = ekranyüksekligi * Camera.main.aspect;
    }
    private void Awake()
    {
        EkranHesapla();
        EkranaSıgdır();
        mesafe = ekrangenislik;
    }
    
    


    // Update is called once per frame
    void Update()
    {
        if (transform.position.x + mesafe < Camera.main.transform.position.x)
        {
            ArkaplanYerlestir();
        }
        
    }
    void ArkaplanYerlestir()
    {
        Vector2 ilerleme = transform.position;
        ilerleme.x += mesafe * 2;
        transform.position = ilerleme;
        //VirüsÜret();
    }
    //void VirüsÜret()
    //{
        
    //    float minX = transform.position.x - ekrangenislik / 2;
    //    float max = transform.position.x + ekrangenislik / 2;
    //    float minY = -ekranyüksekligi / 2;
    //    float maxY = ekranyüksekligi / 2;
    //    OyunKontrol oyunKontrol = FindObjectOfType<OyunKontrol>();

    //    for (int i = 0; i < 10; i++)
    //    {
    //        Vector2 ilkKonum = new Vector2(Random.Range(minX + 1, max), Random.Range(minY + 1, maxY));
    //        oyunKontrol.VirüsÜret(ilkKonum);
    //    }
    //}
}
