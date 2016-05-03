using UnityEngine;
using System.Collections;

public enum EColor
{
	RED,
	GREEN
}

public class Door : MonoBehaviour
{
	public EColor doorColor;
	public bool locked = true;
	public Sprite redDoorTop, redDoorBottom, greenDoorTop, greenDoorBottom, openDoorBottom;
	public Transform targetDoor;


	void Start()
	{
		if (doorColor == EColor.RED)
		{
			transform.FindChild("Top").GetComponent<SpriteRenderer>().sprite = redDoorTop;
			transform.FindChild("Bottom").GetComponent<SpriteRenderer>().sprite = redDoorBottom;
		} else
		{
			transform.FindChild("Top").GetComponent<SpriteRenderer>().sprite = greenDoorTop;
			transform.FindChild("Bottom").GetComponent<SpriteRenderer>().sprite = greenDoorBottom;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!locked && other.CompareTag("Player"))
		{
			if (doorColor == EColor.RED)
			{
				GameManager.i.SendMessage("NextLevel");
			} else
			{
				other.transform.position = targetDoor.position;
			}

		}
	}

	void Unlock()
	{
		transform.FindChild("Bottom").GetComponent<SpriteRenderer>().sprite = openDoorBottom;
		locked = false;
	}
}
