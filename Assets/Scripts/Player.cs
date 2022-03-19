using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]

    public Rigidbody carBody;
    public Transform car;

    public float forwardSpeed, reverseSpeed, turnSpeed, speed;
    public float downForce;

    private float dx, dy;

    [SerializeField]
    //ray cast
    public LayerMask groundLayer;
    public Transform rayPoint;

    private bool grounded;
    public float raySize;

    void Update()
    {
        dx = Input.GetAxis("Horizontal"); //left & right steering
        dy = Input.GetAxis("Vertical"); //forward & backward movement

        /*Vector3 drive = transform.forward * dy * speed * Time.deltaTime;
        suvBody.MovePosition(transform.position + drive);*/

        if(dy > 0)
        {
            dy *= (forwardSpeed * speed);
        }
        else if(dy < 0)
        {
            dy *= (reverseSpeed * speed);
        }

        if(grounded) //the car will only steer if it is grounded
        {
            Vector3 steer = new Vector3(0, dx * turnSpeed * Time.deltaTime * Input.GetAxis("Vertical"), 0);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + steer);

            /*float rotation = turnSpeed * dx;
            rotation = Mathf.Clamp(rotation, -30, 30);
            suvBody.transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);*/
        }

        transform.position = carBody.transform.position;
    }
    void FixedUpdate()
    {
        grounded = false;

        RaycastHit hit;

        if(Physics.Raycast(rayPoint.position, -transform.up, out hit, raySize, groundLayer))
        {
            grounded = true;
        }

        if(grounded && (Mathf.Abs(dy) > 0))
        {   
            carBody.AddForce(transform.forward * dy);
        }

        else
        {
            carBody.AddForce(Vector3.up * -downForce * speed);
        }
    }
}
