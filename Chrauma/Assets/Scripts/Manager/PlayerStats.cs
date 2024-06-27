using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
	public static PlayerStats instance;
	private int max_health;
	private int max_entropy;
	[SerializeField] int current_health;
	[SerializeField] int current_entropy;
	[SerializeField] private int buffer_health;
	private int buffer_entropy;
	private GameObject UI;
	private Slider slider_health;
	private Slider slider_entropy;
	public bool debugMode = false;
	public bool debugDeath = false;
	private GameObject deathScreen;


	private void Awake()
	{
		max_health = 100;
		max_entropy = 100;
	}
	void Start()
	{
		instance = this;
		InitializeUI();
		/* Setup stats */
		current_health = max_health;
		current_entropy = max_entropy;
		buffer_entropy = max_entropy;
		buffer_health = max_health;
		UpdateSliders();
	}
	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	void Update()
	{
		if (buffer_health != current_health)
		{
			current_health = buffer_health;
			UpdateSliders();
		}
		if (buffer_entropy != current_entropy)
		{
			UpdateSliders();
		}
		if (debugDeath) Death();
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		InitializeUI();
	}
	private void InitializeUI()
	{
		UI = GameObject.Find("---- UI ----");
		if (UI != null)
		{
			Transform healthTransform = UI.transform.Find("HealthBar_");
			if (healthTransform != null)
			{
				slider_health = healthTransform.GetComponent<Slider>();
			}

			Transform entropyTransform = UI.transform.Find("EntropyBar_");
			if (entropyTransform != null)
			{
				slider_entropy = entropyTransform.GetComponent<Slider>();
			}
			UpdateSliders();
		}
	}


	// Update is called once per frame
	private void UpdateSliders()
	{
		if (slider_health != null)
		{
			slider_health.value = current_health;
			if (debugMode) Debug.Log("health has been updated to: " + slider_health.value);
		}
		if (slider_entropy != null)
			slider_entropy.value = current_entropy;
	}
	public void TakeDamage(int damage)
	{
		buffer_health -= damage;
		if(buffer_health < 0) Death();
	}
	public void Heal(int heal)
	{
		buffer_health += heal;
	}
	public void LoadData(GameData data)
	{
		this.buffer_health = data.health;
		this.buffer_entropy = data.entropy;
	}
	public void SaveData(GameData data)
	{
		if (data == null)
		{
			Debug.LogError("GameData is null in playerstats.SaveData");
			return;
		}
		data.health = this.current_health;
		data.entropy = this.current_entropy;
	}
	public void SetHealthBar(Slider slider)
	{
		slider_health = slider;
		if (debugMode) Debug.Log("health set to: " + slider_health);
		UpdateSliders();
	}
	public void Death()
	{
		deathScreen = GameObject.Find("DeathScreen");
		if (deathScreen)
		{
			deathScreen.transform.Find("Panel").gameObject.SetActive(true);
		}
	}

	public void HideDeath()
	{
		if (deathScreen)
		{
			deathScreen.transform.Find("Panel").gameObject.SetActive(false);
			Debug.Log("Blergh!!");
		}
	}

}
