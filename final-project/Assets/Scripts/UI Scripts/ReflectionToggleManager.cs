using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflectionToggleManager : MonoBehaviour
{
	private GameObject[] lasers;
	public Toggle toggle1;
	public Toggle toggle2;
	public Toggle toggle3;
	private GameObject popup1;
	private GameObject popup2;
	private GameObject popup3;

	void Start()
	{
		popup1 = toggle1.transform.GetChild(1).gameObject;
		popup2 = toggle2.transform.GetChild(1).gameObject;
		popup3 = toggle3.transform.GetChild(1).gameObject;
	}

	void Update()
	{
		lasers = GameObject.FindGameObjectsWithTag("LightSource");

		if (lasers.Length == 0)
		{
			toggle1.interactable = true;
			toggle2.interactable = false;
			toggle3.interactable = false;
		}
		else
		{
			toggle1.interactable = false;

			if (!lasers[0].GetComponent<ReflectionStateManager>().AreObjectsDetected())
			{
				toggle2.interactable = true;
				toggle3.interactable = false;
			}
			else
			{
				toggle2.interactable = false;
				toggle3.interactable = true;
			}
		}


		if (toggle1.interactable && toggle1.isOn) popup1.SetActive(true);
		else popup1.SetActive(false);
		if (toggle2.interactable && toggle2.isOn) popup2.SetActive(true);
		else popup2.SetActive(false);
		if (toggle3.interactable && toggle3.isOn) popup3.SetActive(true);
		else popup3.SetActive(false);
	}
}
