using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour
{
	public bool greenLocked = false;
	public bool redLocked = false;
	public GameObject greenBlocks;
	public GameObject redBlocks;

	void Update()
	{
		if (greenLocked != greenBlocks.activeSelf)
		{
			greenBlocks.SetActive(greenLocked);
		}

		if (redLocked != redBlocks.activeSelf)
		{
			redBlocks.SetActive(redLocked);
		}
	}

	void Unlock(EColor color)
	{
		if (color == EColor.RED)
		{
			redLocked = true;
		} else
		{
			greenLocked = true;
		}
	}
}
