using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPS;

public class EmoticonPanelProvider : MonoBehaviour
{
	public GameObject EmoticonPanelPrefab;

	public List<Vector2> Coordinates;

	// Start is called before the first frame update
	void Start()
	{
		Vector2 originPos = new Vector2(35.945906f, 128.4611931f);
		GPS.GPSEncoder.SetLocalOrigin(originPos);
		renderEmotionPanel();
	}

	// Update is called once per frame
	void Update()
	{

	}


	public void renderEmotionPanel()
	{
		for (int i = 0; i < Coordinates.Count; i++)
		{
			Vector3 pointVector = GPS.GPSEncoder.GPSToUCS((float)Coordinates[i].y, (float)Coordinates[i].x);
			GameObject prefab = Instantiate(EmoticonPanelPrefab, pointVector, Quaternion.identity);
			prefab.GetComponent<EmotionPanelBehaviour>().emotion = "Angry";
			prefab.GetComponent<EmotionPanelBehaviour>().content = "hi";
		}
	}
}
