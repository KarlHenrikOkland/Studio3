using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacedCulling : MonoBehaviour
{
    [SerializeField]private LayerMask Cullable;
   

    private void Update()
    {
        // Retrieve all objects with a Renderer component in the cullableLayer
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Check if the object is within the camera's frustum
            bool isVisible = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), renderer.bounds);

            // Enable or disable the renderer based on visibility
            renderer.enabled = isVisible;
        }
    }
}
