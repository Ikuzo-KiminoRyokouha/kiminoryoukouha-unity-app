using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Debug = UnityEngine.Debug;

public class EmotionButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
	// 자기 자신의 texture이름을 따온다.
	// 인풋 창에게 선택한 감정읋 전달해 주기 위해 인풋 창의 게임 오브젝트를 받아온다.

	public GameObject EmotionInputCanvas;

	void Start()
	{

	}

	void Update()
	{

	}


	public void OnPointerClick(PointerEventData eventData)
	{
		// 클릭시 인풋 창에게 자신의 감정을 던져 주는 것으로 제출시 자신의 검정을 같이 제출하게 한다.
		EmotionInputCanvas.SetActive(true);
		EmotionInputCanvas.GetComponent<EmotionInputBehaviour>().emotion = gameObject.name;
	}

}