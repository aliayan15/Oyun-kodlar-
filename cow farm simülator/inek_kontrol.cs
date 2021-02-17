using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class inek_kontrol : MonoBehaviour {
    Animator animatör;
    //Vector3[] yerler;
    NavMeshAgent nav;
    Transform player;
    yemlik_kontrol yemlik_Kontrol;
    yönetici yönetici;
    AudioSource audio;

    [SerializeField] GameObject Canvas;
    [SerializeField] AudioClip[] clip;
    [SerializeField] Image ımage;
    [SerializeField] Sprite sütImage;
    [SerializeField] Sprite aşıImage;
    [Header("Özellikler")]
    float büyüme_tekrarı_süresi = 30;

    private Vector3 mevcut_alan, hedefPozizyon;
    public bool yürü, hedefbelirlensinmi;
    public bool sütAktarılsınMı;
    public float süt;
    private int sayıcı=0;
    float day_ilk;
    float day_güncel;
    float yaş;
    private float tohumlu_zaman = 0;
    private float real_second_per_ingame_day;
    float olgun_yaş;
    bool olgunMu;
    bool tohumlamaVaktimi;
    bool tohumlu;
    private float AnimasyonSayıcı;
    private float sayıcı2;
    private float sayıcı3;
    private int e = 0;
    
    


    // Use this for initialization
    void Start () {
        
        audio = GetComponent<AudioSource>();
        yemlik_Kontrol = GameObject.Find("yemlik").GetComponent<yemlik_kontrol>();
        yönetici = GameObject.Find("yönetici").GetComponent<yönetici>();
        real_second_per_ingame_day = yönetici.real_second_per_ingame_day;
        nav = GetComponent<NavMeshAgent>();
        animatör = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        day_ilk = yönetici.day;
        Canvas.SetActive(false);
        Yaşlanma();
        #region Büyüme
        InvokeRepeating("Yaşlanma", büyüme_tekrarı_süresi, büyüme_tekrarı_süresi);
        #endregion
        AnimasyonSayıcı = Random.Range(13, 18);
    }

    // Update is called once per frame
    private void Update()
    {
        real_second_per_ingame_day = yönetici.real_second_per_ingame_day;
        Canvas.transform.LookAt(player);
        day_güncel = yönetici.day;
        yaş = day_güncel - day_ilk;
        if (süt > 1)
        {
            Canvas.SetActive(true);
            ımage.sprite = sütImage;
        }
        else
        {
            Canvas.SetActive(false);
        }
        
        #region SÜT ÜRETME
        if (yönetici.süt_üretme_vaktimi)
        {
            if (sayıcı == 0)
            {
                süt_üret();
                
                sayıcı++;
            }

        }
        else
        {
            sayıcı = 0;
            
        }
        #endregion

        inekÜret();

        ANİMASYONKONTROL();

        #region Yürüme
        if (!nav.pathPending && nav.remainingDistance < 0.1f)
        {
            if (yürü)
            {
                if (hedefbelirlensinmi)
                {
                    hedefbelirle();
                    hedefbelirlensinmi = false;
                }

                yürüme();
                yürü = false;
            }
            animatör.SetBool("walk", false);


        }
        else
        {
            animatör.SetBool("walk", true);
        }
        #endregion

    }

    void ANİMASYONKONTROL()
    {
        sayıcı2 += Time.deltaTime;
        sayıcı3 = Mathf.Floor(sayıcı2);
        //print(sayıcı3);
        if (sayıcı3 == AnimasyonSayıcı)
        {
            int q = Random.Range(1, 8);
            switch (q)
            {
                case 1:
                    audio.PlayOneShot(clip[Random.Range(0, 3)]);
                    sayıcı2 = 0;
                    break;
                case 2:
                    animatör.SetBool("eat", true);
                    sayıcı2 = 0;
                    break;
                case 3:
                    animatör.SetBool("eat", false);
                    audio.PlayOneShot(clip[Random.Range(0, 3)]);
                    sayıcı2 = 0;
                    break;
                case 4:
                    animatör.SetBool("eat", false);
                    audio.PlayOneShot(clip[Random.Range(0, 3)]);
                    sayıcı2 = 0;
                    break;
                case 5:
                    animatör.SetBool("eat", true);
                    sayıcı2 = 0;
                    break;
                case 6:
                    animatör.SetBool("eat", false);
                    hedefbelirlensinmi = true;
                    yürü = true;
                    sayıcı2 = 0;
                    break;
                case 7:
                    animatör.SetBool("eat", false);
                    hedefbelirlensinmi = true;
                    yürü = true;
                    sayıcı2 = 0;
                    break;

            }
            //print("içine girdi");    

        }
        if (yaş > 22.4f)
        {
            animatör.SetBool("öl", true);
            Destroy(this.gameObject, 2f);

        }
    }

    void inekÜret()
    {
        if (olgunMu)
        {
            if (!tohumlu)
            {
                olgun_yaş += Time.deltaTime / real_second_per_ingame_day;
                if (olgun_yaş > 1.5f)
                {
                    tohumlamaVaktimi = true;
                    TohumlamaVakti();
                    if (olgun_yaş > 3)
                    {
                        tohumlamaVaktimi = false;
                        TohumlamaVakti();
                        olgun_yaş = 0;
                    }
                }
            }

            if (tohumlu)
            {
                tohumlamaVaktimi = false;
                tohumlu_zaman += Time.deltaTime / real_second_per_ingame_day;
                if (tohumlu_zaman > 2f)
                {
                    yönetici.İnek_üret(1);
                    olgun_yaş = 0;
                    yönetici.tohumluİnekSayısı--;
                    tohumlu = false;
                }
            }

        }
    }

    void yürüme()
    {
        nav.destination = hedefPozizyon;
           
    }
   
    private void hedefbelirle()
    {
        string inek_ismi = this.gameObject.name;
        switch (inek_ismi)
        {
            case "inek 1(Clone)":
                mevcut_alan= new Vector3(Random.Range(-25, 20), 0, Random.Range(286, 326));
                break;
            case "inek 2(Clone)":
                mevcut_alan= new Vector3(Random.Range(-4.2f,38), 0, Random.Range(245,280));
                break;
            case "inek 3(Clone)":
                mevcut_alan= new Vector3(Random.Range(-25,20), 0, Random.Range(215,240));
                break;
            case "inek 4(Clone)":
                mevcut_alan = new Vector3(Random.Range(25,60), 0, Random.Range(215,240));
                break;
            case "inek 5(Clone)":
                mevcut_alan = new Vector3(Random.Range(43, 62), 0, Random.Range(245,280));
                break;
            case "inek 6(Clone)":
                mevcut_alan = new Vector3(Random.Range(25,60), 0, Random.Range(286,326));
                break;
            case "inek 8(Clone)":
                mevcut_alan = new Vector3(Random.Range(65,140), 0, Random.Range(285,305));
                break;
            case "inek 7(Clone)":
                mevcut_alan = new Vector3(Random.Range(65,140), 0, Random.Range(308,326));
                break;
            case "inek 9(Clone)":
                mevcut_alan = new Vector3(Random.Range(65,95), 0, Random.Range(215,280));
                break;
            case "inek 10(Clone)":
                mevcut_alan = new Vector3(Random.Range(105,140), 0, Random.Range(215,280));
                break;
            case "inek 11(Clone)":
                mevcut_alan = new Vector3(Random.Range(145,182), 0, Random.Range(270,326));
                break;
            case "inek 12(Clone)":
                mevcut_alan = new Vector3(Random.Range(145,182), 0, Random.Range(215,265));
                break;
            case "inek 13(Clone)":
                mevcut_alan = new Vector3(Random.Range(187,224), 0, Random.Range(215,265));
                break;
            case "inek 14(Clone)":
                mevcut_alan = new Vector3(Random.Range(187,224), 0, Random.Range(270,326));
                break;
            case "inek 15(Clone)":
                mevcut_alan = new Vector3(Random.Range(-32,-2), 0, Random.Range(340,390));
                break;
            case "inek 16(Clone)":
                mevcut_alan = new Vector3(Random.Range(8,33), 0, Random.Range(340,390));
                break;
            case "inek 17(Clone)":
                mevcut_alan = new Vector3(Random.Range(43,114), 0, Random.Range(370,390));
                break;
            case "inek 18(Clone)":
                mevcut_alan = new Vector3(Random.Range(43,114), 0, Random.Range(340,360));
                break;
            case "inek 19(Clone)":
                mevcut_alan = new Vector3(Random.Range(124.8f,154.8f), 0, Random.Range(340,390));
                break;
            case "inek 20(Clone)":
                mevcut_alan = new Vector3(Random.Range(164.8f,190), 0, Random.Range(340,390));
                break;
        }
        hedefPozizyon = mevcut_alan;
    }

    private void Yaşlanma()
    {
        float scale = transform.localScale.x;
        if (scale < 7)
        {
            float büyüme_oranı = yaş / 3;
            Vector3 yeni_scale = transform.localScale + new Vector3(büyüme_oranı, büyüme_oranı, büyüme_oranı);
            transform.localScale = yeni_scale;
            olgunMu = false;
            //print("yaşlandı");
        }
        else
        {
            olgunMu = true;
        }
        
    }
    
    void süt_üret()
    {

        float yemek_eksiltme_miktarı;
        float SamanSütMiktarı;
        float YemSütMiktarı;
        if (yaş <= 14)
        {
            yemek_eksiltme_miktarı = 15;
            SamanSütMiktarı = 15;
            YemSütMiktarı = 10;
        }
        else
        {
            yemek_eksiltme_miktarı = 15;
            SamanSütMiktarı = 10;
            YemSütMiktarı = 5;
        }
        if (yemlik_Kontrol.saman > 0 && olgunMu)
        {
            süt = 0;
            süt += SamanSütMiktarı;
            yemlik_Kontrol.saman -= yemek_eksiltme_miktarı;
            yönetici.gübreMiktarı += 10;
            if (yemlik_Kontrol.yem > 0)
            {
                süt += YemSütMiktarı;
                yemlik_Kontrol.yem -= 5;
            }
        }else if (!olgunMu)
        {
            yemlik_Kontrol.saman -= 10;
            
        }
        
    }

    private void TohumlamaVakti()
    {
        if (tohumlamaVaktimi)
        {
            Canvas.SetActive(true);
            ımage.sprite = aşıImage;

        }
        else
        {
            Canvas.SetActive(false);
        }

    }
    public void Tohumlama()
    {
        if(tohumlamaVaktimi)
        {
            if (yönetici.para > yönetici.TohumlamaMaliyeti)
            {
                yönetici.para -= 40;
                tohumlu_zaman = 0;
                tohumlu = true;
                Canvas.SetActive(false);
                yönetici.tohumluİnekSayısı++;
                yönetici.TohumlamaMaliyeti *= 1.4f;
                yönetici.TohumlamaMaliyeti = Mathf.Floor(yönetici.TohumlamaMaliyeti);
            }
        }
        
    }
    

   
}
