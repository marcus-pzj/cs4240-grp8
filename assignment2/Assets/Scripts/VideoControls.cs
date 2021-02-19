using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoControls : MonoBehaviour
{
		public Text currentMinutes;
		public Text currentSeconds;
		public Text totalMinutes;
		public Text totalSeconds;
		private VideoPlayer videoPlayer;

    void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer) {
            setTotalTimeUI();
            if (videoPlayer.isPlaying) {
                setCurrentTimeUI();
            }
        } else {
            resetTimeUI();
        }
    }

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

    }

	void setCurrentTimeUI() {
		string minutes = Mathf.Floor((int) videoPlayer.time / 60).ToString("00");
		string seconds = ((int) videoPlayer.time % 60).ToString("00");

		currentMinutes.text = minutes;
		currentSeconds.text = seconds;
	}

    void setTotalTimeUI() {
        string minutes = Mathf.Floor((int) videoPlayer.clip.length / 60).ToString("00");
        string seconds = ((int) videoPlayer.clip.length % 60).ToString("00");

        totalMinutes.text = minutes;
        totalSeconds.text = seconds;
    }

    void resetTimeUI() {
        currentMinutes.text = "00";
        currentSeconds.text = "00";
        totalMinutes.text = "00";
        totalSeconds.text = "00";
    }
}
