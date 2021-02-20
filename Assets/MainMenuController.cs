using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text questLengthText;

    private void Awake()
    {
        questLengthText.text = StateNameController.questList_len.ToString();
        GameObject.Find("NewItemButton").GetComponent<Button>().enabled = false;
    }

    public void LoadGame(string input)
    {
        StateNameController.currentCategory = input;
        SceneManager.LoadScene("NewQuest");
    }

    public void LoadListQuests(string input)
    {
        StateNameController.currentCategory = input;
        SceneManager.LoadScene("ListQuests");
    }

    public void DebugLength()
    {
        GameObject.Find("");
    }
}
