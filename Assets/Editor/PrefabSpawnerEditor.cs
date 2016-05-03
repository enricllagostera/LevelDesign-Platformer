using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(PrefabSpawner))]
[CanEditMultipleObjects]
public class PrefabSpawnerEditor : Editor
{
	private bool ready = false;
	private int total;
	GameObject[] all;

	void Recalculate()
	{
		all = GameObject.FindGameObjectsWithTag("Spawner");
		total = 0;
		foreach (var go in all)
		{
			if (Resources.Load<GameObject>(go.name) != null)
			{
				total++;
			}
		}
		Debug.Log("Calc: " + total);
	}

	void Spawn()
	{
		if (total > 0)
		{
			foreach (var go in all)
			{
				GameObject res = Resources.Load<GameObject>(go.name);
				//Debug.Log("Creating " + go.name + " -> " + res.name);
				GameObject newGO = (GameObject)Instantiate(res, go.transform.position, go.transform.rotation);
				newGO.name = go.name;
				newGO.transform.SetParent(((PrefabSpawner)target).transform);
				go.SetActive(false);
			}
		}
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if (GUILayout.Button("Recalculate"))
		{
			Recalculate();
		}
		GUILayout.Label("Objects to spawn: " + total);
		if (GUILayout.Button("Spawn"))
		{
			Spawn();
		}
	}
}
