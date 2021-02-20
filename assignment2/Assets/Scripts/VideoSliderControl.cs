using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoSliderControl : MonoBehaviour
{
	public VideoPlayer videoPlayer;
	private Slider videoSlider;
	bool update = false;

	void Awake()
	{
		videoSlider = GetComponent<Slider>();
	}

	public void SetVideoFrame(float sliderValue)
	{
		update = true;
		videoPlayer.frame = Mathf.RoundToInt(sliderValue * videoPlayer.frameCount);
		update = false;
	}

	void Update()
	{
		if (!update)
		{
			videoSlider.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
		}
	}
}