using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestObject : MonoBehaviour
{
    private bool inTrigger = false;

    public List<int> availableQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();


    public GameObject questMarker;
    public Image theImage; 

    public Sprite questAvailableSprite;
    // Start is called before the first frame update
    void Start()
    {
        SetQuestMarker();
    }

    void SetQuestMarker()
    {
        if(QuestManager.questManager.CheckCompletedQuests(this))
        {
            questMarker.SetActive(true);
            theImage.sprite = questAvailableSprite;
            theImage.color = Color.white;
        }
        else if(QuestManager.questManager.CheckAvailableQuests(this))
        {
            questMarker.SetActive(true);
            theImage.sprite = questAvailableSprite;
            theImage.color = Color.white;
        }
        else if(QuestManager.questManager.CheckAcceptedQuests(this))
        {
            questMarker.SetActive(true);
            theImage.sprite = questAvailableSprite;
            theImage.color = Color.yellow;
        }
        else
        {
            questMarker.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            //Quest UI Manager¨
            QuestManager.questManager.QuestRequest(this);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            inTrigger = true;
        }    

    }

    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            inTrigger = false;
            
        }    
        
    }
}
