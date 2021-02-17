using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kol : MonoBehaviour {
    GameObject tutulacak_nesne = null;
    [SerializeField] Transform konum;
    [SerializeField] Transform konum2;
    [SerializeField] Transform player;
    [SerializeField] GameObject Yem;
    public bool el_dolumu;
    private int x = 0;

    public bool F;
    yönetici yönetici;
    private void Start()
    {
        yönetici = GameObject.Find("yönetici").GetComponent<yönetici>();
    }
    // Update is called once per frame
    void Update () {
        
       if (el_dolumu)
        {
            if (F)
            {
                if (tutulacak_nesne.CompareTag("tutulabilir"))
                {
                    tutulacak_nesne.GetComponentInChildren<Rigidbody>().useGravity = true;
                }
                tutulacak_nesne = null;
                el_dolumu = false;
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (tutulacak_nesne != null)
        {
            if (tutulacak_nesne.tag.Equals("tutulabilir"))
            {
                //Vector3 konum2 = new Vector3(konum.position.x, (konum.position.y) - 4, (konum.position.z) + 2);
                tutulacak_nesne.transform.position = konum2.position;
                tutulacak_nesne.GetComponentInChildren<Rigidbody>().useGravity = false;
            }
            else
            {
                tutulacak_nesne.transform.position = konum.position;
            }
            if (tutulacak_nesne.CompareTag("yem"))
            {
                tutulacak_nesne.transform.eulerAngles = new Vector3(player.eulerAngles.x, player.eulerAngles.y + 90f, player.eulerAngles.z);
            }
            else
            {
                tutulacak_nesne.transform.eulerAngles = player.eulerAngles;
            }
            
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("saman"))
        {
            if (F)
            {
                Tutukacak_nesne(other.gameObject);
                Invoke("booltotrua", 0.5f);
                
            }
            
        }
        if (other.CompareTag("yem"))
        {
            if (F)
            {
                Tutukacak_nesne(other.gameObject);
                Invoke("booltotrua", 0.5f);

            }

        }
        if (other.CompareTag("yem üret"))
        {
            if (F)
            {
                yemÜret();
            }
        }
        if (other.CompareTag("tutulabilir"))
        {
            if (F)
            {
                Tutukacak_nesne(other.gameObject);
                Invoke("booltotrua", 0.5f);

            }

        }

    }
    private void booltotrua()
    {
        el_dolumu = true;
        x = 0;
    }
    public void Tutukacak_nesne(GameObject nesne)
    {
        tutulacak_nesne = nesne;
    }
    public void Saman_üret(GameObject nesne)
    {
        
        if (x == 0)
        {
            GameObject y = Instantiate(nesne, konum.position, player.rotation) as GameObject;
            saman saman = y.GetComponent<saman>();
            saman.deger = yönetici.AlınanSamanlıkDeğeri;
            Tutukacak_nesne(y);
            Invoke("booltotrua", 0.5f);
            yönetici.samanSayısı -= yönetici.AlınanSamanlıkDeğeri;
            x++;
        }
    }
    public void yemÜret()
    {
        if (tutulacak_nesne == null && yönetici.yemSayısı > 0)
        {

            GameObject yeniYem = Instantiate(Yem, konum.position, Quaternion.Euler(0, 90, 0)) as GameObject;
            Tutukacak_nesne(yeniYem);
            Invoke("booltotrua", 0.5f);
            yönetici.yemSayısı--;
            
        }
    }
    public void F_doğru()
    {
        F = true;
    }
    public void F_yanlış()
    {
        F = false;
    }
}
