using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook3 : MonoBehaviour
{
    public GameObject JournalGO;
    public bool uiMode = false;
 
    public Camera theCam;
    Transform character;
    
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;
    private float rotX;
    private float rotY;
  public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    public float turnSpeed = 4.0f;
    public float ScreenPointToRay = 0.0f;

    public InteractableAsset sensedObj = null;
    public float pickupDistance = 5.0f;

    void Reset()
    {
        character = GetComponentInParent<PlayerMovement>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        uiMode = false;
    }

    void Update()
    {
        if(JournalGO.activeInHierarchy)
        {
            uiMode = true;
            UIMode(); 
        }
        else
        {
            uiMode = false;
        }
        
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            uiMode = true;
            UIMode();
        }

        if(uiMode != true)
        {
            
            MouseAiming();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            uiMode = false;
        }


        // if(Input.GetKeyDown(KeyCode.E) && sensedObj)
        // {
        //     Debug.Log("Hit Object");
        //     //Picked it up
        //     DestroyImmediate(sensedObj.gameObject);
        //     sensedObj = null;
        // }

    }


    public void MouseAiming()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        
        // rotate the camera
        
        Vector3 camRot = theCam.transform.eulerAngles;
        camRot.x = -rotX;
        theCam.transform.eulerAngles = camRot;
       

        Vector3 body = transform.eulerAngles;
        body.y += y;
        transform.eulerAngles = body;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "GameController")
        {
            if(other.GetComponent<MovingFuse>().Moving == false)
            {
                other.GetComponent<MovingFuse>().Moving = true;
            }
        }
    }

    void RaycastMouse()
    {
         //World pos of where cursor is pointing
         Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
         RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * pickupDistance, Color.cyan);    
         if(Physics.Raycast(ray, out hit, pickupDistance))
        {
            //if we hit something
            InteractableAsset obj = hit.collider.GetComponent<InteractableAsset>();
            if(obj)
            {
                sensedObj = obj;
            }
            else
            {
                sensedObj = null;
            }
        }
        else
        {
            //if we did not hit anything
            sensedObj = null;
        }
    }    

    public void UIMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        uiMode = true;
    }

    




}
