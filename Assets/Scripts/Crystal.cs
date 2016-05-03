using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour
{
	public EColor color;
	public float speed;

	void Start()
	{	
		speed = (Random.Range(-0.5f, 0.5f) < 0) ? -speed : speed;
	}

	void Update()
	{
		transform.Rotate(0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GameObject.Find("BlockManager").SendMessage("Unlock", color, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
