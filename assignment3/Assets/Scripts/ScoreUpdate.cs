using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
	Text scoreText;
	private int score;

    // Start is called before the first frame update
    void Start()
    {
		score = 0;
		scoreText = GetComponent<Text>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightBracket))
		{
			increaseScore();
		}
		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			decreaseScore();
		}
	}

	void increaseScore()
	{
		score++;
		scoreText.text = score.ToString();
	}

	void decreaseScore()
	{
		score--;
		GetComponent<Animator>().SetTrigger("Decrease");
		scoreText.text = score.ToString();
	}
}
