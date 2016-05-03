using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EPlatformBehaviour
{
	PINGPONG,
	LOOP
}

public class MovingPlatform : MonoBehaviour
{
	public EPlatformBehaviour behaviour;
	public EdgeCollider2D basePath;
	public float duration;

	void Start()
	{
		List<Vector3> pathV3 = new List<Vector3>();
		GoSpline path;
		foreach (var p in basePath.points)
		{
			pathV3.Add(basePath.transform.TransformPoint((Vector3)p));
		}
		path = new GoSpline(pathV3, true);
		transform.position = basePath.gameObject.transform.position;
		if (behaviour == EPlatformBehaviour.PINGPONG)
		{
			Go.to(transform, duration, new GoTweenConfig()
				.positionPath(path)
				.setIterations(-1, GoLoopType.PingPong));
		} else
		{
			Go.to(transform, duration, new GoTweenConfig()
				.positionPath(path)
				.setIterations(-1, GoLoopType.RestartFromBeginning));
		}
	}
}

