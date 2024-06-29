using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] GameObject fallPreventPos;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject movementTuto;
    [SerializeField] GameObject dashTuto;
    [SerializeField] GameObject attackTuto;
    [SerializeField] GameObject fightingUI;
    [SerializeField] Spawner spawner;
	[SerializeField] GameObject tpEndOfTuto;
	[SerializeField] Slider healthSlider;

    private CharacterControls characterControls;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {
            characterControls = other.GetComponent<CharacterControls>();
            if(gameObject.name == "DashTutoCube")
            {
                StartDashTuto();
            } else if (gameObject.name == "AttackTutoCube")
            {
                StartAttackTuto();
            } else if(gameObject.name == "BridgeHole")
            {
                CharacterController playerCC = other.gameObject.GetComponent<CharacterController>();
                playerCC.enabled = false;
                other.transform.SetPositionAndRotation(fallPreventPos.transform.position,fallPreventPos.transform.rotation);
                playerCC.enabled = true;
            }

        }
    }
    private void OnTriggerExit(Collider other) {

        gameObject.SetActive(false);
        if (characterControls) characterControls.gamePaused = false;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }
    public void Pause()
    {
        if (characterControls) characterControls.gamePaused = true;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void StartMovTuto()
    {
        movementTuto.SetActive(true);
        Pause();
    }
    public void StartDashTuto()
    {
        dashTuto.SetActive(true);
        Pause();
    }
    private void StartAttackTuto()
    {
        attackTuto.SetActive(true);
		tpEndOfTuto.SetActive(true);
        Pause();
        fightingUI.SetActive(true);
		PlayerStats.instance.SetHealthBar(healthSlider);
        spawner.isSpawningActive = true;
    }
}
