using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class IntroManager : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera introCam;
	[SerializeField] CinemachineVirtualCamera mainCam;
	[SerializeField] Canvas Tutorials;
	[SerializeField] GameObject lookingAroundTarget;
	[SerializeField] Animator lilGhostAnimator;
	[SerializeField] GameObject[] characterDisplay;
	//0-reaper body 1-scythe 2-lilghost body

	[SerializeField] GameObject player;
	[SerializeField] float lookAroundTime;
	[SerializeField] Renderer ghostRenderer;
	[SerializeField] CharacterControls playerControls;
	[SerializeField] TutorialTriggers tutorialTriggers;
	[SerializeField] GameObject introTxt;
	[SerializeField] TMP_Text first_text;
    [SerializeField] TMP_Text second_text;
	private Vector3 posLook1 = new Vector3(-129.06f, 23.90f, -16.44f);
	private Vector3 posLook2 = new Vector3(-123f, 19f, -15f);

    void Start()
	{
		StartCoroutine(Initialize());
	}

	private IEnumerator Initialize()
	{
		yield return null;
		introCam.gameObject.SetActive(true);
		mainCam.gameObject.SetActive(false);
		characterDisplay[0].SetActive(false);
		characterDisplay[1].SetActive(false);
		characterDisplay[2].SetActive(true);
		playerControls.enabled = false;
		StartCoroutine(WhatHappened());
	}
	IEnumerator WhatHappened()
	{
		/*
		active le panel introtxt
		lance coroutine fade, avec le premier text en argument, boolean true (fade in)
		attend 3sec
		relance coroutine fade avec le premier texte en argument, bool false (fade out)
		attend 3 sec
		refait la même chose avec le deuxieme texte
		lance l'intro de base
		*/
		introTxt.gameObject.SetActive(true);
		StartCoroutine(Fade(first_text, true));
		yield return new WaitForSeconds(3);
		StartCoroutine(Fade(first_text, false));
		yield return new WaitForSeconds(3);
		StartCoroutine(Fade(second_text, true));
		yield return new WaitForSeconds(3);
		StartCoroutine(Fade(second_text, false));
		yield return new WaitForSeconds(3);
		introTxt.gameObject.SetActive(false);
		StartCoroutine(IntroSceneStart());
		yield return null;
	}

	IEnumerator Fade(TMP_Text textToFade, bool inOut)
	{
		/*
		check le bool inOut, si true opacité ira de 0 a 1, sinon de 1 a 0
		tant que le fadetime n'est pas atteind
		fait avancer la fade value par le temps passé (comme si chaque unité entre le début est le fadetime equivaut a 1%)
		remet bien l'alpha a la fadevalue a la fin,pour etre sur d'etre a 1 ou a 0 en fonction de fade in ou fade out
		*/
		float fadeTime = 1.5f;
        float elapsedTime = 0f;
		int startValue = 0;
		int endValue = 0;
		if (inOut)
		{
			endValue = 1;
		}
		else
		{
			startValue = 1;
		}
		while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float fadeValue = Mathf.Lerp(startValue, endValue, elapsedTime / fadeTime);
            textToFade.alpha = fadeValue;
            yield return null;
        }
		yield return null;
	}
	IEnumerator IntroSceneStart()
	{
		yield return new WaitForSeconds(1.5f);
		introCam.gameObject.SetActive(false);
		mainCam.gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);
		yield return StartCoroutine(Apparition());
		lookAround(lookingAroundTarget.transform.position);
		yield return new WaitForSeconds(lookAroundTime);
		lookAround(posLook2);
		yield return new WaitForSeconds(lookAroundTime);
		lookAround(posLook1);
		yield return new WaitForSeconds(0.5f);
		lilGhostAnimator.SetTrigger("Surprised");
		yield return new WaitForSeconds(0.7f);
		playerControls.enabled = true;
		tutorialTriggers.StartMovTuto();
		yield return null;
	}

    IEnumerator Apparition()
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        float dissolveTime = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            float dissolveValue = Mathf.Lerp(0, 1, elapsedTime / dissolveTime);

            ghostRenderer.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_Dissolve", dissolveValue);
            ghostRenderer.SetPropertyBlock(propBlock);

            yield return null;
        }
        ghostRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_Dissolve", 1);
        ghostRenderer.SetPropertyBlock(propBlock);
    }
	private void lookAround(Vector3 pos)
	{
		lookingAroundTarget.transform.position = pos;
		player.transform.LookAt(lookingAroundTarget.transform.position);
	}

}
