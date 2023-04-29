using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperDrone : Enemy
{
    public Transform gun;
    private Vector3 moveDirection;
    private float speed;

    protected override void HandleBehaviour()
    {
        if (target == null) target = gameObject.transform;
        transform.position += moveDirection * currentSpeed * Time.deltaTime;
        RotateToTarget();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) target = gameObject.transform;
        // transform.rotation= Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.up, (target.position - transform.position).normalized, Vector3.forward));
        // gun = gameObject.GetComponentInChildren<Transform>();
        moveDirection = (target.position - transform.position).normalized;
        speed = 90;
        RotateToTarget();
    }

    
    void Update()
    {
        HandleBehaviour();
       
    }

    private void RotateToTarget()
    {
        float angle = Vector3.SignedAngle(Vector3.up, (target.position - transform.position).normalized, Vector3.forward);
       
        if (angle < 0 && angle > -180) gun.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if(angle>=0&&angle<180) gun.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        Quaternion rotationToTarget = Quaternion.Euler(0, 0, angle);
        gun.localRotation = Quaternion.RotateTowards(gun.localRotation, rotationToTarget, speed * Time.deltaTime);

    }


  
}
