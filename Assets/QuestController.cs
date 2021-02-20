using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
public class QuestController : MonoBehaviour
{
    public Text questLenText;
    public Dictionary<string, List<Quest>> questListJson;

    public void Awake()
    {
        questLenText.text = StateNameController.questList_len.ToString();
    }

    public void Start()
    {
        StateNameController.questList = new List<Quest>();
        if(StateNameController.questList == null)
        {
            Debug.Log("questList initialised...");
        }

        if(GameObject.Find("DataContainer") != null)
        {
            DontDestroyOnLoad(GameObject.Find("DataContainer"));
        } else
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/DataContainer")).name = "DataContainer";
            DontDestroyOnLoad(GameObject.Find("DataContainer"));
        }
    }

    // Checks if there is anything entered into the input field.
    public void getTextInputField()
    {
        TMP_InputField field_ = (TMP_InputField)GameObject.Find("Title_InputField").GetComponent<TMP_InputField>();
        if (field_.text.Length > 0)
        {
            Debug.Log("Text has been entered");
        }
        else if (field_.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }
    }

    public void setQuestTitle()
    {
        TMP_InputField field_ = GameObject.Find("Title_InputField").GetComponent<TMP_InputField>();
        TMP_Text textField_set_ = GameObject.Find("TitleSetText").GetComponent<TMP_Text>();
        
        StateNameController.currentQuest = new Quest();
        setQuestInProgress();
        StateNameController.currentQuest.title = field_.text;
        try
        {
            StateNameController.currentQuest.title = field_.text;
        }
        catch (NullReferenceException)
        {
            StateNameController.currentQuest = new Quest();
            StateNameController.currentQuest.title = field_.text;
        }
        textField_set_.text = StateNameController.currentQuest.title;
    }

    public void setQuestSDescription()
    {
        TMP_InputField field_ = GameObject.Find("SDescription_InputField").GetComponent<TMP_InputField>();
        TMP_Text textField_set_ = GameObject.Find("SDescriptionSetText").GetComponent<TMP_Text>();
        
        try
        {
            StateNameController.currentQuest.shortDescription = field_.text;
        } catch(NullReferenceException)
        {
            StateNameController.currentQuest = new Quest();
            StateNameController.currentQuest.shortDescription = field_.text;
        }

        textField_set_.text = StateNameController.currentQuest.shortDescription;
    }


    public void setQuestLDescription()
    {
        TMP_InputField field_ = GameObject.Find("LDescription_InputField").GetComponent<TMP_InputField>();
        TMP_Text textField_set_ = GameObject.Find("LDescriptionSetText").GetComponent<TMP_Text>();

        try
        {
            StateNameController.currentQuest.longDescription = field_.text;
        }
        catch (NullReferenceException)
        {
            StateNameController.currentQuest = new Quest();
            StateNameController.currentQuest.longDescription = field_.text;
        }

        textField_set_.text = StateNameController.currentQuest.longDescription;
    }

    public void setQuestInProgress()
    {
        Text QuestProgressLenText_ = GameObject.Find("QuestProgressLenText").GetComponent<Text>();
        QuestProgressLenText_.text = "yes";
    }

    public void IncrementQuestLength()
    {
        if(GameObject.Find("DataContainer") != null)
        {
            StateNameController.questList_len += 1;
            // Debug.Log("current length: "+StateNameController.questList_len.ToString());
            questLenText.text = StateNameController.questList_len.ToString();
        }

    }

    public void saveQuest()
    {
        if(StateNameController.currentQuest != null)
        {

            Debug.Log(StateNameController.currentQuest.title);
            Debug.Log(StateNameController.currentQuest.shortDescription);
            Debug.Log(StateNameController.currentQuest.longDescription);
            if (StateNameController.currentQuest.title.Length == 0)
            {
                Debug.LogError("Title Empty");
                return;
            }
            if (StateNameController.currentQuest.shortDescription.Length == 0)
            {
                Debug.Log("notice: Short Description Empty");
                // optional
                // return
            }
            if (StateNameController.currentQuest.longDescription.Length == 0)
            {
                Debug.Log("notice: Short Description Empty");
                // optional
                // return;
            }

            Debug.Log("Quest: " + StateNameController.currentQuest.title);
            Debug.Log("SDesc: " + StateNameController.currentQuest.shortDescription);
            Debug.Log("LDesc: " + StateNameController.currentQuest.longDescription);

            StateNameController.questList.Add(StateNameController.currentQuest);
            IncrementQuestLength();

            questListJson = new Dictionary<string, List<Quest>>();
            questListJson["questList"] = StateNameController.questList;
            // Dictionary<string, List<Quest> jsonObject = new Dictionary<string, List<Quest>>();
            // jsonObject["questList"] = StateNameController.questList;

            Debug.Log(JsonConvert.SerializeObject(questListJson));

            StateNameController.currentQuest = new Quest();
            TMP_InputField Title_field_ = GameObject.Find("Title_InputField").GetComponent<TMP_InputField>();
            TMP_Text Title_textField_set_ = GameObject.Find("TitleSetText").GetComponent<TMP_Text>();
            TMP_InputField SDescription_field_ = GameObject.Find("SDescription_InputField").GetComponent<TMP_InputField>();
            TMP_Text SDescription_textField_set_ = GameObject.Find("SDescriptionSetText").GetComponent<TMP_Text>();
            TMP_InputField LDescription_field_ = GameObject.Find("LDescription_InputField").GetComponent<TMP_InputField>();
            TMP_Text LDescription_textField_set_ = GameObject.Find("LDescriptionSetText").GetComponent<TMP_Text>();
            Title_field_.text = "";
            Title_textField_set_.text = "";
            SDescription_field_.text = "";
            SDescription_textField_set_.text = "";
            LDescription_field_.text = "";
            LDescription_textField_set_.text = "";

        }
        else
        {
            Debug.LogError("Quest not initialized!");
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainScene");
    }
}
