using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class ListsQuestsController : MonoBehaviour
{
    private readonly string sample_json_file = "Assets/Resources/sample.json";
    public string newFileName_prefix = "quests-";

    public Text questLenText;
    private GameObject panel_quests;
    
    private string exportFilePath = "";
    

    public void ResetAll()
    {
        // reset questLen text
        questLenText.text = "0";

        // get Panel/Quests
        panel_quests = GameObject.Find("Panel/Quests");

        StateNameController.questList = new List<Quest>();
        StateNameController.questList_len = 0;
        StateNameController.currentQuest = new Quest();
        StateNameController.currentCategory = "";
        
        // delete questItem gameobjects at ResetBtn
        if(panel_quests != null)
        {
            foreach (Transform child in panel_quests.gameObject.transform)
            {
                if(child.gameObject.name.StartsWith("quest") && child.gameObject.name.Contains("quest"))
                {
                    Debug.Log(child.gameObject.name);
                    Destroy(child.gameObject);
                }   
            }
        } else
        {
            Debug.LogError("no quests gameobject in panel");
        }

        Debug.Log("StateNameController.questList: " + StateNameController.questList.Count.ToString());
        Debug.Log("StateNameController.questList_len: " + StateNameController.questList_len.ToString());
        Debug.Log("StateNameController.currentCategory: " + StateNameController.currentCategory);
    }

    public void Awake()
    {
        questLenText.text = StateNameController.questList_len.ToString();
        if(File.Exists(sample_json_file))
        {
            string sample_fileContent = File.ReadAllText(sample_json_file);
            if(sample_fileContent.Length > 0)
            {
                QuestList QuestList_ = JsonConvert.DeserializeObject<QuestList>(sample_fileContent);
                if (QuestList_.questList.Count > 0)
                {
                    Debug.Log(QuestList_.questList.Count + " quests found");
                    questLenText.text = QuestList_.questList.Count.ToString();
                    int decrement_ = 140;
                    int current_y = 0;
                    for(int i = 0; i < QuestList_.questList.Count;i++)
                    {
                        Instantiate(Resources.Load<GameObject>("Prefabs/QuestTemplate"), GameObject.Find("Panel/Quests").transform).name = "quest "+i.ToString();
                        GameObject.Find("quest " + i.ToString()).transform.localPosition = new Vector3(0, current_y, 0);
                        GameObject.Find("quest " + i.ToString() + "/QuestIDText").GetComponent<TextMeshProUGUI>().text = "ID: "+QuestList_.questList[i].Id;
                        GameObject.Find("quest " + i.ToString() + "/QuestTitleText").GetComponent<TextMeshProUGUI>().text = QuestList_.questList[i].title;
                        GameObject.Find("quest " + i.ToString() + "/QuestSDescriptionText").GetComponent<TextMeshProUGUI>().text = "# " + QuestList_.questList[i].shortDescription;
                        GameObject.Find("quest " + i.ToString() + "/QuestLDescriptionText").GetComponent<TextMeshProUGUI>().text = "# " + QuestList_.questList[i].longDescription;
                        current_y -= decrement_;
                    }
                }
            } else
            {
                Debug.LogError("Sample file empty!");
                Debug.LogError(" need a:");
                Debug.LogError("{  \"questList\": [    {      \"title\": \"dawdawd\",      \"shortDescription\": \"dawdawdawdawd\",      \"longDescription\": \"awdawdawdawd\",      \"Id\": 0    }  ]}");
            }
        } else
        {
            if(StateNameController.questList == null)
            {
                StateNameController.questList = new List<Quest>();
            }

            if(StateNameController.questList.Count > 0)
            {
                List<Quest> questList = StateNameController.questList;

                Debug.Log("State: " + questList.Count.ToString());
                Debug.Log(questList.Count + " quests found");

                questLenText.text = questList.Count.ToString();

                int decrement_ = 140;
                int current_y = 0;
                for (int i = 0; i < questList.Count; i++)
                {
                    Instantiate(Resources.Load<GameObject>("Prefabs/QuestTemplate"), GameObject.Find("Panel/Quests").transform).name = "quest " + i.ToString();
                    
                    // set position of questItem Element
                    GameObject.Find("quest " + i.ToString()).transform.localPosition = new Vector3(0, current_y, 0);

                    // get placed GameObject-TMPro-TextFields
                    TextMeshProUGUI text_id = GameObject.Find("quest " + i.ToString() + "/QuestIDText").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI text_title = GameObject.Find("quest " + i.ToString() + "/QuestTitleText").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI text_shortDescription = GameObject.Find("quest " + i.ToString() + "/QuestSDescriptionText").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI text_longDescription = GameObject.Find("quest " + i.ToString() + "/QuestLDescriptionText").GetComponent<TextMeshProUGUI>();

                    // fill informations
                    if (text_id != null)
                    {
                        Debug.Log(questList[i].Id);
                        text_id.text = questList[i].Id.ToString();
                    }
               
                    if (text_title != null)
                    {
                        Debug.Log(questList[i].title);
                        text_title.text = questList[i].title;
                    }
                   
                    if (text_shortDescription != null)
                    {
                        Debug.Log(questList[i].shortDescription);
                        text_shortDescription.text = questList[i].shortDescription;
                    }

                    if(text_longDescription != null)
                    {
                        Debug.Log(questList[i].longDescription);
                        text_longDescription.text = questList[i].longDescription;
                    }
                    // decrement y position
                    current_y -= decrement_;
                }
            } else
            {
                try
                {
                    // delete old questItem-gameobjects
                    foreach (GameObject child_go in GameObject.Find("Panel/Quests").GetComponentsInChildren<GameObject>())
                    {
                        Debug.Log(child_go.name);
                    }
                } catch(ArgumentException)
                {
                    Debug.Log(" no items ");
                }
            }
        }
    }

    private string generate_fileName()
    {
        Int32 int32_timestamp = MyUnityUtils.unixTimestamp();
        string timestamp_suffix_ = int32_timestamp.ToString();
        return newFileName_prefix + timestamp_suffix_ + ".json";
    }

    public void ExportAsFile()
    {
        string newFilename_ = generate_fileName();

        exportFilePath = Application.persistentDataPath + "/" + newFilename_;
        if (!Application.isEditor)
        {
            exportFilePath = "/mnt/sdcard/Download/" + newFilename_;
        }

        if(!File.Exists(exportFilePath))
        {
            Dictionary<string, List<Quest>> questListJson = new Dictionary<string, List<Quest>>
            {
                ["questList"] = StateNameController.questList
            };
            File.WriteAllText(exportFilePath, JsonConvert.SerializeObject(questListJson));
            GameObject.Find("SavedStatusText").GetComponent<TextMeshProUGUI>().text = exportFilePath;

            panel_quests = GameObject.Find("Panel/Quests");
            foreach (Transform child in panel_quests.gameObject.transform)
            {
                if (child.gameObject.name.StartsWith("quest") && child.gameObject.name.Contains("quest"))
                {
                    Debug.Log(child.gameObject.name);
                    Destroy(child.gameObject);
                }
            }
            ResetAll();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainScene");
    }
}
