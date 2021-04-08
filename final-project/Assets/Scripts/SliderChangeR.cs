using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderChangeR : MonoBehaviour
{
	private TMPro.TextMeshProUGUI rIndexText;
	public Slider slider;
    
	// Start is called before the first frame update
    void Start()
    {
		rIndexText = GetComponent<TMPro.TextMeshProUGUI>();
		rIndexText.text = "r = " + slider.value.ToString("0.00");
	}

	public void changeRIndexText(float val)
	{
		rIndexText.text = "r = " + slider.value.ToString("0.00");
	}
}
