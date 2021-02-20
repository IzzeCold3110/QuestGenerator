using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; 
public class GuiManager : MonoBehaviour {
	
	
	void Update () 
	{
		ButtonHeight = Screen.height/5;
		ButtonWidth =  Screen.width/2-20;
	}
	
	float ButtonHeight = Screen.height/5;
	float ButtonWidth = Screen.width/2-20;
	void OnGUI ()
	{
		GUI.color = Color.black;
		float ButtonPosx = 10;
		float ButtonPosy = 10;
		GUI.color = Color.white;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Short Toast"))
		{
			AndroidDialogAndToastBinding.instance.toastShort("Test Message");
		}
		
		ButtonPosx += ButtonWidth+10;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Long Toast"))
		{
			AndroidDialogAndToastBinding.instance.toastLong("Test Message");
		}

		ButtonPosy += ButtonHeight+10;
		ButtonPosx = 10;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Dialog Box With One Button"))
		{
			AndroidDialogAndToastBinding.instance.dialogBoxWithOneButton("Test Title", "Test Message", "OK", "", "Tag 001");
		}

		ButtonPosx += ButtonWidth+10;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Dialog Box With Two Buttons"))
		{
			AndroidDialogAndToastBinding.instance.dialogBoxWithTwoButtons("Test Title", "Test Message", "Yes", "No", "test_dialog_icon", "Tag 002");
		}

		ButtonPosy += ButtonHeight+10;
		ButtonPosx = 10;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Dialog Box With Three Buttons"))
		{
			AndroidDialogAndToastBinding.instance.dialogBoxWithThreeButtons("Test Title", "Test Message", "Yes", "No", "Neutral", "test_dialog_icon", "Tag 003");
		}

	}
}