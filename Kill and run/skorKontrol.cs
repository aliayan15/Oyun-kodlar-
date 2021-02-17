using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class skorKontrol : MonoBehaviour
{

    [SerializeField] Text Puan1, Puan2, Puan3;
    int puan1;
    int puan2;
    int puan3;

    // Start is called before the first frame update
    void Start()
    {
        puan1 = PlayerPref.puan1rOku();
        puan2 = PlayerPref.puan2rOku();
        puan3 = PlayerPref.puan3Oku();

        Puan1.text = "1:)   " + puan1.ToString();
        Puan2.text = "2:)   " + puan2.ToString();
        Puan3.text = "3:)   " + puan3.ToString();

    }

    public void Geri()
    {
        SceneManager.LoadScene("Menü");
    }
}
