using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Kontrol : MonoBehaviour
{
    [Header("Özellikler")]
    [SerializeField] float Yürüme_Hızı;

    [Header("Sürükle")]
    [SerializeField] private AudioClip[] m_FootstepSounds;

    [Header("Değişkenler")]
    private float axıs_x;
    private float axıs_y;
    private bool ileri;
    private bool geri;
    private float horizontal;
    private float vertical;
    private AudioSource m_AudioSource;

    [SerializeField] Joystick joystick;
    private Rigidbody rg;

    float ZAMAN;


    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        rg = GetComponent<Rigidbody>();
        
    }

    
    //void Update()
    //{
        
    //}
    private void FixedUpdate()
    {
        //Input_al();
        //#region İleri Geri Bool
        //if (ileri)
        //{
        //    axıs_x = 1;
        //}
        //else if (geri)
        //{
        //    axıs_x = -1;
        //}
        //else
        //{
        //    axıs_x = 0;
        //}
        //#endregion

        Hareket();
        
        
       


    }

    void Hareket()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        Vector3 kuvvet = new Vector3(x, 17.46f, y);
        if (x != 0 || y != 0)
        {
            rg.isKinematic = false;
            transform.Translate(kuvvet * Yürüme_Hızı * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 17.46f, transform.position.z);
        }
        else
        {
            rg.isKinematic = true;
        }
        if (x != 0 || y != 0)
        {
            ZAMAN += Time.deltaTime;
            if (ZAMAN > 0.8f)
            {
                PlayFootStepAudio();
                ZAMAN = 0;

            }
        }
    }
    
    private void PlayFootStepAudio()
    {
        
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;

    }

    public void down_İleri()
    {
        ileri = true;

    }
    public void up_Geri()
    {
        geri = false;
    }
    public void down_Geri()
    {
        geri = true;
    }
    public void up_İleri()
    {
        ileri = false;
    }
    private void Input_al()
    {
        horizontal = axıs_x * 5;
        vertical = 0;
    }
}
