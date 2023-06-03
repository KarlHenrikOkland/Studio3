using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //moving primitives
    public float walkSpeed = 5.0f;
    public float strafeSpeed = 5.0f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 15.0f;

    //noclip
    private bool isNoclip = false;
    public float noclipWalkSpeed = 10.0f;
    public float noclipStrafeSpeed = 10.0f;
    public float noclipSprintMultiplier = 3.0f;

    //speed check
    public float velocityThreshold = 1.0f;

    //moving booleans
    private bool isSprinting = false;
    private bool isJumping = false;


    public GameObject JournalGO;

    void FixedUpdate()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            Jump();
        }
    }

    void Update()
    {
        if(JournalGO.activeInHierarchy)
        {
            return; 
        }
        else if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.V))
            {
                if(!isNoclip)
                {
                    isNoclip = true;
                    GetComponent<Collider>().enabled = false;
                    GetComponent<Rigidbody>().useGravity = false;
                }
                else
                {
                    isNoclip = false;
                    GetComponent<Collider>().enabled = true;
                    GetComponent<Rigidbody>().useGravity = true;
                }
            }    
        }
        if(isNoclip)
        {
        Noclip();
        }
        else
        {
        controllerMoving();
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        
    }

    

    void controllerMoving()
    {
        float fwd = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        if(isSprinting)
        {
            fwd *= sprintMultiplier;
        }

        //applying strafe into controller. The player is moving (translated on the X-axis)
        strafe *= strafeSpeed * Time.deltaTime;
        transform.Translate(strafe, 0, 0);

        Vector3 pos = transform.position;
        Vector3 forward = transform.forward;

        forward *= fwd * Time.deltaTime * walkSpeed;
        pos += forward;
        transform.position = pos;
    }

    public void Jump() 
    {
        if (GetComponent<Rigidbody>().velocity.y < velocityThreshold) 
        {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void Noclip()
    {
        float fwd = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        if(isSprinting)
        {
            fwd *= noclipSprintMultiplier;
        }

        //move relative to camera forward
        Vector3 cameraFwd = Camera.main.transform.forward;
        cameraFwd = cameraFwd.normalized;

        Vector3 pos = transform.position;
        Vector3 forward = cameraFwd * fwd * Time.deltaTime * noclipWalkSpeed;
        Vector3 side = transform.right * strafe * Time.deltaTime * noclipStrafeSpeed;

        if(Input.GetKey(KeyCode.Space))
        {
            forward += new Vector3(0, Time.deltaTime * noclipWalkSpeed, 0);
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            forward -= new Vector3(0, Time.deltaTime * noclipWalkSpeed, 0);
        }

        pos += forward + side;
        transform.position = pos;
    }
}
