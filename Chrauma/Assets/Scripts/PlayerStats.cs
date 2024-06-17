using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
	/* Singleton reference */
	public static PlayerStats instance;

	/* Player stats */
	private int max_health;
	private int max_entropy;
	[SerializeField] int current_health;
	[SerializeField] int current_entropy;
	[SerializeField] private int buffer_health;
	private int buffer_entropy;

	/* References to the UI */
	private GameObject UI;
	private Slider slider_health;
	private Slider slider_entropy;


	private void Awake()
	{
		/* Set player stat to max(just in case) */
		max_health = 100;
		max_entropy = 100;
	}
	void Start()
	{
		/*
		set singleton instance
		initialise the ui
		Setup stats and update the bars sliders
		*/
		instance = this;
		InitializeUI();
		current_health = max_health;
		current_entropy = max_entropy;
		buffer_entropy = max_entropy;
		buffer_health = max_health;
		UpdateSliders();
	}
	private void OnEnable()
	{
		/* Subscribe to sceneLoaded event*/
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		/* Unsubscribe to sceneLoaded event*/
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	void Update()
	{
		/*
		Check if current stat is different from buffer stat
		Update sliders if yes
		*/
		if (buffer_health != current_health)
		{
			current_health = buffer_health;
			UpdateSliders();
		}
		if (buffer_entropy != current_entropy)
		{
			UpdateSliders();
		}
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		/*
        Called when a new scene is loaded.
		Initialise the UI
		scene: the scene that was loaded
        mode: the mode in wich the scene was loaded 
		*/
		InitializeUI();
	}
	private void InitializeUI()
	{
		/* Find the stat bars and their sliders */
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

	private void UpdateSliders()
	{
		/* Set the sliders value to the stats values*/
		if (slider_health != null)
		{
			slider_health.value = current_health;
		}
		if (slider_entropy != null)
			slider_entropy.value = current_entropy;
	}
	public void TakeDamage(int damage)
	{
		/*
		Reduce  buffer health by $damage
		damage: damage dealt to the player
		*/
		buffer_health -= damage;
	}
	public void LoadData(GameData data)
	{
		/* Load palyer stats from the GameData*/
		this.buffer_health = data.health;
		this.buffer_entropy = data.entropy;
	}
	public void SaveData(GameData data)
	{
		/*
		Check if GameData exist
		Store current stats to GameData
		*/
		if (data == null)
		{
			Debug.LogError("GameData is null in playerstats.SaveData");
			return;
		}
		data.health = this.current_health;
		data.entropy = this.current_entropy;
	}
}
