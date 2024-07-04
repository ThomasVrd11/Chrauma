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
        /* if object in trigger is the player, get his control script and start a tutorial based on the gameobject the script is on */
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
        /* deactivate the gameobejct when player leave */
        if (characterControls) characterControls.gamePaused = false;
        gameObject.SetActive(false);
    }
    public void Resume()
    {
        /* unpause the game */
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }
    public void Pause()
    {
        /* pause the game */
        if (characterControls) characterControls.gamePaused = true;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void StartMovTuto()
    {
        /* activate the movement tutorial and pause the game */
        movementTuto.SetActive(true);
        Pause();
    }
    public void StartDashTuto()
    {
        /* activate the dash tutorial and pause the game */
        dashTuto.SetActive(true);
        Pause();
    }
    private void StartAttackTuto()
    {
        /* activate the attack tutorial and fight UI with health initialized, pause the game and start the spawner */
        attackTuto.SetActive(true);
		tpEndOfTuto.SetActive(true);
        Pause();
        fightingUI.SetActive(true);
		PlayerStats.instance.SetHealthBar(healthSlider);
        spawner.isSpawningActive = true;
    }
}
