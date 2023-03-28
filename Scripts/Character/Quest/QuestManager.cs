using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    
    public List<Quest> questList = new List<Quest>();  //Master Quest List
    public List<Quest> currentQuestList = new List<Quest>();  //Current Quest List

    //private vars for QuestObject

    void Awake() 
    {
        if(questManager == null)
        {
            questManager = this;
        }
        else if(questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public void QuestRequest(QuestObject NPC_QuestObject)
    {
        //checking for available quests
        if(NPC_QuestObject.availableQuestIDs.Count > 0)
        {
            for(int i = 0; i < questList.Count; i++)
            {
                for(int j = 0; j < NPC_QuestObject.availableQuestIDs.Count; j++)
                {
                    if(questList[i].id == NPC_QuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestState.AVAILABLE)
                    {
                        Debug.Log(("Quest ID: " + NPC_QuestObject.availableQuestIDs[j] + " " + questList[i].progress));
                        //TEST
                        AcceptQuest(NPC_QuestObject.availableQuestIDs[j]);

                        //Quest UI Manager
                    }
                }
            }
        }

            //Active Quests
            for(int i = 0; i < questList.Count; i++)
            {
                for(int j = 0; j < NPC_QuestObject.availableQuestIDs.Count; j++)
                {
                   if(currentQuestList[i].id == NPC_QuestObject.availableQuestIDs[j] && currentQuestList[i].progress == Quest.QuestState.ACCEPTED || currentQuestList[i].progress == Quest.QuestState.COMPLETED)
                   {
                        Debug.Log("Quest ID: " + NPC_QuestObject.receivableQuestIDs[j] + " is " + currentQuestList[i].progress);
                        //quest UI manager

                   }
                }
            }
    }

    //Accept Quest
    public void AcceptQuest(int questID)
    {
        for(int i = 0; i<questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.QuestState.AVAILABLE)
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestState.ACCEPTED;
            }
        }
    }

    //Give up Quest
    public void GiveUpQuest(int questID)
    {
        for(int i = 0; i<questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.QuestState.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestState.AVAILABLE;
                currentQuestList[i].questObjectiveCount = 0;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }
    }
    //Complete Quest
    public void CompleteQuest(int questID)
    {
        for(int i = 0; i<questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.QuestState.COMPLETED)
            {
                currentQuestList[i].progress = Quest.QuestState.FINISHED;
                currentQuestList.Remove(currentQuestList[i]);

                //REWARD

            }
        }
        //check for chain quests
        CheckChainQuests(questID);
    }

    //CHECK CHAIN QUESTS
    void CheckChainQuests(int questID)
    {
        int tempID = 0;
        for(int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }

        if(tempID > 0)
        {
            for(int i = 0; i < questList.Count; i++)
            {
                if(questList[i].id == tempID && questList[i].progress == Quest.QuestState.NOT_AVAILABLE)
                {
                    questList[i].progress = Quest.QuestState.AVAILABLE;
                }
            }
        }
    }




    //Add Items  
    public void AddQuestItem(string QuestObjective, int itemAmount)
    {
        for(int i = 0; i < currentQuestList.Count; i++)
        {
            if(currentQuestList[i].questObjective == QuestObjective && currentQuestList[i].progress == Quest.QuestState.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }

            if(currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement && currentQuestList[i].progress == Quest.QuestState.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestState.COMPLETED;
            }
        }
    }
    //Remove Items



    //Bools for state checking
    public bool RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questList.Count;i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.QuestState.AVAILABLE)
            {
                return true;
            }

        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for(int i = 0; i < questList.Count;i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.QuestState.ACCEPTED)
            {
                return true;
            }

        }
        return false;
    }

    public bool RequestCompletedQuest(int questID)
    {
        for(int i = 0; i < questList.Count;i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.QuestState.COMPLETED)
            {
                return true;
            }

        }
        return false;
    }


    //bools 2
    public bool CheckAvailableQuests(QuestObject NPCQuestObject)
    {
        for(int i = 0; 1 < questList.Count; i++)
        {
            for(int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
            {
                if(questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestState.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject NPCQuestObject)
    {
        for(int i = 0; 1 < questList.Count; i++)
        {
            for(int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if(questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestState.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompletedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; 1 < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestState.COMPLETED)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
