using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateAfterXkills : MonoBehaviour
{
    public Spawner spawner;
    public int nbrKillsToOpen;
    private BoxCollider bc;

    private void Start() {
        bc = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (spawner.numberOfKilledEnnemies >= nbrKillsToOpen)
        {
            StartCoroutine(PortalDisappear());
        }
    }
    IEnumerator PortalDisappear()
	{
        bc.enabled = false;
		Vector3 initialScale = transform.localScale;
		float currentTime = 0f;
		while (currentTime < 2.5f)
		{
			float newScaleX = Mathf.Lerp(initialScale.x, 0, currentTime / 2.5f);
            float newScaleY = Mathf.Lerp(initialScale.y, 0, currentTime / 2.5f);

			transform.localScale = new Vector3(newScaleX, newScaleY, initialScale.z);
			currentTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = new Vector3(0, 0, initialScale.z);
        gameObject.SetActive(false);
	}
}
