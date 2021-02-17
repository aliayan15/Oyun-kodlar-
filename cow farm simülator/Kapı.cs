using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapı : MonoBehaviour {

    private AudioSource audio;
	// Use this for initialization
	
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            audio = GetComponent<AudioSource>();
            transform.localEulerAngles = new Vector3(0,Mathf.LerpAngle(0, 90, 3f),0);
            audio.Play();
            Invoke("default_konum", 3.5f);
            
        }
        
    }

    private void default_konum()
    {
        transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(90, 0, 2f), 0);
    }
}
