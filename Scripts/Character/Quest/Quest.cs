using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestState 
    {
        NOT_AVAILABLE,
        AVAILABLE,
        ACCEPTED,
        COMPLETED,
        FINISHED
    }
    public bool isActive;
    public int id;      //ID number for quest
    public string title;    //title for quest
    public string description;      //description of quest
    public string hint;         //hint for guiding player if stuck
    public int infoReward;      //what you receive as reward, information wise
    public int goldReward;      //gold reward amount
    public int itemReward;      //any other kind of rewards


    //quest ints
    public string questObjective;       //name of quest objective, also used for events
    public int nextQuest;       //if it is connected to chain quest it will activate
    public int questObjectiveCount;     //amount of objectives in quest
    public int questObjectiveRequirement;       //required amount to complete quest



    public QuestState progress; //state of the current quest (enum)
}
