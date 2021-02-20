using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoControls : MonoBehaviour
{
		/*public Text currentMinutes;
		public Text currentSeconds;*/
		public Text totalMinutes;
		public Slider videoSlider;
		private VideoPlayer videoPlayer;

    void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
	{
		if (videoPlayer != null)
		{
			setTimeLeftUI();
			videoSlider.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
		}
		else
		{
			resetTimeUI();
		}
	}
	/*
		public void PlayPause() {
			videoPlayer = GetComponent<VideoPlayer>();
			if (videoPlayer) {
				if (videoPlayer.isPlaying) {
					Debug.Log("Should pause");
					videoPlayer.Pause();
				} else {
					videoPlayer.Play();
				}
			} else {
				Debug.Log("No player reference");
			}

		}*/

	void setTimeLeftUI()
	{
		string minutes = Mathf.Floor((int)((videoPlayer.clip.length - videoPlayer.time)/ 60)).ToString("00");
		string seconds = ((int)((videoPlayer.clip.length - videoPlayer.time) % 60)).ToString("00");
		totalMinutes.text = "-" + minutes + ":" + seconds;
	}

    void resetTimeUI() {
        //currentMinutes.text = "00";
        //currentSeconds.text = "00";
        totalMinutes.text = "-00:00";
        //totalSeconds.text = "00";
    }

	public void SetVideoFrame(float sliderValue)
	{
		videoPlayer.frame = Mathf.RoundToInt(sliderValue * videoPlayer.frameCount);
	}
}
