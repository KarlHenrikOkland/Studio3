using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Transform aircraftMesh;
    [SerializeField]
    private Transform mousePosition;
    [SerializeField]
    private Transform cameraRig;
    [SerializeField]
    private Transform MainCamera;

    [SerializeField]
    public float cameraSmoothSpeed = 10.0f;
    [SerializeField]
    public float mouseSensitivity = 3.0f;
    
    [SerializeField]
    public float boreSightDistance = 1000f;

   private Vector3 planeDirection = Vector3.forward;

   private bool isMouseFrozen = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isMouseFrozen = true;
            planeDirection = mousePosition.forward;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isMouseFrozen = false;
            mousePosition.forward = planeDirection;
        }
        
        cameraRotation();
        
        planeRotation();
    }

    private Vector3 GetFrozenMouseAim()
    {
        if(mousePosition != null)
        {
           return mousePosition.position + (planeDirection * boreSightDistance); 
        }
        else
        {
            return transform.forward * boreSightDistance;
        }
    }

    public Vector3 mousePosTarget
    {
        get
        {
            if(mousePosition != null)
            {
                return isMouseFrozen
                ? GetFrozenMouseAim()
                : mousePosition.position + (mousePosition.forward * boreSightDistance);
            }
            else
            {
                return transform.forward * boreSightDistance;
            }
        }
    }

    public void cameraRotation()
    {
        transform.position = aircraftMesh.position;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        mousePosition.Rotate(MainCamera.right, mouseY, Space.World);
        mousePosition.Rotate(MainCamera.up, mouseX, Space.World);

        Vector3 upVector = (Mathf.Abs(mousePosition.forward.y) > 1f) ? cameraRig.up : Vector3.up;
        Quaternion targetRotation = Quaternion.LookRotation(mousePosition.forward, upVector);
        cameraRig.rotation = Quaternion.Slerp(cameraRig.rotation, targetRotation, cameraSmoothSpeed * Time.deltaTime);
    }
    
    
    
    public void planeRotation()
    {
        // Calculate the target position towards the mouse position
        Vector3 targetPosition = mousePosTarget;
        Vector3 currentPosition = aircraftMesh.position;

        // Calculate the direction towards the target position
        Vector3 direction = targetPosition - currentPosition;

        // Calculate the target rotation towards the target position
        Quaternion targetRotation = Quaternion.LookRotation(direction, aircraftMesh.up);

        // Interpolate towards the target rotation using Slerp
        float lerpSpeed = Mathf.Clamp01(aircraftMesh.GetComponent<Rigidbody>().velocity.magnitude / 20f); // adjust the divisor to change the rate of rotation
        aircraftMesh.rotation = Quaternion.Slerp(aircraftMesh.rotation, targetRotation, lerpSpeed * Time.deltaTime);
    
    }

    



}
