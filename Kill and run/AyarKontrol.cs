using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AyarKontrol : MonoBehaviour
{

    [SerializeField] Button kolay, orta, zor;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPref.kolayDegerOku() == 1)
        {
            kolay.interactable = false;
            orta.interactable = true;
            zor.interactable = true;
        } 
        if (PlayerPref.ortaDegerOku() == 1) {

            kolay.interactable = true;
            orta.interactable = false;
            zor.interactable = true;
        }
        if(PlayerPref.zorDegerOku() == 1)
        {
            kolay.interactable = true;
            orta.interactable = true;
            zor.interactable = false;
        }
       
    }

    public void Geri()
    {
        SceneManager.LoadScene("Menü");
    }

    public void Kolay()
    {
        PlayerPref.kolayDegerAta(1);
        PlayerPref.ortaDegerAta(0);
        PlayerPref.zorDegerAta(0);

        kolay.interactable = false;
        orta.interactable = true;
        zor.interactable = true;
    }
    public void Orta()
    {
        PlayerPref.kolayDegerAta(0);
        PlayerPref.ortaDegerAta(1);
        PlayerPref.zorDegerAta(0);
        kolay.interactable = true;
        orta.interactable = false;
        zor.interactable = true;
    }
    public void Zor()
    {
        PlayerPref.kolayDegerAta(0);
        PlayerPref.ortaDegerAta(0);
        PlayerPref.zorDegerAta(1);
        kolay.interactable = true;
        orta.interactable = true;
        zor.interactable = false;
    }
}
