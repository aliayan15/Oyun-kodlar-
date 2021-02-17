using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using GoogleMobileAds;

public class OyunKontrol : MonoBehaviour
{
    private RewardedAd rewardedAd;
    [SerializeField] GameObject virüsPrepab;
    [SerializeField] GameObject canPrepab;
    [SerializeField] GameObject cadde1;
    [SerializeField] GameObject cadde2;
    [SerializeField] GameObject virüsÜretmeYeri;
    [SerializeField] Text puan, oyunSonuPuan;
    [SerializeField] GameObject oyunsonuPaneli;
    [SerializeField] GameObject reklam, reklamPlay, internetBaglantısı;

    Ekran_Hesaplama ekran_Hesaplama;
    [SerializeField] List<GameObject> canlar = new List<GameObject>();
    List<GameObject> AlınmısCanlar = new List<GameObject>();

    float carpan1;
    float carpan2;
    float canEklemeZamanı;
    float virüsÜretmeZamanı;

    [HideInInspector] public float Puan;
    float sayıcı;
    int canEklemeSayacı;
    [HideInInspector] public bool oyunBitti;
    Vector2 camKonum;

    // Start is called before the first frame update
    void Start()
    {
        #region PlayerPref
        if (PlayerPref.kolayDegerOku() == 1)
        {
            carpan1 = 0.7f;
            carpan2 = 1.3f;
            canEklemeZamanı = 10;
            virüsÜretmeZamanı = 2.4f;
        }
        if (PlayerPref.ortaDegerOku() == 1)
        {
            carpan1 = 0.9f;
            carpan2 = 1.5f;
            canEklemeZamanı = 12;
            virüsÜretmeZamanı = 2.2f;
        }
        if (PlayerPref.zorDegerOku() == 1)
        {
            carpan1 = 1.1f;
            carpan2 = 1.8f;
            canEklemeZamanı = 14;
            virüsÜretmeZamanı = 2f;
        }
        #endregion
        Puan = 0;
        ekran_Hesaplama = cadde1.GetComponent<Ekran_Hesaplama>();
        camKonum = Camera.main.transform.position;
        cadde1.transform.position = new Vector2(camKonum.x, camKonum.y);
        cadde2.transform.position = new Vector2(camKonum.x + ekran_Hesaplama.EkranGenislik, camKonum.y);
        oyunBalangıcı();
        
    }
    void oyunBalangıcı()
    {
        Time.timeScale = 1;
        float minX = camKonum.x + ekran_Hesaplama.EkranGenislik / 2;
        float max = minX + ekran_Hesaplama.EkranGenislik;
        float minY = -ekran_Hesaplama.EkranYükseklik / 2;
        float maxY = ekran_Hesaplama.EkranYükseklik / 2;
        for (int i = 0; i < 3; i++)
        {
            Vector2 ilkKonum = new Vector2(Random.Range(minX + 1, max), Random.Range(minY + 1, maxY));
            VirüsÜret(ilkKonum);
            
        }
        sayıcı = -6;
        oyunsonuPaneli.SetActive(false);
        string adUnitId = "ca-app-pub-1528826168986774/6186720933";
        this.rewardedAd = new RewardedAd(adUnitId);
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when an ad request failed to load.
        //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    // Update is called once per frame
    void Update()
    {
        sayıcı += Time.deltaTime;
        if (sayıcı >= virüsÜretmeZamanı)
        {
            for (int i = 0; i < 2; i++)
            {
                VirüsÜret(VirüsÜretmeYeri());
                Puan += 10;
            }
            sayıcı = 0;
            canEklemeSayacı++;
        }
        if (canEklemeSayacı >= canEklemeZamanı)
        {
            CanÜret(VirüsÜretmeYeri());
            canEklemeSayacı = 0;
        }
        if (oyunBitti)
        {
            OyunBitti();
        }
        puan.text = Puan.ToString("00000");
    }

    void OyunBitti()
    {
        oyunsonuPaneli.SetActive(true);
        if (this.rewardedAd.IsLoaded())
        {
            reklam.SetActive(true);
        }
        reklamPlay.SetActive(false);
        oyunSonuPuan.text = "Score: " + Puan.ToString();
        Time.timeScale = 0;
        
    }

    void CanÜret(Vector2 konum)
    {
        GameObject can = Instantiate(canPrepab, konum, Quaternion.identity);
        Destroy(can, 30f);
    }

    public void VirüsÜret(Vector2 konum)
    {

        GameObject virüs = Instantiate(virüsPrepab, konum, Quaternion.identity);
        float scale = Random.Range(0.5f, 0.8f);
        virüs.transform.localScale = new Vector3(scale, scale);
        Rigidbody2D rg = virüs.GetComponent<Rigidbody2D>();
        if (scale <= 0.7f)
        {
            rg.AddForce(new Vector2(Random.Range(-2f, -0.5f) * 0.7f, Random.Range(2, -3)) * 1.2f, ForceMode2D.Impulse);
        }
        else
        {
            rg.AddForce(new Vector2(Random.Range(-2f, -0.5f) * carpan1, Random.Range(2, -3)) * carpan2, ForceMode2D.Impulse);
        }
        rg.AddTorque(10 * Random.Range(-2.9f,2.9f));

    }

    Vector2 VirüsÜretmeYeri()
    {
        Vector2 objeKonum = virüsÜretmeYeri.transform.position;
        BoxCollider2D objeKolaydır = virüsÜretmeYeri.GetComponent<BoxCollider2D>();
        float minX = objeKonum.x - objeKolaydır.size.x / 2;
        float maxX = minX + objeKolaydır.size.x;
        float maxY = objeKonum.y + objeKolaydır.size.y / 2;
        float minY= objeKonum.y - objeKolaydır.size.y / 2;
        Vector2 Konum = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        return Konum;
    }
    public void canazalt()
    {
        int i = canlar.Count - 1;
        canlar[i].SetActive(false);
        AlınmısCanlar.Add(canlar[i]);
        canlar.Remove(canlar[i]);
    }
    public void canArttır()
    {
        int i = AlınmısCanlar.Count - 1;
        AlınmısCanlar[i].SetActive(true);
        canlar.Add(AlınmısCanlar[i]);
        AlınmısCanlar.Remove(AlınmısCanlar[i]);
    }

    public void Geri()
    {
        PlayerPref.puanDegerAta((int)Puan);
        SceneManager.LoadScene("Menü");
    }
    public void TekrarOyna()
    {
        PlayerPref.puanDegerAta((int)Puan);
        SceneManager.LoadScene("Oyun");
       
    }
    public void DevamEt()
    {
        oyunsonuPaneli.SetActive(false);
        player_kontrol player_Kontrol = FindObjectOfType<player_kontrol>().GetComponent<player_kontrol>();
        player_Kontrol.OyunuDevamEttir();
        Time.timeScale = 1;
        player_Kontrol.ButonAteşEtme();
        reklam.SetActive(false);
        reklamPlay.SetActive(false);
    }
    public void ReklamGöster()
    {
        // Reklamı göster
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        oyunBitti = false;
        Time.timeScale = 1;
        reklam.SetActive(false);
        reklamPlay.SetActive(true);
        Time.timeScale = 0;
    }
    //public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    //{
    //    Debug.Log("faild reklam");
    //}
}
