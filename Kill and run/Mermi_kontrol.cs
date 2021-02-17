using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi_kontrol : MonoBehaviour
{
    float Hız = default;


    private void Start()
    {
        Hız = 12;
        Destroy(gameObject, 8f);
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Hız * Time.deltaTime;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("virüs üretme yeri"))
        {
            
            Destroy(gameObject);
        }
    }
    
}
