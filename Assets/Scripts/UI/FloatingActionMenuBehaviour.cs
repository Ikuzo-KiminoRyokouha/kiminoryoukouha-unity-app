using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Debug = UnityEngine.Debug;

public class FloatingActionMenuBehaviour : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private List<GameObject> ButtonArr;
	public float MaxPosYEach = 120.0f;
	private float speed = 8.0f;
	private bool IsPendingEffect = false;
	private bool IsUp = false;
	private bool IsDown = false;
	private bool IsMax = false;


	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (IsUp)
		{
			IsUp = MenuUp();
		}

		if (IsDown)
		{
			IsMax = false;
			IsDown = MenuDown();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{

		if (!IsUp && !IsMax)
		{
			IsUp = true;
			IsDown = false;
		}
		else if (!IsDown)
		{
			IsDown = true;
			IsUp = false;
		}
	}

	bool MenuUp()
	{
		if (ButtonArr[ButtonArr.Count - 1].GetComponent<RectTransform>().anchoredPosition.y >= MaxPosYEach * ButtonArr.Count)
		{
			IsMax = true;
			return false;
		}
		for (int i = 0; i < ButtonArr.Count; i++)
		{
			Vector3 originTransform = ButtonArr[i].GetComponent<RectTransform>().anchoredPosition;
			if (originTransform.y >= MaxPosYEach)
			{
				ButtonArr[i].SetActive(true);
			}
			if (originTransform.y < MaxPosYEach * (i + 1))
			{
				ButtonArr[i].GetComponent<RectTransform>().anchoredPosition = originTransform + new Vector3(0, speed, 0);
			}
		}
		return true;
	}
	bool MenuDown()
	{
		if (ButtonArr[ButtonArr.Count - 1].GetComponent<RectTransform>().anchoredPosition.y <= 0.0f)
		{
			return false;
		}
		for (int i = 0; i < ButtonArr.Count; i++)
		{
			Vector3 originTransform = ButtonArr[i].GetComponent<RectTransform>().anchoredPosition;

			if (originTransform.y > 0.0f)
			{
				if ((originTransform - new Vector3(0, speed, 0)).y <= 0.0f)
				{
					ButtonArr[i].SetActive(false);
				}
				ButtonArr[i].GetComponent<RectTransform>().anchoredPosition = originTransform - new Vector3(0, speed, 0);
			}
		}
		return true;
	}
}
