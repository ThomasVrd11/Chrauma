using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GetReaper : MonoBehaviour
{
	[SerializeField] Transform tpDestination;
	[SerializeField] GameObject player;
	[SerializeField] GameObject effects;
	[SerializeField] List<GameObject> playerModels;
	[SerializeField] CinemachineVirtualCamera VCGetReaper;
	[SerializeField] CinemachineVirtualCamera VCGetReaper2;
	[SerializeField] CinemachineVirtualCamera VCFollowPlayer;
	[SerializeField] Animator ghostAnimator;
	[SerializeField] Volume volume;
	ColorAdjustments colorAdjustments;
	[SerializeField] MeshRenderer weaponMeshRenderer;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			player.GetComponent<CharacterController>().enabled = false;
			player.GetComponent<CharacterControls>().enabled = false;
			effects.SetActive(true);
			StartCoroutine(TurnIntoReaper());
		}
	}

	IEnumerator TurnIntoReaper()
	{
		player.transform.LookAt(VCGetReaper.gameObject.transform.position);
		VCGetReaper.gameObject.SetActive(true);
		yield return new WaitForSeconds(2);
		ghostAnimator.SetTrigger("Surprised");
		yield return new WaitForSeconds(3);
		ghostAnimator.SetTrigger("Surprised");
		weaponMeshRenderer.enabled = false;
		if (volume.profile.TryGet(out colorAdjustments))
		{
			StartCoroutine(BringColorBack(colorAdjustments));
		}
		playerModels[0].SetActive(true);
		playerModels[2].SetActive(true);
		yield return new WaitForSeconds(0.5f);
		playerModels[1].SetActive(false);
		yield return new WaitForSeconds(0.5f);
		player.GetComponent<Animator>().SetTrigger("Skill1Stage");
		yield return new WaitForSeconds(4);
		VCGetReaper2.gameObject.SetActive(true);
		yield return new WaitForSeconds(8);
		player.transform.position = tpDestination.position;
		yield return new WaitForSeconds(3);
		VCFollowPlayer.m_Lens.NearClipPlane = -20;
		VCGetReaper2.gameObject.SetActive(false);
		VCGetReaper.gameObject.SetActive(false);
		yield return new WaitForSeconds(1);
		player.GetComponent<CharacterControls>().enabled = true;
		player.GetComponent<CharacterController>().enabled = true;
		Destroy(gameObject);
		yield return null;
	}
	IEnumerator BringColorBack(ColorAdjustments colorAdjustments)
	{
		float currentTime = 0f;
		while (currentTime <= 2.5f)
		{
			colorAdjustments.saturation.value = Mathf.Lerp(-30, 30, currentTime / 2.5f);
			currentTime += Time.deltaTime;
			yield return null;
		}
	}
}
