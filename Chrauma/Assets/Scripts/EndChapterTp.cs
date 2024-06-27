using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChapterTp : MonoBehaviour
{
	[SerializeField] private int nextMapIndex;
	[SerializeField] private Spawner spawner;
	private CapsuleCollider cc;
	private bool hasAppeared = false;


	private void Awake()
	{
		cc = GetComponent<CapsuleCollider>();
	}

	private void Update()
	{
		if (spawner)
		{
			if (spawner.numberOfKilledEnnemies == 3 && !hasAppeared)
			{
				StartCoroutine(PortalAppear());
			}
		}

	}
	private void OnTriggerEnter(Collider other)
	{
		GameManager.instance.SwitchScene(nextMapIndex);
	}
	IEnumerator PortalAppear()
	{
		Vector3 initialScale = gameObject.transform.localScale;
		float currentTime = 0f;
		while (currentTime < 2.5f)
		{
			float newScaleY = Mathf.Lerp(initialScale.y, 1, currentTime / 2.5f);

			transform.localScale = new Vector3(initialScale.x, newScaleY, initialScale.z);
			currentTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = new Vector3(initialScale.x, 1, initialScale.z);
		cc.enabled = true;
		hasAppeared = true;
	}
}

