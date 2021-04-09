using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene("CardScene", LoadSceneMode.Single);
	}

	public void PlayReflection()
	{
		SceneManager.LoadScene("ReflectionLevelScene", LoadSceneMode.Single);
	}

	public void PlayRefraction()
	{
		SceneManager.LoadScene("RefractionLevelScene", LoadSceneMode.Single);
	}

	public void PlayTIR()
	{
		SceneManager.LoadScene("TotalInternalReflectionLevelScene", LoadSceneMode.Single);
	}
}