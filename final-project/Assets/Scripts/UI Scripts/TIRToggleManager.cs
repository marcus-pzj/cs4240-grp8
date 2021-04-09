using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIRToggleManager : MonoBehaviour
{
	private GameObject[] lasers;
	public Toggle toggle1;
	public Toggle toggle2;
	public Toggle toggle3;
	public Toggle toggle4;
	public AudioSource audioSource;
	public AudioClip clip;
	private GameObject popup1;
	private GameObject popup2;
	private GameObject popup3;
	private GameObject popup4;
	private TIRStateManager stateManager;
	private bool soundPlayed;

	void Start()
	{
		popup1 = toggle1.transform.GetChild(1).gameObject;
		popup2 = toggle2.transform.GetChild(1).gameObject;
		popup3 = toggle3.transform.GetChild(1).gameObject;
		popup4 = toggle4.transform.GetChild(1).gameObject;
		soundPlayed = false;
	}

	void Update()
	{
		lasers = GameObject.FindGameObjectsWithTag("LightSource");

		if (lasers.Length == 0)
		{
			toggle1.interactable = true;
			toggle2.interactable = false;
			toggle3.interactable = false;
			toggle4.interactable = false;
		}
		else
		{
			toggle1.interactable = false;

			stateManager = lasers[0].GetComponent<TIRStateManager>();

			if (!stateManager.AreObjectsDetected())
			{
				toggle2.interactable = true;
				toggle3.interactable = false;
				toggle4.interactable = false;
			}
			else
			{
				toggle2.interactable = false;
				if (!stateManager.isObjectiveMet())
				{
					toggle3.interactable = true;
					toggle4.interactable = false;
				}
				else
				{
					toggle3.interactable = false;
					toggle4.interactable = true;

					if (!soundPlayed)
					{
						audioSource.PlayOneShot(clip, 0.7f);
						soundPlayed = true;
					}
				}
			}
		}


		if (toggle1.interactable && toggle1.isOn) popup1.SetActive(true);
		else popup1.SetActive(false);
		if (toggle2.interactable && toggle2.isOn) popup2.SetActive(true);
		else popup2.SetActive(false);
		if (toggle3.interactable && toggle3.isOn) popup3.SetActive(true);
		else popup3.SetActive(false);
		if (toggle4.interactable && toggle4.isOn) popup4.SetActive(true);
		else popup4.SetActive(false);
	}
}

