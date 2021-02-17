using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using GoogleMobileAds.Api;


public class yönetici : MonoBehaviour {
    public float day;
    private float süt;
    public float para;
    public float samanSayısı;
    private int x = 0;
    public float yemSayısı = 10;
    public float toplamSüt;
    public float hoursstring_f;
    private int i=0;
    private int ix = 0;
    private bool panelAçıkmı;
    private bool m_cursorIsLocked=true;
    private float inekSayısı;
    public float tohumluİnekSayısı;
    private float bankaBorcu = 5000;
    public float gübreMiktarı=100;
    private double sütBirimFiyat;
    private float gübreBirimFiyat;
    private float samanBirimFiyat;
    private float yemBirimFiyat;
    private bool fiyatBelirle = true;
    public float TohumlamaMaliyeti = 40;

    

    public float AlınanSamanlıkDeğeri;
    string[] günler = {"MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY" };
    GameObject[] inekler_sayısı;
    private Rigidbody rg;
    public int real_second_per_ingame_day = 130;
    public bool pause;
    
    //float move_x;
    //float move_y;
    //private Vector3 gecişPozizyonu_1;
    //private Vector3 gecişPozizyonu_2;
    [SerializeField] float Kamera_Dönüş_Hızı;

    //[SerializeField] yemlik_kontrol yemlik_Kontrol;
    public bool süt_üretme_vaktimi;
    kol kol;
    private bool I;

    [Header("Text'ler")]
    [SerializeField] Text zaman;
    [SerializeField] Text para_text;
    [SerializeField] Text para_text2;
    [SerializeField] Text para_text3;
    [SerializeField] Text saman_text;
    [SerializeField] Text yem_text;
    [SerializeField] Text gübre_text;
    [SerializeField] Text günText;
    [SerializeField] Text ToplamSüt;
    [SerializeField] Text İnekSayısı;
    [SerializeField] Text TohumluİnekSayısı;
    [SerializeField] Text banka_borcu;
    [SerializeField] Text samanFiyat;
    [SerializeField] Text yemFiyat;
    [SerializeField] Text sütFiyat;
    [SerializeField] Text güpreFiyat;
    [SerializeField] Text SamanlıkBilgisi;
    [SerializeField] Text İçerik;
    [SerializeField] Text Tohumlama_Maliyeti;
    [SerializeField] InputField sütınputt;
    [SerializeField] InputField samanInputt;
    [SerializeField] InputField yemInputt;
    [SerializeField] InputField güpreInputt;
    [SerializeField] InputField SamanAlMiktarInput;

    [Header("Objeler")]
    public GameObject[] inekler;
    [SerializeField] GameObject kolObje;
    [SerializeField] GameObject güneş;
    [SerializeField] GameObject saman;
    [SerializeField] GameObject panel_1;
    [SerializeField] GameObject panel_2;
    [SerializeField] GameObject panel_3;
    [SerializeField] GameObject panel_kontroller;
    [SerializeField] GameObject panel_Başlangıç;
    [SerializeField] GameObject samanInput;
    [SerializeField] GameObject tuş_Panel;
    [SerializeField] GameObject yemInput;
    [SerializeField] GameObject sütInput;
    [SerializeField] GameObject gübreInput;
    [SerializeField] GameObject samanPaneli;
    [SerializeField] GameObject CıkısButon;
    [SerializeField] NasılOynanır NasılOynanır;
    //[SerializeField] GameObject Kamera;
    [SerializeField] GameObject player;


