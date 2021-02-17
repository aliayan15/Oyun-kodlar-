using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
public class menüKontrol : MonoBehaviour
{
    [SerializeField] GameObject yükleme_paneli;
    private BannerView bannerView;
    AsyncOperation loadingOperation;
    


    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-1528826168986774/2831439053";
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void Update()
    {
        if (loadingOperation.progress == 0.9f)
        {
            bannerView.Destroy();
        }


    }

    public void YeniOyun()
    {
        loadingOperation = SceneManager.LoadSceneAsync("oyunSahnesi");
        yükleme_paneli.SetActive(true);
        
    }
    public void Çıkış()
    {
        Application.Quit();
    }
}
