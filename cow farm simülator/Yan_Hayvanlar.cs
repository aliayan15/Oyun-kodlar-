using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Yan_Hayvanlar : MonoBehaviour
{
    NavMeshAgent nav;
    Animator animatör;
    AudioSource audioS;
    [SerializeField] AudioClip[] clip;
    [SerializeField] GameObject Yönetici;
    yönetici yönetic;
    private bool yürü;
    private bool hedefbelirlensinmi;
    private bool horoz;
    private Vector3 mevcut_alan;
    private Vector3 hedefPozizyon;
    private float sayıcı2;
    private float sayıcı3;
    private Vector3 başlanğıçKonumu;
    private float saat;
    private int xx;

    // Start is called before the first frame update
    void Start()
    {
        yönetic = Yönetici.GetComponent<yönetici>();
        audioS = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        animatör = GetComponent<Animator>();
        başlanğıçKonumu = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Animasyon Belirleme
        sayıcı2 += Time.deltaTime;
        sayıcı3 = Mathf.Floor(sayıcı2);
        
        if (sayıcı3 == 5)
        {
            int q = Random.Range(1, 8);
            //print(q);
            switch (q)
            {
                case 1:
                    if (this.gameObject.name == "Ördek")
                    {
                        audioS.PlayOneShot(clip[0]);
                    }
                    else if (this.gameObject.name == "Tavuk")
                    {
                        audioS.PlayOneShot(clip[1]);
                    }
                    
                    hedefbelirlensinmi = true;
                    yürü = true;
                    sayıcı2 = 0;
                    break;
                case 2:
                    animatör.SetBool("eat", true);
                    sayıcı2 = 0;
                    break;
                case 3:
                    animatör.SetBool("eat", false);
                    if (this.gameObject.name == "Ördek")
                    {
                        audioS.PlayOneShot(clip[0]);
                    }
                    else if (this.gameObject.name == "Tavuk")
                    {
                        audioS.PlayOneShot(clip[1]);
                    }
                    
                    sayıcı2 = 0;
                    break;
                case 4:
                    animatör.SetBool("eat", false);
                    if (this.gameObject.name == "Ördek")
                    {
                        audioS.PlayOneShot(clip[0]);
                    }
                    else if (this.gameObject.name == "Tavuk")
                    {
                        audioS.PlayOneShot(clip[1]);
                    }
                    
                    sayıcı2 = 0;
                    break;
                case 5:
                    animatör.SetBool("eat", true);
                    sayıcı2 = 0;
                    break;
                case 6:
                    hedefbelirlensinmi = true;
                    yürü = true;
                    sayıcı2 = 0;
                    break;
                case 7:
                    hedefbelirlensinmi = true;
                    yürü = true;
                    sayıcı2 = 0;
                    break;

            }
               

        }
        #endregion

        if (this.gameObject.name == "horoz")
        {
            
            saat = yönetic.hoursstring_f;
            if (saat == 7)
            {
                horozuÖttür();
            }
            else
            {
                xx = 0;
            }     
            
            
        }
        

    }
    private void FixedUpdate()
    {
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
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
    
    }

    private void hedefbelirle()
    {
        float x = başlanğıçKonumu.x;
        float y = başlanğıçKonumu.y;
        float z = başlanğıçKonumu.z;
        mevcut_alan = new Vector3(x + Random.Range(-3.5f, 3.5f), y + Random.Range(-3.5f, 3.5f), z + Random.Range(-3.5f, 3.5f));
        hedefPozizyon = mevcut_alan;
    }

    void yürüme()
    {
        nav.destination = hedefPozizyon;

    }
    private void horozuÖttür()
    {
        if (xx == 0)
        {
            audioS.PlayOneShot(clip[2]);
            //print("oynat");
            xx++;
        }
        

    }
}