    private InterstitialAd interstitial;
    // Use this for initialization
    void Start()
    {
        para = 100;
        samanSayısı = 10;
        kol = kolObje.GetComponent<kol>();
        Vector3 inek_8 = new Vector3(75, 0, 290);
        Vector3 inek_17 = new Vector3(80, 0, 380);
        GameObject inek8 = Instantiate(inekler[0], inek_8, Quaternion.identity) as GameObject;
        inek8.transform.localScale = new Vector3(5, 5, 4);
        Instantiate(inekler[1], inek_17, Quaternion.identity);
        day = 0.40f;
        InvokeRepeating("BankaBorcu", 60, 60);
        BirimFiyatBelirle();
        panel_Başlangıç.SetActive(true);
        m_cursorIsLocked = false;
        rg = player.GetComponent<Rigidbody>();
        Time.timeScale = 0;
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
        // Interstitial reklamı Aktif Et //
        //RequestInterstitial();
        //ReklamıOynat();
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    private void ReklamıOynat()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    // Update is called once per frame
    void Update () {
        inekler_sayısı = GameObject.FindGameObjectsWithTag("inek");
        inekSayısı = inekler_sayısı.Length;
        Textler();

        #region ZAMAN

        day += Time.deltaTime / real_second_per_ingame_day;
        float daynormalized = day % 1f;
        float hoursperday = 24f;
        float minuteperhour = 60f;
        hoursstring_f = Mathf.Floor(daynormalized * hoursperday);
        float minutestring_f = Mathf.Floor(((daynormalized * hoursperday) % 1) * minuteperhour);
        string hoursstring = hoursstring_f.ToString("00");
        string minutestring = minutestring_f.ToString("00");
        zaman.text = hoursstring + ":" + minutestring;
        //print(day);
        #endregion

        #region GÜN
        if (hoursstring_f == 0 && ix==0)
        {
            i++;
            if (i == 7)
            {
                i = 0;
            }
            //print("girdi");
            ix++;
        }
        else if(hoursstring_f != 0)
        {
            ix = 0;
        }
        
        #endregion

        #region GÜNEŞ

        güneş.transform.eulerAngles = new Vector3((daynormalized * 360)- 90f, 0, 0);

        #endregion

        #region SÜT ÜRETME VAKTİMİ
        if (hoursstring_f ==  8f || hoursstring_f==18f)
        {
           süt_üretme_vaktimi = true;
             
        }
        else
        {
            süt_üretme_vaktimi = false;
            
        }
        #endregion

        İnekSayısınaGöreZamanAyarlama();
#if UNITY_EDITOR
        CURSORvePaneller();

#elif UNITY_ANDROID
    //CURSORvePaneller();
#endif

        #region Birim Fiyat Belirle
        if (hoursstring_f == 00 || hoursstring_f == 12)
        {
             BirimFiyatBelirle();
        }
        else 
        { 
            fiyatBelirle = true;
        }
#endregion

    }

    void CURSORvePaneller()
    {
        if (Input.GetKeyDown(KeyCode.B) && !panel_kontroller.activeInHierarchy)
        {
            panel_kontroller.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.B) && panel_kontroller.activeInHierarchy)
        {
            panel_kontroller.SetActive(false);
            Time.timeScale = 1;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
            CıkısButon.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_cursorIsLocked = true;
            CıkısButon.SetActive(false);
        }

        CursorAyarla();
    }

    void İnekSayısınaGöreZamanAyarlama()
    {
        if (inekSayısı <= 2)
        {
            real_second_per_ingame_day = 130;
        }
        else if (inekSayısı > 2 && inekSayısı <= 5)
        {
            real_second_per_ingame_day = 180;
        }
        else if (inekSayısı > 5 && inekSayısı <= 10)
        {
            real_second_per_ingame_day = 270;
        }
        else if (inekSayısı > 10 && inekSayısı <= 15)
        {
            real_second_per_ingame_day = 360;
        }
        else if (inekSayısı > 15)
        {
            real_second_per_ingame_day = 460;
        }
    }
    void Textler()
    {
        para = (float)Math.Round(para, 1);
        para_text.text = para.ToString() + " $";
        para_text2.text = para.ToString() + " $";
        para_text3.text = para.ToString() + " $";
        saman_text.text = "Grass: " + samanSayısı.ToString();
        yem_text.text = "Silage bag: " + yemSayısı.ToString();
        günText.text = günler[i];
        ToplamSüt.text = "Total milk: " + toplamSüt.ToString() + " L";
        İnekSayısı.text = "Total cow number: " + inekSayısı.ToString();
        TohumluİnekSayısı.text = "Total inseminated cow: " + tohumluİnekSayısı.ToString();
        banka_borcu.text = "bank debt: " + bankaBorcu.ToString() + " $";
        gübre_text.text = "Amount of manure: " + gübreMiktarı.ToString();
        Tohumlama_Maliyeti.text = "Insemination price: " + TohumlamaMaliyeti;
        samanFiyat.text = "Grass price: " + samanBirimFiyat.ToString();
        sütFiyat.text = "Milk price: " + sütBirimFiyat.ToString();
        güpreFiyat.text = "Manure price: " + gübreBirimFiyat.ToString();
        yemFiyat.text = "Silage bag price: " + yemBirimFiyat.ToString();
    }
    
    public void İnek_üret(int x)
    {
        int sayı=x;
        if (sayı > 0)
        {
            int i = UnityEngine.Random.Range(2, inekler.Length);
            GameObject inek = Instantiate(inekler[i], inekler[i].transform.position, Quaternion.identity);
            Array.Clear(inekler, i, 1);
            inek.transform.localScale = new Vector3(5, 5, 4);
            sayı = 0;
            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (kol.F)
            {
                if (!kol.el_dolumu && samanSayısı > 0)
                {
                    samanPaneli.SetActive(true);
                    m_cursorIsLocked = false;
                }

            }
            
        }
        
    }

