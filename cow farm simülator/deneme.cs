using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class deneme : MonoBehaviour
{

    
    private Vector3 firstpoint; //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTemp = 0.0f;

    [SerializeField] GameObject player;
    private Rigidbody rg;
    [SerializeField] float Dönüş_hızı;

    void Start()
    {
        //Initialization our angles of camera
        xAngle = 0.0f;
        yAngle = 0.0f;
        transform.localRotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        rg = player.GetComponent<Rigidbody>();
    }
    //void Update()
    //{
        
    //}
    private void FixedUpdate()
    {
        //Check count touches
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)// Ekrana dokunan her parmak için
            {
                Touch parmak = Input.GetTouch(i);
                if (parmak.position.x > (Screen.width / 2))
                {
                    //Touch began, save position
                    if (parmak.phase == TouchPhase.Began)
                    {
                        firstpoint = parmak.position;
                        xAngTemp = xAngle;
                        yAngTemp = yAngle;
                    }
                    //Move finger by screen
                    if (parmak.phase == TouchPhase.Moved)
                    {
                        secondpoint = parmak.position;
                        //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                        xAngle = (float)(xAngTemp + (secondpoint.x - firstpoint.x) * 180.0 / Screen.width);
                        yAngle = (float)(yAngTemp - (secondpoint.y - firstpoint.y) * 90.0 / Screen.height);
                        //Rotate camera
                        this.transform.localRotation = Quaternion.Euler(yAngle * Dönüş_hızı, player.transform.rotation.y, 0.0f);
                        player.transform.localRotation = Quaternion.Euler(0, (xAngle * Dönüş_hızı) - 90, 0);
                        
                    }
                }
            }
            
        }
        else
        {
            rg.constraints = RigidbodyConstraints.FreezeRotation;
        }
        
    }
}
