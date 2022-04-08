using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Joystick joystick;
    Vector3 direction;
    [SerializeField] float rotationSpeed = 5f, speed = 5f;
    public float horizontal, vertical;
    
    Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        ToInputAxis();
    }
    private void FixedUpdate()
    {
        ToMove();
    }
    void ToInputAxis()
    {
        horizontal = joystick.result.x;
        vertical = joystick.result.y;
        direction = new Vector3(horizontal, 0, vertical).normalized;
    }

    void ToMove()
    {
        if (vertical != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            rb.velocity = new Vector3(horizontal * speed, 0f, vertical * speed);
            anim.SetBool("isShoot", false);
            anim.SetBool("isRun",true);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);

        }
        else
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isRun", false);

        }
    }
}
