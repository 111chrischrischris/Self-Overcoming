using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float maxSpeed;
    public float jumpSpeed;
    public Animator animator;
    private float distToGround;
    private Collider col;
    public bool grounded;
    public Quaternion originalRotation;
    private int jumpsDone = 0;
    public static playerScript Instance; //Script instance
    bool jumpWanted;
    void Start()
    {
        Instance = this;
        //Component references
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();


        distToGround = col.bounds.extents.y;
        originalRotation = transform.rotation;
        speed -= 5; //we do this because the player is in the air at the first frame, so it increments 100 again when player is grounded
        
    }
    void FixedUpdate()
    {
        //INPUT
        float hor = Input.GetAxisRaw("Horizontal") * speed * 10 * Time.deltaTime;

        if(hor < 0 )  //when character runs towards right side we rotate and play the running animation
        {
            transform.rotation = Quaternion.EulerAngles(0,-90,0);
            if(grounded)animator.Play("running");
        }
        else if(hor > 0 ) //when the character runs towards the left side we rotate and play the running animation
        {

            transform.rotation = originalRotation; //return to the normal rotation 
            if(grounded)animator.Play("running");
        }
        else if(hor == 0 && grounded) //if we are still and grounded we play the idle animation
        {
            animator.Play("idle");
        }
        else if(hor == 0 && !grounded) //if we aren't moving but jumping we play the stand animation
        {
            animator.Play("stand jump", 0, 0.60f);
        }

         // move the player when grounded according to the horizontal value from 0-1
        rb.AddForce(hor, 0, 0, ForceMode.VelocityChange); //Adding force to move the player
        
        
     

        if (speed > maxSpeed && !grounded) //limiting playes jumping velocity
     
        {
            float brakeSpeed = speed - maxSpeed;  // calculate the speed decrease
     
             Vector3 normalisedVelocity  = rb.velocity.normalized;
             Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value
     
             rb.AddForce(-brakeVelocity);  // apply opposing brake force
         }
        
         if(Mathf.Abs(rb.velocity.x) > maxSpeed) // if character exceeds the maximum speed while running, we slow down 
        {
            if(rb.velocity.x < 0)
            {

                rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

            }
        }

        if (jumpWanted)
        {

            jumpsDone++;
           // rb.velocity -= new Vector3(0,1,0);
            if(hor == 0)
            {
                rb.AddForce(0, (((jumpSpeed *100) * 80 ) / 100) *Time.deltaTime, 0, ForceMode.Force); //if player is still, we only jump with 20% of the force.
                
            }
            else
            {
                rb.AddForce(0, (jumpSpeed * 100 )*Time.deltaTime, 0, ForceMode.Force);
            

            }

            jumpWanted = false;
        }

       }


    private void Update()
    {
          if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded) || ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpsDone != 2) ) //jumping with space or up arrow
        {

            jumpWanted = true;

         }

    }



    //Checking if the player is grounded
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            if (!grounded)
            {

                speed += 5;
                jumpsDone = 0;
               grounded = true;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.tag)
        {
            case "obstacle":
                GameManager.Instance.Respawn(); //When we hit an obstacle we respawn
                break;
            case "finish line":
                GameManager.Instance.NextLevel(); //load the next level after we touch the finish line
                break;
            case "key":
                collision.gameObject.SetActive(false);
                GameManager.Instance.keyCount++;
                break;

        }
       

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
               if (grounded)
            {

                speed -= 5;
               grounded = false;
            }

        }
    }
}
