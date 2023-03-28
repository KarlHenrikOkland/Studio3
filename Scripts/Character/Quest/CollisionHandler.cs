using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        QuestManager.questManager.AddQuestItem("Enter House", 1);
    }
}
