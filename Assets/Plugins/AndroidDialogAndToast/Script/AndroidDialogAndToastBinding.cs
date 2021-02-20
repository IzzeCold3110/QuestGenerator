using UnityEngine;
using System.Collections;
using System;

public class AndroidDialogAndToastBinding : MonoBehaviour {

	
	private static AndroidJavaObject jo;
	private static AndroidJavaClass jc;
	public static AndroidDialogAndToastBinding instance;
	
	void Start () {
		instance = this;
		
		if( ( gameObject != null ) && ( gameObject.name != null ) )
		{
			if( !Application.isEditor )
			{
				jc = new AndroidJavaClass ("com.werplay.androidutilities.CallActivity"); 
				jo = jc.CallStatic <AndroidJavaObject> ("getInstance");
				jo.Call ("setObjectName", gameObject.name);
			}
		}
	}

	//Pop up the dialog box with three buttons
	//Set arguments to change the text of dialog box
	//set 'iconName' as the name of the icon file in drawable folder. No icon will be set if the string 'iconName' will not match with the name of the file.
	//set a 'Tag' to identifiy your dialog box in callback functions.
	//on pressing positive button (Left Most), 'pressedPositive' function will be called
	//on pressing neutral button (Mid One), 'pressedNeutral' function will be called
	//on pressing negative button (Right Most), 'pressedNegative' function will be called
	public void dialogBoxWithThreeButtons(string title, string message, string positiveButtonText, string negativeButtonText, string neutralButtonText,
										string iconName, string Tag)
	{
		if( !Application.isEditor )
			jo.Call("dialogBoxWithThreeButtons", title, message, positiveButtonText, negativeButtonText, neutralButtonText, iconName, Tag);
	}

	//Pop up the dialog box with two buttons
	//Set arguments to change the text of dialog box
	//set 'iconName' as the name of the icon file in drawable folder. No icon will be set if the string 'iconName' will not match with the name of the file.
	//set a 'Tag' to identifiy your dialog box in callback functions. 
	//on pressing positive button (Left Most), 'pressedPositive' function will be called
	//on pressing negative button (Right Most), 'pressedNegative' function will be called
	public void dialogBoxWithTwoButtons(string title, string message, string positiveButtonText, string negativeButtonText, string iconName, string Tag)
	{
		if( !Application.isEditor )
			jo.Call("dialogBoxWithTwoButtons", title, message, positiveButtonText, negativeButtonText, iconName, Tag);
	}

	//Pop up the dialog box with one button
	//Set arguments to change the text of dialog box
	//set 'iconName' as the name of the icon file in drawable folder. No icon will be set if the string 'iconName' will not match with the name of the file.
	//set a 'Tag' to identifiy your dialog box in callback functions.
	//on pressing the dialog single button, 'pressedPositive' function will be called
	public void dialogBoxWithOneButton(string title, string message, string positiveButtonText, string iconName, string Tag)
	{
		if( !Application.isEditor )
			jo.Call("dialogBoxWithOneButton", title, message, positiveButtonText, iconName, Tag);
	}

	//Show the text message in toast for long duration
	public void toastLong(string message)
	{
		if( !Application.isEditor )
			jo.Call("toastLong", message);
	}

	//Show the text message in toast for short duration
	public void toastShort(string message)
	{
		if( !Application.isEditor )
			jo.Call("toastShort", message);
	}

	///////////////////////////////////////////// Call-backs //////////////////////////////////////////////
	public static event Action <string> pressedPositiveEvent;	
	public static event Action <string> pressedNegativeEvent;
	public static event Action <string> pressedNeutralEvent;	

	public void pressedPositive(string Tag){

		if(pressedPositiveEvent != null)
			pressedPositiveEvent(Tag);

		Debug.Log("pressedPositive " + Tag);

		Application.Quit (); 
	}

	public void pressedNegative(string Tag){

		if(pressedNegativeEvent != null)
			pressedNegativeEvent(Tag);

		Debug.Log("pressedNegative " + Tag);
	}

	public void pressedNeutral(string Tag){

		if(pressedNeutralEvent != null)
			pressedNeutralEvent(Tag);

		Debug.Log("pressedNeutral " + Tag);
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////

}
