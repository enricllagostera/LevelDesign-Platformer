using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sign : MonoBehaviour
{
	public string text;
	GameObject dialog;
	public float duration;

	void Start()
	{
		dialog = GameObject.Find("Dialog");
		dialog.transform.localScale = Vector3.zero;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			dialog.GetComponentInChildren<Text>().text = text;
			Go.to(dialog.transform, duration, new GoTweenConfig()
			.scale(Vector3.one)
			.setEaseType(GoEaseType.BackInOut));
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			dialog.GetComponentInChildren<Text>().text = "";
			Go.to(dialog.transform, duration, new GoTweenConfig()
			.scale(Vector3.zero)
			.setEaseType(GoEaseType.BackInOut));
		}
	}
}