    private void CursorAyarla()
    {
        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void istatistik_paneli()
    {
        
        panel_2.SetActive(false);
        panel_3.SetActive(false);
    }
    

    public void Yardım_paneli()
    {
        panel_3.SetActive(false);
        panel_2.SetActive(true);
    }
    public void Satın_alma_paneli()
    {
        panel_3.SetActive(true);
        panel_2.SetActive(false);
    }

    private void BankaBorcu()
    {
        if (bankaBorcu > 0)
        {
            para -= 5;
            bankaBorcu -= 5;
        }
    }
    public void samanAl()
    {
        string değer = samanInput.GetComponent<Text>().text;
        float yazılanSaman = float.Parse(değer);
        float gider = yazılanSaman * samanBirimFiyat;
        if (para >= gider)
        {
            samanSayısı += yazılanSaman;
            para -= gider;
            samanInputt.text = "";
        }
        

    }

    public void yemAl()
    {
        string değer = yemInput.GetComponent<Text>().text;
        float yazılanYem = float.Parse(değer);
        float gider = yazılanYem * yemBirimFiyat;
        if (para >= gider)
        {
            yemSayısı += yazılanYem;
            para -= gider;
            yemInputt.text = "";
        }
    }

    public void sütSat()
    {
        string değer = sütInput.GetComponent<Text>().text;
        float yazılanSüt = float.Parse(değer);
        float gelir = yazılanSüt * (float)sütBirimFiyat;
        if (toplamSüt >= yazılanSüt)
        {
            toplamSüt -= yazılanSüt;
            para += gelir;
            sütınputt.text = "";
            print("girdi");
        }
    }

    public void güpreSat()
    {
        string değer = gübreInput.GetComponent<Text>().text;
        float yazılanGübre=float.Parse(değer);
        float gelir = yazılanGübre * gübreBirimFiyat;
        if (gübreMiktarı >= yazılanGübre)
        {
            gübreMiktarı -= yazılanGübre;
            para += gelir;
            güpreInputt.text = "";
        }
        
    }

    private void BirimFiyatBelirle()
    {
        if (fiyatBelirle)
        {
            gübreBirimFiyat = 0.10f;
            sütBirimFiyat = UnityEngine.Random.Range(1.5f, 2.3f);
            sütBirimFiyat = Math.Round(sütBirimFiyat, 2);
            samanBirimFiyat = UnityEngine.Random.Range(20, 30);
            yemBirimFiyat = UnityEngine.Random.Range(15, 25);
            fiyatBelirle = false;
        }
    }

    public void Baslangıç_ekranını_kapat()
    {
        panel_Başlangıç.SetActive(false);
        m_cursorIsLocked = true;
        Time.timeScale = 1;
    }
    public void Süt_sağım()
    {
        İçerik.text = NasılOynanır.durum_süt();
    }
    public void Yiyecekler()
    {
        İçerik.text = NasılOynanır.durum_yemek();
    }
    public void Güpre_nasılOynanır()
    {
        İçerik.text = NasılOynanır.durum_gübre();
    }
    public void Tohumlama_nasılOynanır()
    {
        İçerik.text = NasılOynanır.durum_tohumlama();
    }
    public void SamanlıktanSamanAl()
    {
        samanPaneli.SetActive(false);
        m_cursorIsLocked = true;
        string değer = SamanlıkBilgisi.GetComponent<Text>().text;
        AlınanSamanlıkDeğeri = float.Parse(değer);
        if (AlınanSamanlıkDeğeri < samanSayısı)
        {
            kol.Saman_üret(saman);
            
        }
        SamanAlMiktarInput.text = "";
    }
    public void Cıkıs()
    {
        Application.Quit();
    }
    public void I_doğru()
    {
        if (!panelAçıkmı)
        {
            panel_1.SetActive(true);
            m_cursorIsLocked = false;
            panelAçıkmı = true;
            tuş_Panel.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void I_yanlış()
    {
        if (panelAçıkmı)
        {
            panel_1.SetActive(false);
            m_cursorIsLocked = true;
            panelAçıkmı = false;
            tuş_Panel.SetActive(true);
            Time.timeScale = 1;
        }
    }
    public void tohumlama()
    {
        for (int i = 0; i < inekler_sayısı.Length; i++)
        {
            float mesafe = Vector3.Distance(player.transform.position, inekler_sayısı[i].transform.position);
            if (mesafe < 12)
            {
                inek_kontrol aaa = inekler_sayısı[i].GetComponent<inek_kontrol>();
                aaa.Tohumlama();
            }
        }
    }
}
