using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.AI;

public class DoorCinematic : MonoBehaviour
{
    public CinemachineVirtualCamera perspectiveCamera;
    public CinemachineVirtualCamera orthographicCamera;
	public CinemachineVirtualCamera cameraDoor2;
	[SerializeField] private CharacterController characterController;
	[SerializeField] private NavMeshAgent navAgentPlayer;
	[SerializeField] private GameObject waypoint1;
	[SerializeField] private GameObject doorLeft;
	[SerializeField] private GameObject doorRight;
	private bool hasBeenTriggered = false;
    public float time = 5.0f;

    private Camera mainCamera;
    private bool isOrthographic = true;

    void Start()
    {
        mainCamera = Camera.main;

    }
	private void OnTriggerEnter(Collider other) {
		if(other.name == "Player" && isOrthographic && !hasBeenTriggered)
		{
			SwitchCam(isOrthographic);
			StartCoroutine(StopPlayerForASec(time));
			hasBeenTriggered = true;
		}
	}
	private void SwitchCam(bool isOrthographic)
	{
		if(isOrthographic)
		{
			perspectiveCamera.gameObject.SetActive(true);
			orthographicCamera.gameObject.SetActive(false);
		}
		else
		{
			cameraDoor2.gameObject.SetActive(false);
			orthographicCamera.gameObject.SetActive(true);
		}
	}
	IEnumerator StopPlayerForASec(float timeStopped){
		float elapsedTime = 0;

		Quaternion startRotLeft = doorLeft.transform.rotation;
		Quaternion startRotRight = doorRight.transform.rotation;
		Quaternion endRotLeft = Quaternion.Euler(0, -555f,0);
		Quaternion endRotRight = Quaternion.Euler(0, 5f,0);
		characterController.enabled = false;
		Debug.Log("cc: " + characterController.enabled);

		while(elapsedTime < 3f){
			float t = elapsedTime / 3f;
			doorLeft.transform.rotation = Quaternion.Slerp(startRotLeft,endRotLeft,t);
			doorRight.transform.rotation = Quaternion.Slerp(startRotRight,endRotRight,t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		navAgentPlayer.enabled = true;
		navAgentPlayer.SetDestination(waypoint1.transform.position);
		StartCoroutine(AnimateDoorCamera());
		yield return new WaitForSeconds(timeStopped);
		characterController.enabled = true;
		Debug.Log("cc: " + characterController.enabled);
		navAgentPlayer.enabled = false;
		orthographicCamera.m_Lens.NearClipPlane = -7.9f;
		SwitchCam(false);
	}
	IEnumerator AnimateDoorCamera()
	{
		yield return new WaitForSeconds(2);
		perspectiveCamera.gameObject.SetActive(false);
		cameraDoor2.gameObject.SetActive(true);

	}

}
