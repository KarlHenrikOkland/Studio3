using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
  public bool journalOpen = false;

  [Header("Journal")]
  [SerializeField] public GameObject journal;

  private void Start()
  {
    journalOpen = false;
    journal.SetActive(false);
  }

  private void Update()
  {
    if(Input.GetKeyDown(KeyCode.Tab))
    {
      ActivateJournal();
    }

    if(journalOpen = true)
    {
      if(Input.GetKeyDown(KeyCode.Z))
      {
        DeactivateJournal();
      }
    }
  }



  public void ActivateJournal()
  {
    journal.SetActive(true);
    journalOpen = true;
  }

  public void DeactivateJournal()
  {
      journal.SetActive(false);
      journalOpen = false;
  }

}
