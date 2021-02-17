using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class PlayerPref
{
    public static string kolay = "kolay";
    public static string orta = "orta";
    public static string zor = "zor";
    public static string puan = "puan";

    public static int sonPuan;
    public static string Puan1 = "puan1";
    public static string Puan2 = "puan2";
    public static string Puan3 = "puan3";
    public static List<int> Puanlar = new List<int>();

    //public static bool internet;

    

    public static void kolayDegerAta(int kolay)
    {
        PlayerPrefs.SetInt(PlayerPref.kolay, kolay);
    }
    public static int kolayDegerOku()
    {
        return PlayerPrefs.GetInt(PlayerPref.kolay);
    }

    public static void ortaDegerAta(int orta)
    {
        PlayerPrefs.SetInt(PlayerPref.orta, orta);
    }
    public static int ortaDegerOku()
    {
        return PlayerPrefs.GetInt(PlayerPref.orta);
    }

    public static void zorDegerAta(int kolay)
    {
        PlayerPrefs.SetInt(PlayerPref.zor, kolay);
    }
    public static int zorDegerOku()
    {
        return PlayerPrefs.GetInt(PlayerPref.zor);
    }


    public static bool ZorlukDegerKontrol()
    {
        if(PlayerPrefs.HasKey(PlayerPref.kolay)|| PlayerPrefs.HasKey(PlayerPref.orta)|| PlayerPrefs.HasKey(PlayerPref.zor))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public static void puanDegerAta(int puan)
    {
        
        sonPuan = puan;
        Puanlar.Add(sonPuan);
        Puanlar.Sort();
        Puanlar.Reverse();
        puan1Ata(PlayerPref.Puanlar[0]);
        puan2Ata(PlayerPref.Puanlar[1]);
        puan3Ata(PlayerPref.Puanlar[2]);
    }

    #region Puan Ata
    public static void puan1Ata(int puan)
    {
        PlayerPrefs.SetInt(PlayerPref.Puan1, puan);
    }
    public static int puan1rOku()
    {
        return PlayerPrefs.GetInt(PlayerPref.Puan1);
    }

    public static void puan2Ata(int orta)
    {
        PlayerPrefs.SetInt(PlayerPref.Puan2, orta);
    }
    public static int puan2rOku()
    {
        return PlayerPrefs.GetInt(PlayerPref.Puan2);
    }

    public static void puan3Ata(int kolay)
    {
        PlayerPrefs.SetInt(PlayerPref.Puan3, kolay);
    }
    public static int puan3Oku()
    {
        return PlayerPrefs.GetInt(PlayerPref.Puan3);
    }
    #endregion

}
