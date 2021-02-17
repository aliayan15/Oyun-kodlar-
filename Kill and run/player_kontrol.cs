using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_kontrol : MonoBehaviour
{
    [SerializeField] Ekran_Hesaplama ekran_Hesaplama;
    [SerializeField] Joystick joystick;
    [SerializeField] GameObject MermiPrepab;
    [SerializeField] Transform Namlu;
    [SerializeField] Transform silah;

    OyunKontrol oyunKontrol;
    

    //Rigidbody2D rg;
    Animator animator;
    Animator animator2;
    //AudioSource audioSource;
    bool Hareket = true;
    bool atesEdilebilir;

    float axıs_x;
    float axıs_y;
    float i;
    [HideInInspector] public float Can;

    [SerializeField] float Hız;
    // Start is called before the first frame update
    void Start()
    {
        oyunKontrol = FindObjectOfType<OyunKontrol>();
        animator2 = silah.GetComponent<Animator>();
        //rg = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Can = 3;
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayeriEkrandaTut();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            i = 0.2f;
            AtesEt();
            animator2.SetBool("ates", true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            atesEdilebilir = true;
            animator2.SetBool("ates", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            atesEdilebilir = false;
            animator2.SetBool("ates", false);
            AtesEt();
        }
        if (atesEdilebilir)
        {
            AtesEt();
        }

        if (Hareket)
        {
            HareketEt();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("virüs"))
        {
            Can -= 1;
            oyunKontrol.canazalt();
            if (Can <= 0)
            {
                OyunuDurdur();
            }
        }
        if (col.gameObject.CompareTag("Can"))
        {
            Destroy(col.gameObject);
            if (Can < 3)
            {
                Can++;
                oyunKontrol.canArttır();
            }
            
        }
    }
    void OyunuDurdur()
    {
        oyunKontrol.oyunBitti = true;
        Hareket = false;
        atesEdilebilir = false;
    }
    public void OyunuDevamEttir()
    {
        oyunKontrol.oyunBitti = false;
        Hareket = true;
        atesEdilebilir = true;
        for (int i = 0; i < 2; i++)
        {
            Can++;
            oyunKontrol.canArttır();
        }
    }


    void PlayeriEkrandaTut()
    {
        float mesafe = Camera.main.transform.position.x + ekran_Hesaplama.EkranGenislik / 2;
        if (transform.position.x >= mesafe)
        {

            Vector2 konum = new Vector2(mesafe, transform.position.y);
            transform.position = konum;
        }
    }
    void HareketEt()
    {
#if UNITY_EDITOR
        axıs_x = Input.GetAxis("Horizontal");
        axıs_y = Input.GetAxis("Vertical");
#elif UNITY_ANDROID
        axıs_x = joystick.Horizontal;
        axıs_y = joystick.Vertical;
#endif
        Vector2 Kuvvet = new Vector2(axıs_x, axıs_y);
        Kuvvet *= Hız * Time.deltaTime;
        //rg.AddForce(Kuvvet, ForceMode2D.Impulse);
        transform.Translate(Kuvvet);
        if (axıs_x != 0 || axıs_y != 0)
        {
            animator.SetBool("yürüme", true);
        }
        else
        {
            animator.SetBool("yürüme", false);
        }
    }


    public void ButonAteşEt()
    {
        i = 0.2f;
        atesEdilebilir = true;
        animator2.SetBool("ates", true);
    }
    public void ButonAteşEtme()
    {
        atesEdilebilir = false;
        AtesEt();
        animator2.SetBool("ates", false);
    }

    void AtesEt()
    {
        if (atesEdilebilir)
        {
            if (i == 0.2f)
            {
                //animator.SetBool("ates", true);
                Instantiate(MermiPrepab, Namlu.position, Quaternion.identity);
                //audioSource.PlayOneShot(audioSource.clip);
            }
            i -= Time.deltaTime;
            if (i < 0)
            {
                i = 0.2f;
            }
        }
       
    }
}
