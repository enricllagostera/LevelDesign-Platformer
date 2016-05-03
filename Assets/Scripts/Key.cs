using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{
	public Door[] doors;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			foreach (var door in doors)
			{
				door.SendMessage("Unlock", SendMessageOptions.DontRequireReceiver);
			}
			Destroy(gameObject);
		}
	}
}
