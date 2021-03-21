using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRIndex : MonoBehaviour
{
	private Text rIndexValue;


	void Start()
    {
		rIndexValue = GetComponent<Text>();
		rIndexValue.text = "1.50";
	}

	public void SetRIndexValue(float sliderValue)
	{
		rIndexValue.text = sliderValue.ToString("F");
	}

	public void SetRIndexColorDown()
	{
		rIndexValue.color = new Color(1f, 1f, 1f, 1f);
	}

	public void SetRIndexColorUp()
	{
		rIndexValue.color = new Color(0.9f, 0.9f, 0.9f);
	}
}
