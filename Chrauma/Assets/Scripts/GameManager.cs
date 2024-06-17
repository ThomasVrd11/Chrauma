using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	/* Singleton reference */
	public static GameManager instance;
	private void Awake()
	{
		/* singleton pattern*/
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void SwitchScene(int sceneId)
	{
		/*
		Asynchronously with to new scene with ID
		sceneID: ID of the scene to switch to
		*/
		SceneManager.LoadSceneAsync(sceneId);
	}
	
	public void ExitGame()
	{
		/*
		If running in the Unity Editor, exit the play mode
        If running in a standalone build, exit the application
		*/
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
	}
}
