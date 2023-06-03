using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;
using Ink.UnityIntegration;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Globals Ink File")]
    [SerializeField]private InkFile globalsInkFile;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private static DialogueManager instance;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set;}

    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
           choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>(); 
           index++;
        }
    }

    void Update()
    {
        //return right away if dialogue is not playing
        if(!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing to next line in dialogue when interaction happens
        if(InputManager.GetInstance().GetInteractPressed() || InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        
        
        ContinueStory();
        
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.0f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            //set current d
            dialogueText.text = currentStory.Continue();
            //display choices, if any, for this dialogue
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        //defensive check to support number of choices
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than UI can support. Number of choices given:" + currentChoices.Count);
        }

        int index = 0;
        //enable and initializze the choices
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }


    private IEnumerator SelectFirstChoice()
    {
        //Event System requires to clear it, then wait one frame to set current object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    //private IEnumerator SelectSecondChoice()
    //{
    //    //Event System requires to clear it, then wait one frame to set current object
    //    EventSystem.current.SetSelectedGameObject(null);
    //    yield return new WaitForEndOfFrame();
    //    EventSystem.current.SetSelectedGameObject(choices[1].gameObject);
    //}

    //private IEnumerator SelectThirdChoice()
    //{
    //    //Event System requires to clear it, then wait one frame to set current object
    //    EventSystem.current.SetSelectedGameObject(null);
    //    yield return new WaitForEndOfFrame();
    //    EventSystem.current.SetSelectedGameObject(choices[2].gameObject);
    //}


    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        //Potential fix - Bug where buttons won't be pressed
        InputManager.GetInstance().RegisterSubmitPressed(); //specific to InputManager
        ContinueStory();
        
        if (choiceIndex == 0)
        {
           InputManager.GetInstance().RegisterSubmitPressed(); //specific to InputManager
           StartCoroutine(SelectFirstChoice());
           ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);

        if(variableValue == null)
        {
            Debug.LogWarning("Ink Variable was not found as null: " + variableName);
        }
        return variableValue;
    }
}
