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
		string[,] testData = new string[6, 2] { { "Laugh", "めちゃいいねここ！.." }, { "Simuruk", "思ったこととは少し違う..." }, { "Surprise", "ここにこんなところが！.!" }, { "Surprise", "飛行機あるやねーか? ㅋㅋㅋ" }, { "Angry", "あ....学校行きたくない" }, { "Laugh", "グルグル回転ロータリー~" } };
		for (int i = 0; i < Coordinates.Count; i++)
		{
			Vector3 pointVector = GPS.GPSEncoder.GPSToUCS((float)Coordinates[i].y, (float)Coordinates[i].x);
			GameObject prefab = Instantiate(EmoticonPanelPrefab, pointVector, Quaternion.identity);
			prefab.GetComponent<EmotionPanelBehaviour>().emotion = testData[i, 0];
			prefab.GetComponent<EmotionPanelBehaviour>().content = testData[i, 1];
			prefab.GetComponent<EmotionPanelBehaviour>().IsStickyCamera = true;
		}
	}
}
