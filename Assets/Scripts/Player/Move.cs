using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{

    private GameObject player;
    [SerializeField]
    private float speed;
    public FloatingJoystick joystic;
    [SerializeField]
    private Transform gun;

    [SerializeField]
    private Transform health;

    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        //gun = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
      
        float y = joystic.Vertical;
        float x = joystic.Horizontal;
        Vector3 joysticMove = new Vector3(x, y, 0);
          float angle = Vector3.SignedAngle(Vector3.up, joysticMove, Vector3.forward);
        player.transform.position += joysticMove * Time.deltaTime*Speed;
        if (angle != 0) gun.rotation = Quaternion.Euler(0, 0, angle);// Quaternion.RotateTowards(gun.localRotation, Quaternion.Euler(0,0,angle),90f);
        else return;  
        
        if (angle > 0) transform.localScale=new Vector3(-1,1,1);//gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else transform.localScale = new Vector3(1, 1, 1); //gameObject.GetComponent<SpriteRenderer>().flipX = false;//player.transform.rotation= Quaternion.Euler(0, 0, angle);
                                                          // GetComponentInChildren<Transform>().localScale = transform.localScale;
        health.localScale = transform.localScale;



    }
}
