using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager i;
	int currentLevel;
	//public string[] lvls;
	private bool changingLvls = false;

	void Awake()
	{
		if (i == null)
		{
			i = this;

			DontDestroyOnLoad(gameObject);
		} else
		{
			Destroy(gameObject);
		}
		GameManager.i.currentLevel = SceneManager.GetActiveScene().buildIndex;
	}

	void NextLevel()
	{
		if (changingLvls)
			return;
		currentLevel++;
		if (currentLevel >= SceneManager.sceneCountInBuildSettings)
		{
			currentLevel = 0;
		}
		StartCoroutine(Load(0.1f));
	}

	void Reload()
	{
		if (changingLvls)
			return;
		currentLevel = SceneManager.GetActiveScene().buildIndex; 
		StartCoroutine(Load(0.2f));
	}

	IEnumerator Load(float wait)
	{
		changingLvls = true;
		yield return new WaitForSeconds(wait);
		SceneManager.LoadScene(currentLevel);
		changingLvls = false;
	}
}
