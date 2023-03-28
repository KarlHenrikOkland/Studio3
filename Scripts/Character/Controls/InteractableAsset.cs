using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Which types of interactable objects you can pick up
public enum EPickupType
{
    EPT_Note,
    EPT_Fuse,

}
public class InteractableAsset : MonoBehaviour
{
  public EPickupType pickupType = EPickupType.EPT_Fuse;
  public float amount = 1;
}
