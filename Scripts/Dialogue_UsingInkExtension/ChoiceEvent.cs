// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystem;

// public class ChoiceEvent : MonoBehaviour
// {
//     [Serializable]
//     public class MyEvent : UnityEvent<string,GameObject> {}
//     [Serializable]
//     public class MyEvent : UnityEvent {}

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         string dialogueName = ((Ink.Runtime.StringValue)DialogueManager
//         .GetInstance()
//         .GetVariableState("missing_Girl_name")).value;

//         switch(dialogueName)
//         {
//             case "":
//             break;
//             case "Choice1":
//             MyEvent OnEvent;
//                 break;
//             case "Choice2":
//             MyEvent OnEvent;
//                 break;
//             case "Choice3":
//             MyEvent OnEvent;
//                 break;
//             case "Choice4":
//             MyEvent OnEvent;
//                 break;
//             default:
//             Debug.LogWarning("Choice not handled by switch statement: " + dialogueName);
//             break;

//         }
//     }
// }
