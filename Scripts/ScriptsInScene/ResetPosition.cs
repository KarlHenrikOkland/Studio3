using UnityEngine;
using UnityEngine.UI;

public class ResetPosition : MonoBehaviour
{
    public GameObject targetObject;
    public Transform resetTransform;
    public Button resetButton;

    private void Start()
    {
        // Add a listener to the button click event
        resetButton.onClick.AddListener(resetPosition);
    }

    private void resetPosition()
    {
        if (targetObject != null && resetTransform != null)
        {
            // Reset the position of the target object to the reset transform
            targetObject.transform.position = resetTransform.position;
            targetObject.transform.rotation = resetTransform.rotation;
            targetObject.transform.localScale = resetTransform.localScale;
        }
        else
        {
            Debug.LogWarning("Target object or reset transform is not assigned.");
        }
    }
}
