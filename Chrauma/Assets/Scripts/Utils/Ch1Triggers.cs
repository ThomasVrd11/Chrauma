using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch1Triggers : MonoBehaviour
{
    public GameObject gate1;
    public GameObject gate2;
    public GameObject gate3;
    public Spawner spawner;
    [SerializeField] GameObject spawn1;
    [SerializeField] GameObject spawn2;
    [SerializeField] GameObject spawn3;
    private float gate1X = 2.70f;
    private float gate1Y = 2.70f;
    private float gate2X = 5.55f;
    private float gate2Y = 5.55f;
    private float gate3X = 6.64f;
    private float gate3Y = 6.64f;
    public bool debugMode = false;
    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (debugMode) Debug.Log(other.name + " has entered");
        if (other.tag == "Player" && !hasEntered)
        {
            if (debugMode) Debug.Log("tag is " + other.tag);
            switch (gameObject.name)
            {
                case "TriggerRoom1":
                    if (debugMode) Debug.Log("I'm in trigger room 1");
                    gate1.SetActive(true);
                    gate2.SetActive(true);
                    StartCoroutine(PortalAppear(gate1));
                    StartCoroutine(PortalAppear(gate2));
                    spawner.numberOfEnnemiesNeeded = 5;
                    spawner.StartSpawning();
                    break;
                case "TriggerRoom2":
                    if (debugMode) Debug.Log("i'm in trigger 2");
                    spawn1.transform.position = new Vector3(103, -8, -1f);
                    spawn2.transform.position = new Vector3(99, -8, -17f);
                    spawn3.transform.position = new Vector3(80, -8, -25f);
                    spawner.numberOfKilledEnnemies = 0;
                    gate2.SetActive(true);
                    gate3.SetActive(true);
                    StartCoroutine(PortalAppear(gate2));
                    StartCoroutine(PortalAppear(gate3));
                    spawner.numberOfEnnemiesNeeded = 5;
                    spawner.StartSpawning();
                    if (debugMode) Debug.Log("spawn active: " + spawner.isSpawningActive);
                    if (debugMode) Debug.Log("gate1 2 active: " + gate2.activeInHierarchy);
                    if (debugMode) Debug.Log("gate 2 active: " + gate3.activeInHierarchy);

                    break;
                default:
                    break;
            }
        }
        hasEntered = true;
    }
    IEnumerator PortalAppear(GameObject gate)
    {
        float scaleX = 0f;
        float scaleY = 0f;
        switch (gate.name)
        {
            case "Gate1":
                scaleX = gate1X;
                scaleY = gate1Y;
                break;
            case "Gate2":
                scaleX = gate2X;
                scaleY = gate2Y;
                break;
            case "Gate3":
                scaleX = gate3X;
                scaleY = gate3Y;
                break;
            default:
                break;
        }
        gate.GetComponent<BoxCollider>().enabled = true;
        Vector3 initialScale = gate.transform.localScale;
        float currentTime = 0f;
        while (currentTime < 2.5f)
        {
            float newScaleX = Mathf.Lerp(initialScale.x, scaleX, currentTime / 2.5f);
            float newScaleY = Mathf.Lerp(initialScale.y, scaleY, currentTime / 2.5f);

            gate.transform.localScale = new Vector3(newScaleX, newScaleY, initialScale.z);
            currentTime += Time.deltaTime;
            yield return null;
        }
        gate.transform.localScale = new Vector3(scaleX, scaleY, initialScale.z);
    }
}
