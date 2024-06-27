using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	UILinker uiLinker;
	[SerializeField] GameObject pausePanel;
	[SerializeField] GameObject confirmPanel;
	[SerializeField] TMP_Text confirmText;
	[SerializeField] GameObject saveConfirmButton;
	[SerializeField] GameObject loadConfirmButton;
	[SerializeField] GameObject quitConfirmButton;

	bool pauseState = false;

	private void Awake()
	{
		uiLinker = GetComponent<UILinker>();
	}
	public void PressPause()
	{
		pauseState = !pauseState;
		HandlePause();
	}
	
	private void HandlePause()
	{
		if (pauseState)
		{
			Time.timeScale = 0;
			pausePanel.SetActive(true);
		} else
		{
			Time.timeScale = 1;
			pausePanel.SetActive(false);
		}
	}
	public void ConfirmChoice(int choice)
	{
		confirmPanel.SetActive(true);
		switch (choice)
		{
			case 0:
				confirmText.text = "This will override your previous save.\nAre you sure?";
				DeactivateUnnecessaryButtons();
				saveConfirmButton.SetActive(true);
				break;
			case 1:
				confirmText.text = "You will loose all unsaved data.\nAre you sure?";
				DeactivateUnnecessaryButtons();
				loadConfirmButton.SetActive(true);
				break;
			case 2:
				confirmText.text = "You will loose all unsaved data.\nAre you sure?";
				DeactivateUnnecessaryButtons();
				quitConfirmButton.SetActive(true);
				break;
		}
	}
	private void DeactivateUnnecessaryButtons()
	{
		saveConfirmButton.SetActive(false);
		loadConfirmButton.SetActive(false);
		quitConfirmButton.SetActive(false);
	}
}
