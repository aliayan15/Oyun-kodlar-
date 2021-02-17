using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;


public class MenüKontrol : MonoBehaviour
{
    private BannerView bannerView;

    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 5.0f;
    //public bool internetConnectBool;
    private Ping ping;
    private float pingStartTime;
    //private static float internet = 0;

    private void Awake()
    {
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        BennerReklamİste();
    }

    void Start()
    {
        Time.timeScale = 1;
        if (!PlayerPref.ZorlukDegerKontrol())
        {
            PlayerPref.ortaDegerAta(1);
        }
        //Debug.Log("aa"+PlayerPref.Puanlar.Count);
        if (PlayerPref.Puanlar.Count < 1)
        {
            PlayerPref.Puanlar.Add(PlayerPref.puan1rOku());
            PlayerPref.Puanlar.Add(PlayerPref.puan2rOku());
            PlayerPref.Puanlar.Add(PlayerPref.puan3Oku());
        }
        
    }

    private void Update()
    {
        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
                InternetAvailable();
            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;
            else
                InternetIsNotAvailable();
            if (stopCheck)
                ping = null;
            
        }
    }

    public void InternetIsNotAvailable()
    {
        //PlayerPref.internet = false;
    }

    public void InternetAvailable()
    {
        //PlayerPref.internet = true;
    }

    void İnternetKontrol()
    {
        //internet++;
        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                //internetPossiblyAvailable = allowCarrierDataNetwork;
                internetPossiblyAvailable = true;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
        
    }

    void BennerReklamİste()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        string adUnitId = "ca-app-pub-1528826168986774/5093131111";
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void Play()
    {
        bannerView.Destroy();
        SceneManager.LoadScene("Oyun");
    }

    public void Puanlar()
    {
        SceneManager.LoadScene("Skorlar");
    }

    public void AYARLAR()
    {
        SceneManager.LoadScene("Ayarlar");
    }
    public void İnternetKontrolEt()
    {
        İnternetKontrol();
    }
}
