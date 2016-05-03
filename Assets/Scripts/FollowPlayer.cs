using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Player player;

	public bool useGroundSnap = true;
	public bool useZoomOut = false;
	public float groundSnapRange = 2f;
	private float _lastGroundY;

	private Camera _cam;
	private Vector2 _camSize;
	public float cameraDistance;
	public float zoomOutDistance;
	public Vector2 generalOffset;

	private Vector2 _targetPos;
	public float smoothFactor;

	public Bounds deadZone, outerLimits;
	public Vector2 deadZoneOffset;



	void Start()
	{
		_lastGroundY = player.transform.position.y;
		_cam = GetComponent<Camera>();
		_cam.orthographicSize = cameraDistance;
		_targetPos = player.transform.position + new Vector3(0, 0, -50f) + (Vector3)deadZoneOffset;
		deadZone.size += new Vector3(0, 0, 100);
	}

	void FixedUpdate()
	{
		deadZone.center = transform.position + new Vector3(0, 0, 20f) + (Vector3)deadZoneOffset;
		// deadzone
		if (player.transform.position.x < deadZone.min.x ||
		    player.transform.position.x > deadZone.max.x)
		{
			_targetPos.x = player.transform.position.x;
		}
		if (player.transform.position.y < deadZone.min.y ||
		    player.transform.position.y > deadZone.max.y)
		{
			_targetPos.y = player.transform.position.y;
		}
		generalOffset = (player.fastFall) ? generalOffset = new Vector2(0, -4f) : generalOffset = new Vector2(0, 0);
		if (Input.GetAxis("Vertical") < -0.02f)
		{
			generalOffset.y += 2f * Input.GetAxis("Vertical");
		}

		Vector3 newPos = Vector2.Lerp(transform.position, _targetPos + generalOffset, Time.fixedDeltaTime * smoothFactor);

		if (player.fastFall && useZoomOut)
		{
			_cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, zoomOutDistance, Time.fixedDeltaTime * 0.75f);
		} else
		{
			_cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, cameraDistance, Time.fixedDeltaTime * 7f);
		}

		_camSize = new Vector2();
		_camSize.x = Mathf.Abs(_cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);
		_camSize.y = Mathf.Abs(_cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - _cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);
		// outerlimits
		if (newPos.y - _camSize.y / 2 < outerLimits.min.y ||
		    newPos.y + _camSize.y / 2 > outerLimits.max.y)
		{
			newPos.y = transform.position.y;
		}

		if (newPos.x - _camSize.x / 2 < outerLimits.min.x ||
		    newPos.x + _camSize.x / 2 > outerLimits.max.x)
		{
			newPos.x = transform.position.x;
		}
		newPos.z = -50f;
		transform.position = newPos;
	}



	void GroundTouched()
	{
		if (useGroundSnap)
		{
			if (Mathf.Abs(_lastGroundY - player.transform.position.y) > groundSnapRange)
			{
				_lastGroundY = player.transform.position.y;
				_targetPos.y = player.transform.position.y;
			}
		}

	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(_targetPos + generalOffset, 0.7f);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(outerLimits.center, outerLimits.size);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(_camSize.x, _camSize.y));
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(deadZone.center, deadZone.size);
	}

}
